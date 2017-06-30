using System;
using DistCqrs.Interfaces.Domain;

namespace DistCqrs.Interfaces.Command
{
    public interface ICommand 
    {
        Guid RootId { get; }

        Guid CorrelationId { get; }
    }
}
