using AbstractCqrs.Core.Command;

namespace AbstractCqrs.Core.Resolve
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}