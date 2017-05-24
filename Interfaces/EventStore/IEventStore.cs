using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.EventStore
{
    public interface IEventStore
    {
        Task SaveEvents(IList<IEventRecord> records);

        Task<IList<IEventRecord>> GetEventRecords(Guid rootId);
    }
}
