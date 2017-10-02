using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;
using DistCqrs.Core.EventStore;
using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.View;

namespace DistCqrs.Core.Command.Impl
{
    [ServiceRegistration(ServiceRegistrationType.Scope)]
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IEventStore eventStore;
        private readonly IRootTypeResolver rootTypeResolver;
        private readonly IServiceLocator serviceLocator;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IViewWriter viewWriter;

        public CommandProcessor(IServiceLocator serviceLocator,
            IRootTypeResolver rootTypeResolver,
            IUnitOfWorkFactory unitOfWorkFactory,
            IEventStore eventStore,
            IViewWriter viewWriter)
        {
            this.serviceLocator = serviceLocator;
            this.rootTypeResolver = rootTypeResolver;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.eventStore = eventStore;
            this.viewWriter = viewWriter;
        }

        public async Task Process(ICommand cmd)
        {
            IRoot root;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var rootType = await eventStore.GetRootType(cmd.RootId) ??
                               rootTypeResolver.GetRootType(cmd);

                var commandHandler = InvokeGeneric<object>(this,
                    "ResolveCommandHandler", new[] {rootType, cmd.GetType()});
                if (commandHandler == null)
                    throw new ServiceLocationException(
                        $"Cannot resolve service to process command of type {cmd.GetType().FullName}");

                root = await GetRoot(rootType, cmd.RootId);

                var events = (IList) await Invoke<dynamic>(commandHandler,
                    "Handle",
                    new object[] {root, cmd});

                await InvokeGeneric<Task>(this, "SaveEvents",
                    new[] {rootType}, new object[] {events});

                await ApplyEvents(root, events);

                await unitOfWork.Complete();
            }

            await viewWriter.UpdateView(root);
        }

        private async Task<IRoot> GetRoot(Type rootType, Guid rootId)
        {
            var root = (IRoot) Activator.CreateInstance(rootType);
            var events = (IList) await InvokeGeneric<dynamic>(this,
                "GetEvents", new[] {rootType}, new object[] {rootId});

            if (events.Count == 0)
                return (IRoot) Activator.CreateInstance(rootType);

            await ApplyEvents(root, events);
            return root;
        }

        private async Task ApplyEvents<TRoot>(TRoot root, IList events)
            where TRoot : IRoot
        {
            foreach (var evt in events)
            {
                var evtHandler = InvokeGeneric<object>(this,
                    "ResolveEventHandler",
                    new[] {root.GetType(), evt.GetType()});
                var applyMethod = evtHandler.GetType().GetTypeInfo()
                    .GetMethod("Apply");

                var task =
                    (Task) applyMethod.Invoke(evtHandler, new[] {root, evt});
                await task;
            }
        }

        private T InvokeGeneric<T>(object src,
            string methodName,
            Type[] types,
            object[] values = null)
        {
            var method = src.GetType().GetTypeInfo().GetMethod(methodName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            var genericMethod = method.MakeGenericMethod(types);

            return (T) genericMethod.Invoke(src, values);
        }

        private T Invoke<T>(object src,
            string methodName,
            object[] values = null)
        {
            var method = src.GetType().GetTypeInfo().GetMethod(methodName);

            return (T) method.Invoke(src, values);
        }

        //wrappers to make refactor safe
        //ReSharper disable UnusedMember.Local
        private ICommandHandler<TRoot, TCmd>
            ResolveCommandHandler<TRoot, TCmd>()
            where TRoot : IRoot, new()
            where TCmd : ICommand
        {
            return serviceLocator.ResolveCommandHandler<TRoot, TCmd>();
        }

        private IEventHandler<TRoot, TEvent>
            ResolveEventHandler<TRoot, TEvent>()
            where TRoot : IRoot, new()
            where TEvent : IEvent<TRoot>
        {
            return serviceLocator.ResolveEventHandler<TRoot, TEvent>();
        }

        private Task<IList<IEvent<TRoot>>> GetEvents<TRoot>(Guid rootId)
            where TRoot : IRoot
        {
            return eventStore.GetEvents<TRoot>(rootId);
        }

        private Task SaveEvents<TRoot>(IList<IEvent<TRoot>> events)
            where TRoot : IRoot
        {
            return eventStore.SaveEvents(events);
        }
        //ReSharper restore UnusedMember.Local
    }
}