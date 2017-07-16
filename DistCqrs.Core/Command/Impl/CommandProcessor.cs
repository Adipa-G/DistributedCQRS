using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DistCqrs.Core.DependencyInjection;
using DistCqrs.Core.Domain;
using DistCqrs.Core.EventStore;
using DistCqrs.Core.Exceptions;
using DistCqrs.Core.View;

namespace DistCqrs.Core.Command.Impl
{
    public class CommandProcessor<TRoot, TCmd> : ICommandProcessor<TRoot, TCmd>
        where TRoot : IRoot, new()
        where TCmd : ICommand<TRoot>
    {
        private readonly IServiceLocator serviceLocator;
        private readonly IEventStore eventStore;
        private readonly IViewWriter viewWriter;

        public CommandProcessor(IServiceLocator serviceLocator,
            IEventStore eventStore,
            IViewWriter viewWriter)
        {
            this.serviceLocator = serviceLocator;
            this.eventStore = eventStore;
            this.viewWriter = viewWriter;
        }


        public async Task Process(TCmd cmd)
        {
            var commandHandler = serviceLocator.ResolveCommandHandler<TRoot,TCmd>();
            if (commandHandler == null)
            {
                throw new ServiceLocationException(
                    $"Cannot resolve service to process command of type {cmd.GetType().FullName}");
            }

            var root = await GetRoot(cmd);
            var events = await commandHandler.Handle(root, cmd);

            await eventStore.SaveEvents(events);
            await ApplyEvents(root, events);

            await viewWriter.UpdateView(root);
        }

        private async Task<TRoot> GetRoot(TCmd cmd)
        {
            var events = await eventStore.GetEvents<TRoot>(cmd.RootId);
            if (!events.Any()) return default(TRoot);

            var root = new TRoot();
            await ApplyEvents(root, events);
            return root;
        }

        private async Task ApplyEvents(TRoot root, IList<IEvent<TRoot>> events)
        {
            foreach (var evt in events)
            {
                var type = evt.GetType();

                var resolveMethod = serviceLocator.GetType().GetMethod("ResolveEventHandler");
                var genericResolveMethod = resolveMethod.MakeGenericMethod(root.GetType(),type);

                var evtHandler = genericResolveMethod.Invoke(serviceLocator, new object[] {  });
                var applyMethod = evtHandler.GetType().GetMethod("Apply");
                
                var task = (Task)applyMethod.Invoke(evtHandler, new object[] { root, evt });
                await task;
            }
        }
    }
}