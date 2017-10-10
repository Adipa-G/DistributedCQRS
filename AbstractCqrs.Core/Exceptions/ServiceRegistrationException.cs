using System;

namespace AbstractCqrs.Core.Exceptions
{
    public class ServiceRegistrationException : Exception
    {
        public ServiceRegistrationException(string message) : base(message)
        {
        }
    }
}