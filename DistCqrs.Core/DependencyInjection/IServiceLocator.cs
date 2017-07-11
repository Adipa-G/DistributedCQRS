using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Services;

namespace DistCqrs.Core.DependencyInjection
{
    public interface IServiceLocator
    {
        ICommandHandler<TCmd> ResolveCommandHandler<TCmd>(TCmd cmd)
            where TCmd:ICommand;
            
        IEventHandler<TRoot, TEvent> ResolveEventHandler<TRoot, TEvent>(TRoot root, TEvent evt) 
            where TRoot : IRoot 
            where TEvent : IEvent;

        IService<T> ResolveService<T>()
            where T : ICommand;
    }
}
