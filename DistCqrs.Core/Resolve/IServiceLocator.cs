using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Services;

namespace DistCqrs.Core.Resolve
{
    public interface IServiceLocator
    {
        T Resolve<T>();

        IBus ResolveBus(string busId);

        IService ResolveService(string serviceId);

        ICommandHandler<TRoot, TCmd> ResolveCommandHandler<TRoot, TCmd>()
            where TRoot : IRoot, new()
            where TCmd : ICommand;

        IEventHandler<TRoot, TEvent> ResolveEventHandler<TRoot, TEvent>()
            where TRoot : IRoot, new()
            where TEvent : IEvent<TRoot>;

        void Register(IBus bus);

        void Register(IService service);
    }
}