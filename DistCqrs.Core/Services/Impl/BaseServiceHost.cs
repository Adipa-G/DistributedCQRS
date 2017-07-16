using System;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.DependencyInjection;
using DistCqrs.Core.Domain;
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

        public async Task CommandReceived<TRoot, TCmd>(TCmd cmd)
            where TRoot : IRoot
            where TCmd : ICommand<TRoot>
        {
            var service = serviceLocator.ResolveService<TRoot, TCmd>();
            if (service == null)
            {
                throw new UnknownCommandException(
                    $"Unable to process command {cmd}, as it's unknown");
            }

            try
            {
                await service.Process(cmd);
                await OnCommandProcessed(cmd);
            }
            catch (Exception ex)
            {
                log.LogException($"Error while processing command {cmd}", ex);
                await OnCommandError(cmd);
            }
        }

        protected abstract Task OnCommandProcessed<TRoot>(ICommand<TRoot> cmd)
            where TRoot : IRoot;

        protected abstract Task OnCommandError<TRoot>(ICommand<TRoot> cmd)
            where TRoot : IRoot;
    }
}