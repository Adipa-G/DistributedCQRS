using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.EventStore
{
    public interface IEventStore
    {
        Task SaveEvents(IList<IEvent> events);

        Task<IList<IEvent>> GetEvents(Guid rootId);
    }
}
