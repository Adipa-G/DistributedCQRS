using System;

namespace AbstractCqrs.Core.Resolve.Helpers
{
    public class CommandToEntityMapping
    {
        public CommandToEntityMapping(Type commandType, Type entityType)
        {
            EntityType = entityType;
            CommandType = commandType;
        }

        public Type CommandType { get; }

        public Type EntityType { get; }
    }
}