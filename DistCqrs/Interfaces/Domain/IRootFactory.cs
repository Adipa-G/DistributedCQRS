namespace DistCqrs.Interfaces.Domain
{
    public interface IRootFactory
    {
        IRoot Create(IEvent firstEvent);
    }
}
