using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Test.TestData
{
    public class Account : IRoot
    {
        public double Balance { get; set; }
        public Guid Id { get; set; }
    }
}