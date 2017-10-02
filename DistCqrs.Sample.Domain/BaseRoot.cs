using System;
using DistCqrs.Core.Domain;

namespace DistCqrs.Sample.Domain
{
    public abstract class BaseRoot : IRoot
    {
        public bool IsDeleted { get; set; }
        public Guid Id { get; set; }
    }
}