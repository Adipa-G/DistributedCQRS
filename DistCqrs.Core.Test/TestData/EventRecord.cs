using System;
using DistCqrs.Core.EventStore;

namespace DistCqrs.Core.Test.TestData
{
    public class EventRecord : IEventRecord
    {
        public Guid RootId { get; set; }

        public DateTime EventTimestamp { get; set; }

        public string Data { get; set; }
    }
}