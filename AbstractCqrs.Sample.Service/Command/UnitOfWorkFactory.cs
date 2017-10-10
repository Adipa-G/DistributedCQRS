using AbstractCqrs.Core.Command;
using AbstractCqrs.Core.Resolve;

namespace AbstractCqrs.Sample.Service.Command
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