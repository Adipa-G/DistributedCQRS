using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.Resolve.Helpers;
using DistCqrs.Core.Services;
using DistCqrs.Sample.Domain;

namespace DistCqrs.Sample.Service.Resolve
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IList<Tuple<Type, Type, Type>> commandHandlerMappings;
        private readonly IList<Tuple<Type, Type, Type>> eventHandlerMappings;
        private readonly IDictionary<string,IBus> buses;
        private readonly IDictionary<string,IService> services;

        public ServiceLocator()
        {
            buses = new ConcurrentDictionary<string, IBus>();
            services = new ConcurrentDictionary<string, IService>();

            var assemblies = new[] { typeof(BaseCommand).GetTypeInfo().Assembly };
            commandHandlerMappings = ResolveUtils.GetCommandHandlerMappings(assemblies);
            eventHandlerMappings = ResolveUtils.GetEventHandlerMappings(assemblies);
        }

        public IBus ResolveBus(string busId)
        {
            throw new NotImplementedException();
        }

        public IService ResolveService(string serviceId)
        {
            throw new NotImplementedException();
        }

        public ICommandHandler<TRoot, TCmd> ResolveCommandHandler<TRoot, TCmd>()
            where TRoot : IRoot, new() where TCmd : ICommand
        {
            var handler = commandHandlerMappings
                .Where(m => m.Item1 == typeof(TRoot) && m.Item2 == typeof(TCmd))
                .Select(m => m.Item3);

            return (ICommandHandler<TRoot, TCmd>)handler;
        }

        public IEventHandler<TRoot, TEvent> ResolveEventHandler<TRoot, TEvent>() 
            where TRoot : IRoot, new() where TEvent : IEvent<TRoot>
        {
            var handler = eventHandlerMappings
                .Where(m => m.Item1 == typeof(TRoot) && m.Item2 == typeof(TEvent))
                .Select(m => m.Item3);

            return (IEventHandler<TRoot, TEvent>)handler;
        }
    }
}
