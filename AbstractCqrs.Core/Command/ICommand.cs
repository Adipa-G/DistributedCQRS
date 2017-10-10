using System;

namespace AbstractCqrs.Core.Command
{
    public interface ICommand
    {
        Guid RootId { get; }
    }
}