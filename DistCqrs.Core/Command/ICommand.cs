using System;

namespace DistCqrs.Core.Command
{
    public interface ICommand
    {
        Guid RootId { get; }
    }
}