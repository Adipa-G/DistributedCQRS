using DistCqrs.Interfaces.Command;
using DistCqrs.Interfaces.Domain;

namespace DistCqrs.Interfaces.Services
{
    public interface IServiceLocator
    {
        ICommandHandler<TCmd> ResolveCommandHandler<TCmd>(TCmd cmd) 
            where TCmd : ICommand;

        IEventHandler<TRoot, TEvent> ResolveEventHandler<TRoot, TEvent>(TRoot root, TEvent evt) 
            where TRoot : IRoot 
            where TEvent : IEvent;
    }
}
