using System;
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
        
        protected BaseServiceHost(ILog log,
            IServiceLocator serviceLocator)
        {
            this.log = log;
            this.serviceLocator = serviceLocator;
        }

        public async Task CommandReceived<TCmd>(TCmd cmd) 
            where TCmd:ICommand
        {
            var service = serviceLocator.ResolveService<TCmd>();
            if (service == null)
            {
                throw new UnknownCommandException($"Unable to process command {cmd}, as it's unknown");
            }

            try
            {
                await service.Process(cmd); ;
                await OnCommandProcessed(cmd);
            }
            catch (Exception ex)
            {
                log.LogException($"Error while processing command {cmd}", ex);
                await OnCommandError(cmd);
            }
        }

        protected abstract Task OnCommandProcessed(ICommand cmd);

        protected abstract Task OnCommandError(ICommand cmd);
    }
}
