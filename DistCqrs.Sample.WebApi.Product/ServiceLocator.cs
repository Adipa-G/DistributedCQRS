using System;
using DistCqrs.Core.Resolve;
using DistCqrs.Sample.Service.Resolve;
using Microsoft.Extensions.DependencyInjection;

namespace DistCqrs.Sample.WebApi.Product
{
    [ServiceRegistration(ServiceRegistrationType.Singleton)]
    public class ServiceLocator : BaseServiceLocator 
    {
        private IServiceProvider serviceProvider;

        public void Init(IServiceProvider sp)
        {
            this.serviceProvider = sp;
        }

        protected override object Resolve(Type @interface)
        {
            return serviceProvider.GetService(@interface);
        }
    }
}
