using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.EventStore.Impl
{
    public abstract class BaseEventStore : IEventStore
    {
        protected abstract IEventRecord Create();

        protected abstract string Serialize(IEvent evt);

        protected abstract IEvent DeSerialize(string data);

        protected abstract Task Save(IList<IEventRecord> records);

        protected abstract Task<IList<IEventRecord>> Load(Guid rootId);

        public async Task SaveEvents(IList<IEvent> events)
        {
            var eventRecords = new List<IEventRecord>();
            foreach (var evt in events)
            {
                var record = Create();
                record.RootId = evt.RootId;
                record.EventTimestamp = DateTime.UtcNow;
                record.Data = Serialize(evt);
                eventRecords.Add(record);
            }
            await Save(eventRecords);
        }

        public async Task<IList<IEvent>> GetEvents(Guid rootId)
        {
            var events = new List<IEvent>();
            var eventRecords = await Load(rootId);
            var sortedRecords = eventRecords.OrderBy(r => r.EventTimestamp);

            foreach (var record in sortedRecords)
            {
                events.Add(DeSerialize(record.Data));      
            }

            return events;
        }
    }
}
