using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.Resolve.Helpers;
using DistCqrs.Core.Services;
using DistCqrs.Sample.Domain;

namespace DistCqrs.Sample.Service.Resolve
{
    public class ServiceLocator : IServiceLocator, IServiceRegister
    {
        private readonly IList<CommandHandlerMapping> commandHandlerMappings;
        private readonly IList<EventHandlerMapping> eventHandlerMappings;
        private readonly IDictionary<string, IBus> buses;
        private readonly IDictionary<string, IService> services;

        public ServiceLocator()
        {
            buses = new ConcurrentDictionary<string, IBus>();
            services = new ConcurrentDictionary<string, IService>();

            var assemblies = new[] {typeof(BaseCommand).GetTypeInfo().Assembly};

            commandHandlerMappings =
                ResolveUtils.GetCommandHandlerMappings(assemblies);
            eventHandlerMappings =
                ResolveUtils.GetEventHandlerMappings(assemblies);
        }

        public IBus ResolveBus(string busId)
        {
            if (!buses.ContainsKey(busId))
            {
                throw new ServiceLocationException(
                    $"Bus {busId} is not registered.");
            }
            return buses[busId];
        }

        public IService ResolveService(string serviceId)
        {
            if (!services.ContainsKey(serviceId))
            {
                throw new ServiceLocationException(
                    $"Service {serviceId} is not registered.");
            }
            return services[serviceId];
        }

        public ICommandHandler<TRoot, TCmd> ResolveCommandHandler<TRoot, TCmd>()
            where TRoot : IRoot, new() where TCmd : ICommand
        {
            var handler = commandHandlerMappings
                .Where(m => m.EntityType == typeof(TRoot) && m.CommandType == typeof(TCmd))
                .Select(m => m.CommandHandlerType);

            return (ICommandHandler<TRoot, TCmd>) handler;
        }

        public IEventHandler<TRoot, TEvent> ResolveEventHandler<TRoot, TEvent>()
            where TRoot : IRoot, new() where TEvent : IEvent<TRoot>
        {
            var handler = eventHandlerMappings
                .Where(m => m.EntityType == typeof(TRoot) &&
                            m.EventType == typeof(TEvent))
                .Select(m => m.EventHandlerType);

            return (IEventHandler<TRoot, TEvent>) handler;
        }

        public void Register(IBus bus)
        {
            if (buses.ContainsKey(bus.Id))
            {
                throw new ServiceRegistrationException(
                    $"Bus {bus.Id} is already registered.");
            }
            buses.Add(bus.Id, bus);
        }

        public void Register(IService service)
        {
            if (services.ContainsKey(service.Id))
            {
                throw new ServiceRegistrationException(
                    $"Service {service.Id} is already registered.");
            }
            services.Add(service.Id, service);
        }
    }
}