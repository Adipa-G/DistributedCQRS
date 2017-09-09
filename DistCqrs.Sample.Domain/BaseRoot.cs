using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Sample.Domain
{
    public abstract class BaseRoot : IRoot
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
