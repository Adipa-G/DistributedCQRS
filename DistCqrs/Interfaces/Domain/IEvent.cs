using System;

namespace DistCqrs.Interfaces.Domain
{
    public interface IEvent
    {
        Guid RootId { get; }
    }
}
