using System;

namespace DistCqrs.Core.Exceptions
{
    public class BusRegistrationException : Exception
    {
        public BusRegistrationException(string message) : base(message)
        {
        }
    }
}