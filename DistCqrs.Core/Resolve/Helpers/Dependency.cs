using System;

namespace DistCqrs.Core.Resolve.Helpers
{
    public class Dependency
    {
        public Dependency(Type @interface, Type implementation)
        {
            Interface = @interface;
            Implementation = implementation;
        }

        public Type Interface { get; }

        public Type Implementation { get; }
    }
}
