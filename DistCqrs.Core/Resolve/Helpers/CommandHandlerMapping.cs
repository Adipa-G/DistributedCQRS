using System;

namespace DistCqrs.Core.Resolve.Helpers
{
    public class CommandHandlerMapping
    {
        public CommandHandlerMapping(Type entityType, Type commandType,
            Type commandHandlerType)
        {
            CommandType = commandType;
            EntityType = entityType;
            CommandHandlerType = commandHandlerType;
        }

        public Type EntityType { get; }

        public Type CommandType { get; }

        public Type CommandHandlerType { get; }
    }
}