using System;

namespace DistCqrs.Interfaces.Domain
{
    public interface IRoot
    {
        Guid Id { get; }
    }
}
