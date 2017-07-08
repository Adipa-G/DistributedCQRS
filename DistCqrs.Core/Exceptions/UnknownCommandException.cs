using System;

namespace DistCqrs.Core.Exceptions
{
    public class UnknownCommandException : Exception
    {
        public UnknownCommandException(string message) : base(message)
        {
        }
    }
}
