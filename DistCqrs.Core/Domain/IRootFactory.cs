namespace DistCqrs.Core.Domain
{
    public interface IRootFactory
    {
        IRoot Create(IEvent firstEvent);
    }
}
