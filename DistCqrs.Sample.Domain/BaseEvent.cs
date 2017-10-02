using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Sample.Domain
{
    public abstract class BaseEvent<T> : IEvent<T> where T : IRoot
    {
        protected BaseEvent(Guid rootId)
        {
            RootId = rootId;
        }

        public Guid RootId { get; }
    }
}