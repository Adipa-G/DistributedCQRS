using System;
using DistCqrs.Core.Resolve;
using DistCqrs.Sample.Service.Resolve;

namespace DistCqrs.Sample.WebApi.Order
{
    [ServiceRegistration(ServiceRegistrationType.Singleton)]
    public class ServiceLocator : BaseServiceLocator
    {
        private IServiceProvider serviceProvider;

        public void Init(IServiceProvider sp)
        {
            serviceProvider = sp;
        }

        protected override object Resolve(Type @interface)
        {
            return serviceProvider.GetService(@interface);
        }
    }
}