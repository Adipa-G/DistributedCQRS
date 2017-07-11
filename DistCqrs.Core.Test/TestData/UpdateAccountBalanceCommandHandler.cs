using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class UpdateAccountBalanceCommandHandler : ICommandHandler<UpdateAccountBalanceCommand>
    {
        public Task<IList<IEvent>> Handle(IRoot root, UpdateAccountBalanceCommand cmd)
        {
            IList<IEvent> list = new List<IEvent>();
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
