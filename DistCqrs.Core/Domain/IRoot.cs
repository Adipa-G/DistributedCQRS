using System;

namespace DistCqrs.Core.Domain
{
    public interface IRoot
    {
        Guid Id { get; }
    }
}