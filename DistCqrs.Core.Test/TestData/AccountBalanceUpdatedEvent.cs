using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class AccountBalanceUpdatedEvent : IEvent
    {
        public Guid RootId { get; set; }
        
        public double Change { get; set; }
    }
}
