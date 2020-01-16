using AbstractCqrs.Core.Services;

namespace AbstractCqrs.Core.Resolve
{
    public interface IServiceLocator
    {
        IScope CreateScope();

        IBus ResolveBus(string busId);

        void Register(IBus bus);
    }
}