using System;

namespace DistCqrs.Core.Domain
{
    public interface IEvent<TRoot>
        where TRoot : IRoot
    {
        Guid RootId { get; }
    }
}