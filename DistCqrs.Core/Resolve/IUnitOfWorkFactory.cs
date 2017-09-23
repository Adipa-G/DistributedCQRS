using DistCqrs.Core.Command;

namespace DistCqrs.Core.Resolve
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}