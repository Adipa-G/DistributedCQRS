using System;
using DistCqrs.Core.Command;
using DistCqrs.Core.Services;

namespace DistCqrs.Sample.Domain
{
    public abstract class BaseCommand : ICommand, IBusMessage
    {
        protected BaseCommand(Guid rootId)
        {
            RootId = rootId;
        }

        public Guid RootId { get; }
    }
}