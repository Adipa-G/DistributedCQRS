using System;

namespace DistCqrs.Exceptions
{
    public class ServiceLocationException : Exception
    {
        public ServiceLocationException(string message) : base(message)
        {
        }
    }
}
