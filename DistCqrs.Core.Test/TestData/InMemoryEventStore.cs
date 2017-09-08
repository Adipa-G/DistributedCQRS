using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;
using DistCqrs.Core.EventStore;
using DistCqrs.Core.EventStore.Impl;
using Newtonsoft.Json;

namespace DistCqrs.Core.Test.TestData
{
    public class InMemoryEventStore : BaseEventStore
    {
        public List<IEventRecord> EventRecords { get; }

        public InMemoryEventStore()
        {
            EventRecords = new List<IEventRecord>();
        }

        protected override IEventRecord Create()
        {
            return new EventRecord();
        }

        protected override string Serialize<TRoot>(IEvent<TRoot> evt)
        {
            var settings = new JsonSerializerSettings()
                           {
                               TypeNameHandling = TypeNameHandling.All
                           };
            return JsonConvert.SerializeObject(evt, settings);
        }

        protected override IEvent<TRoot> DeSerialize<TRoot>(string data)
        {
            var settings = new JsonSerializerSettings()
                           {
                               TypeNameHandling = TypeNameHandling.All
                           };
            return (IEvent<TRoot>) JsonConvert.DeserializeObject(data, settings);
        }

        protected override async Task Save(IList<IEventRecord> records)
        {
            EventRecords.AddRange(records);
            await Task.Delay(0);
        }

        protected override async Task<IList<IEventRecord>> Load(Guid rootId)
        {
            IList<IEventRecord> events = EventRecords.Where(er => er.RootId == rootId).ToList();
            var result = await Task.FromResult(events);
            return result;
        }

        public override async Task<Type> GetRootType(Guid rootId)
        {
            var result = await Task.FromResult(typeof(Account));
            return result;
        }
    }
}
