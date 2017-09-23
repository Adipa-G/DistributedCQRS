using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Sample.Domain
{
    public abstract class BaseEvent<T> : IEvent<T> where T : IRoot
    {
        public Guid RootId { get; }

        protected BaseEvent(Guid rootId)
        {
            RootId = rootId;
        }
    }
}