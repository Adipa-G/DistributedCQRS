using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.DependencyInjection;
using DistCqrs.Core.Exceptions;

namespace DistCqrs.Core.Services.Impl
{
    public abstract class BaseServiceHost : IServiceHost
    {
        private readonly ILog log;
        private readonly IServiceLocator serviceLocator;
        private readonly List<IService> services;
        
        protected BaseServiceHost(ILog log,
            IServiceLocator serviceLocator)
        {
            this.log = log;
            this.serviceLocator = serviceLocator;
            services = new List<IService>();
        }

        protected async Task CommandReceived(ICommand cmd)
        {
            bool canProcess = false;
            foreach (var service in services)
            {
                if (service.CanProcess(cmd))
                {
                    canProcess = true;
                    try
                    {
                        await service.Process(cmd);
                        await OnCommandProcessed(cmd);
                    }
                    catch (Exception ex)
                    {
                        log.LogException($"Error while processing command {cmd}",ex);
                        await OnCommandError(cmd);
                    }
                    
                }
            }

            if (!canProcess)
            {
                throw new UnknownCommandException($"Unable to process command {cmd}, as it's unknown");
            }
        }

        protected abstract Task OnCommandProcessed(ICommand cmd);

        protected abstract Task OnCommandError(ICommand cmd);

        
        public void Register<T>() where T : IService
        {
            services.Add(serviceLocator.ResolveService<T>());
        }
    }
}
