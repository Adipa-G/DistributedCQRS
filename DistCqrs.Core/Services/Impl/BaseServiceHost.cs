using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Resolve;

namespace DistCqrs.Core.Services.Impl
{
    public abstract class BaseServiceHost : IServiceHost
    {
        private readonly Dictionary<string, IService> services;
        private readonly Dictionary<string, IBus> buses;

        private readonly ILog log;
        private readonly IServiceLocator serviceLocator;

        protected BaseServiceHost(ILog log,
            IServiceLocator serviceLocator)
        {
            this.log = log;
            this.serviceLocator = serviceLocator;

            this.services = new Dictionary<string, IService>();
            this.buses = new Dictionary<string, IBus>();
        }

        public abstract void PrepareExternalEntpoints();

        public void RegisterService(IService service)
        {
            if (services.ContainsKey(service.Id))
            {
                throw new ServiceRegistrationException($"Service {service.Id} is already registered.");
            }
            services[service.Id] = service;
        }

        public void InitialiseServices()
        {
            throw new NotImplementedException();
        }

        public async Task CommandReceived<TRoot, TCmd>(TCmd cmd)
            where TRoot : IRoot, new ()
            where TCmd : ICommand
        {
//            var service = serviceLocator.ResolveService<TRoot>();
//            if (service == null)
//            {
//                throw new UnknownCommandException(
//                    $"Unable to process command {cmd}, as it's unknown");
//            }
//
//            try
//            {
//                //await service.Process(cmd);
//                await service.OnCommandProcessed<TCmd>(new TRoot(), cmd);
//            }
//            catch (Exception ex)
//            {
//                log.LogException($"Error while processing command {cmd}", ex);
//                await service.OnCommandError(new TRoot(), cmd);
//            }
        }
    }
}