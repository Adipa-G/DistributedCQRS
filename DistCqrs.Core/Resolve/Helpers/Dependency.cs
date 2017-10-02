using System;

namespace DistCqrs.Core.Resolve.Helpers
{
    public class Dependency
    {
        public ServiceRegistrationType RegistrationType;

        public Dependency(Type @interface,
            Type implementation,
            ServiceRegistrationType registrationType)
        {
            Interface = @interface;
            Implementation = implementation;
            RegistrationType = registrationType;
        }

        public Type Interface { get; }

        public Type Implementation { get; }
    }
}