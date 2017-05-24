using Interfaces.Events;

namespace Interfaces.Domain
{
    public interface IEventHandler<TRoot,TEvent> 
        where TRoot:IRoot 
        where TEvent:IEvent
    {
        void Apply(TRoot root, TEvent evt);
    }
}
