using System;

namespace Interfaces.Command
{
    public interface ICommand
    {
        Guid RootId { get; }

        Guid CorrelationId { get; }
    }
}
