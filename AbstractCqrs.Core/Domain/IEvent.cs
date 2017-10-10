using System;

namespace AbstractCqrs.Core.Domain
{
    public interface IEvent<TRoot>
        where TRoot : IRoot
    {
        Guid RootId { get; }
    }
}