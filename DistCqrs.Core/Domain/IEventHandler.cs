using System.Threading.Tasks;

namespace DistCqrs.Core.Domain
{
    public interface IEventHandler<TRoot, in TEvent>
        where TRoot : IRoot
        where TEvent : IEvent<TRoot>
    {
        Task Apply(TRoot root, TEvent evt);
    }
}