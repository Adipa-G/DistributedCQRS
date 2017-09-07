using System;

namespace DistCqrs.Core.Exceptions
{
    public class ServiceRegistrationException : Exception
    {
        public ServiceRegistrationException(string message) : base(message)
        {
        }
    }
}