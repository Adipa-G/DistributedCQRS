using System;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class
        AccountCreatedEventHandler : IEventHandler<Account, AccountCreatedEvent>
    {
        public Task Apply(Account root, AccountCreatedEvent evt)
        {
            root.Id = evt.RootId;
            return Task.Delay(0);
        }
    }
}