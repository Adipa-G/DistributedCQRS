using System;

namespace AbstractCqrs.Core.Resolve
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceRegistrationAttribute : Attribute
    {
        public ServiceRegistrationAttribute(
            ServiceRegistrationType registrationType)
        {
            RegistrationType = registrationType;
        }

        public ServiceRegistrationType RegistrationType { get; }
    }
}