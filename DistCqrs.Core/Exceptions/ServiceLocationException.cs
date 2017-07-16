using System;

namespace DistCqrs.Core.Exceptions
{
    public class ServiceLocationException : Exception
    {
        public ServiceLocationException(string message) : base(message)
        {
        }
    }
}