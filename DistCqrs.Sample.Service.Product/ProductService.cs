using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.Services;
using DistCqrs.Core.Services.Impl;

namespace DistCqrs.Sample.Service.Product
{
    [ServiceRegistration(ServiceRegistrationType.Singleton)]
    public class ProductService : BaseService, IProductService
    {
        public ProductService(ILog log,
            IServiceLocator serviceLocator) : base(log,
            serviceLocator)
        {
            Id = Constants.ServiceId;
        }

        protected override IList<string> GetSubscriptionBusIds()
        {
            return new List<string> {Constants.BusId};
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