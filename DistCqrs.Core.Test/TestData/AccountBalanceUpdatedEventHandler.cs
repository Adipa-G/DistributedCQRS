using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class AccountBalanceUpdatedEventHandler : IEventHandler<Account,AccountBalanceUpdatedEvent>
    {
        public Account Apply(Account root, AccountBalanceUpdatedEvent evt)
        {
            root.Balance += evt.Change;
            return root;
        }
    }
}
