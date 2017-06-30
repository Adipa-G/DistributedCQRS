using System;

namespace DistCqrs.Interfaces.EventStore
{
    public interface IEventRecord
    {
        Guid RootId { get; set; }

        DateTime EventTimestamp { get; set; }

        string Data { get; set; }
    }
}
