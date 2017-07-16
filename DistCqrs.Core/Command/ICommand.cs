using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Command
{
    public interface ICommand<TRoot>
        where TRoot : IRoot
    {
        Guid RootId { get; }

        Guid CommandId { get; }
    }
}