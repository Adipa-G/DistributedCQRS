﻿using System;
using DistCqrs.Core.Command;

namespace DistCqrs.Core.Test.TestData
{
    public class UpdateAccountBalanceCommand : ICommand
    {
        public Guid CommandId { get; set; }

        public double Change { get; set; }
        public Guid RootId { get; set; }
    }
}