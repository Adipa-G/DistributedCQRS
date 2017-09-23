using System;
using DistCqrs.Core.EventStore;

namespace DistCqrs.Sample.Service.EventStore
{
    public class EventRecord : IEventRecord
    {
        public Guid RootId { get; set; }

        public DateTime EventTimestamp { get; set; }

        public string Data { get; set; }
    }
}