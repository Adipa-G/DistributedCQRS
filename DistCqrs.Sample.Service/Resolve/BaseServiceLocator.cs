using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.Resolve.Helpers;
using DistCqrs.Core.Services;

namespace DistCqrs.Sample.Service.Resolve
{
    public abstract class BaseServiceLocator : IServiceLocator
    {
        private readonly IDictionary<string, IBus> buses;
        private readonly IDictionary<string, IService> services;

        protected BaseServiceLocator()
        {
            buses = new ConcurrentDictionary<string, IBus>();
            services = new ConcurrentDictionary<string, IService>();
        }

        protected abstract object Resolve(Type @interface);

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
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
            return (ICommandHandler<TRoot, TCmd>) Resolve(
                typeof(ICommandHandler<TRoot, TCmd>));
        }

        public IEventHandler<TRoot, TEvent> ResolveEventHandler<TRoot, TEvent>()
            where TRoot : IRoot, new() where TEvent : IEvent<TRoot>
        {
            return (IEventHandler<TRoot, TEvent>)Resolve(
                typeof(IEventHandler<TRoot, TEvent>));
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