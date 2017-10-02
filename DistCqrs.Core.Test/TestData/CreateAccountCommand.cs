using System;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Test.TestData
{
    public class CreateAccountCommand : ICommand
    {
        public Guid CommandId { get; set; }
        public Guid RootId { get; set; }
    }
}