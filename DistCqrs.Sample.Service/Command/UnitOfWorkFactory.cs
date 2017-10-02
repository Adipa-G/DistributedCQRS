using DistCqrs.Core.Command;
using DistCqrs.Core.Resolve;

namespace DistCqrs.Sample.Service.Command
{
    [ServiceRegistration(ServiceRegistrationType.Singleton)]
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWork();
        }
    }
}