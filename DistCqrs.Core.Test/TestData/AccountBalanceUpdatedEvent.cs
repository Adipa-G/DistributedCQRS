using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class AccountBalanceUpdatedEvent : IEvent<Account>
    {
        public double Change { get; set; }
        public Guid RootId { get; set; }
    }
}