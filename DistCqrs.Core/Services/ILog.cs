using System;

namespace DistCqrs.Core.Services
{
    public interface ILog
    {
        void LogException(string message, Exception ex);
    }
}