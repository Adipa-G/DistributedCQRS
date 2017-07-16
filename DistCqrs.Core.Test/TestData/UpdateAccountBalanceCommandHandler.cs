using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class UpdateAccountBalanceCommandHandler : ICommandHandler<Account,UpdateAccountBalanceCommand>
    {
        public Task<IList<IEvent<Account>>> Handle(Account root, UpdateAccountBalanceCommand cmd)
        {
            IList<IEvent<Account>> list = new List<IEvent<Account>>();
            list.Add(
                new AccountBalanceUpdatedEvent()
                {
                    RootId = cmd.RootId,
                    Change = cmd.Change
                });
            return Task.FromResult(list);
        }
    }
}