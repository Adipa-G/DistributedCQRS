using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand>
    {
        public Task<IList<IEvent>> Handle(IRoot root, CreateAccountCommand cmd)
        {
            IList<IEvent> list = new List<IEvent>();
            list.Add(new AccountCreatedEvent() {RootId = cmd.RootId});
            return Task.FromResult(list);
        }
    }
}
