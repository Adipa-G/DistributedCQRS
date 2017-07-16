using System;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Test.TestData
{
    public class CreateAccountCommand : ICommand<Account>
    {
        public Guid RootId { get; set; }

        public Guid CommandId { get; set; }
    }
}