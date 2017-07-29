using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Command
{
    public interface ICommand
    {
        Guid RootId { get; }

        Guid CommandId { get; }
    }
}