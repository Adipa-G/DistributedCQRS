using System;

namespace DistCqrs.Core.EventStore
{
    public interface IEventRecord
    {
        Guid RootId { get; set; }

        DateTime EventTimestamp { get; set; }

        string Data { get; set; }
    }
}
