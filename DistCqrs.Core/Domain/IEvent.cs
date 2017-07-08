using System;

namespace DistCqrs.Core.Domain
{
    public interface IEvent
    {
        Guid RootId { get; }
    }
}
