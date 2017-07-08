using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class AccountCreatedEventHandler : IEventHandler<Account,AccountCreatedEvent>
    {
        public Account Apply(Account root, AccountCreatedEvent evt)
        {
            return new Account() {Id = evt.RootId};
        }
    }
}
