using System;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Resolve;

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