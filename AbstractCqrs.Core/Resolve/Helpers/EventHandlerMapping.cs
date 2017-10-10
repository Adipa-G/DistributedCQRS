using System;

namespace AbstractCqrs.Core.Resolve.Helpers
{
    public class EventHandlerMapping
    {
        public EventHandlerMapping(Type entityType, Type eventType,
            Type eventHandlerType)
        {
            EventType = eventType;
            EntityType = entityType;
            EventHandlerType = eventHandlerType;
        }

        public Type EntityType { get; }

        public Type EventType { get; }

        public Type EventHandlerType { get; }
    }
}