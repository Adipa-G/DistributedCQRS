using System;

namespace AbstractCqrs.Core.Exceptions
{
    public class UnknownCommandException : Exception
    {
        public UnknownCommandException(string message) : base(message)
        {
        }
    }
}