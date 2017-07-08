namespace DistCqrs.Core.Domain
{
    public interface IEventHandler<TRoot,TEvent> 
        where TRoot:IRoot 
        where TEvent:IEvent
    {
        TRoot Apply(TRoot root, TEvent evt);
    }
}
