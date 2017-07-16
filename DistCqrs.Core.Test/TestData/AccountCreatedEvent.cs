using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class AccountCreatedEvent : IEvent<Account>
    {
        public Guid RootId { get; set; }
    }
}