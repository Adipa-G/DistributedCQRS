using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Interfaces.Domain;

namespace DistCqrs.Interfaces.EventStore
{
    public interface IEventStore
    {
        Task SaveEvents(IList<IEvent> events);

        Task<IList<IEvent>> GetEvents(Guid rootId);
    }
}
