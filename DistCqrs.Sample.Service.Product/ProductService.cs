using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.Services;
using DistCqrs.Core.Services.Impl;

namespace DistCqrs.Sample.Service.Product
{
    [ServiceRegistration(ServiceRegistrationType.Scope)]
    public class ProductService : BaseService
    {
        public ProductService(ILog log,
            IServiceLocator serviceLocator,
            ICommandProcessor commandProcessor) : base(log,
                serviceLocator,
                commandProcessor)
        {
        }

        protected override IList<string> GetSubscriptionBusIds()
        {
            return new List<string>() { Constants.BusId };
        }

        protected override Task OnCommandProcessed(ICommand cmd)
        {
            return Task.FromResult(0);
        }

        protected override Task OnCommandError(ICommand cmd)
        {
            return Task.FromResult(0);
        }
    }
}
