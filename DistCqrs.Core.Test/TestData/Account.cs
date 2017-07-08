using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class Account : IRoot
    {
        public Guid Id { get; set; }

        public double Balance { get; set; }
    }
}
