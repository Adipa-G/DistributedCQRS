using System;
using AbstractCqrs.Core.Resolve;
using AbstractCqrs.Sample.Service.Resolve;

namespace AbstractCqrs.Sample.WebApi.Combined
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