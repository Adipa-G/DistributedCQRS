using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Resolve;

namespace DistCqrs.Core.Services.Impl
{
    public abstract class BaseServiceHost : IServiceHost
    {
        private readonly IServiceLocator serviceLocator;

        protected BaseServiceHost(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public void Init(string[] serviceIds)
        {
            RegisterAllBuses();

            foreach (var serviceId in serviceIds)
            {
                var service = serviceLocator.ResolveService(serviceId);
                if (service == null)
                    throw new ServiceLocationException(
                        $"Unable to resolve service {serviceId}");

                service.Init();
            }
        }

        protected abstract void RegisterAllBuses();
    }
}