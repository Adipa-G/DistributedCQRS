using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class AccountCreatedEvent : IEvent
    {
        public Guid RootId { get; set; }
    }
}
