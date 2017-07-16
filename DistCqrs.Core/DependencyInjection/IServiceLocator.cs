using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Services;

namespace DistCqrs.Core.DependencyInjection
{
    public interface IServiceLocator
    {
        ICommandHandler<TRoot, TCmd> ResolveCommandHandler<TRoot, TCmd>()
            where TRoot : IRoot
            where TCmd : ICommand<TRoot>;

        IEventHandler<TRoot, TEvent> ResolveEventHandler<TRoot, TEvent>()
            where TRoot : IRoot
            where TEvent : IEvent<TRoot>;

        IService<TRoot, TCmd> ResolveService<TRoot, TCmd>()
            where TRoot : IRoot
            where TCmd : ICommand<TRoot>;
    }
}