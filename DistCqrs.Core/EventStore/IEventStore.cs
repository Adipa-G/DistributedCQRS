using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.EventStore
{
    public interface IEventStore
    {
        Task<Type> GetRootType(Guid rootId);

        Task SaveEvents<TRoot>(IList<IEvent<TRoot>> events)
            where TRoot : IRoot;

        Task<IList<IEvent<TRoot>>> GetEvents<TRoot>(Guid rootId)
            where TRoot : IRoot;
    }
}