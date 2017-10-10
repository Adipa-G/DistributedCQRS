using System;

namespace AbstractCqrs.Core.Domain
{
    public interface IRoot
    {
        Guid Id { get; }
    }
}