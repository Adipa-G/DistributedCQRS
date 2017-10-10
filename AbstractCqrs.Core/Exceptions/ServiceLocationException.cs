using System;

namespace AbstractCqrs.Core.Exceptions
{
    public class ServiceLocationException : Exception
    {
        public ServiceLocationException(string message) : base(message)
        {
        }
    }
}