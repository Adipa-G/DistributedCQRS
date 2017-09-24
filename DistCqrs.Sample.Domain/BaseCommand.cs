using System;
using DistCqrs.Core.Command;
using DistCqrs.Core.Services;

namespace DistCqrs.Sample.Domain
{
    public abstract class BaseCommand : ICommand, IBusMessage
    {
        public Guid RootId { get; }

        protected BaseCommand(Guid rootId)
        {
            RootId = rootId;
        }
    }
}