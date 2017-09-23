using System;
using DistCqrs.Core.Command;

namespace DistCqrs.Sample.Domain
{
    public abstract class BaseCommand : ICommand
    {
        public Guid RootId { get; }

        protected BaseCommand(Guid rootId)
        {
            RootId = rootId;
        }
    }
}