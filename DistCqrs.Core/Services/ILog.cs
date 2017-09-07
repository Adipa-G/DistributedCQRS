using System;

namespace DistCqrs.Core.Services
{
    public interface ILog
    {
        void LogError(string message);

        void LogException(string message, Exception ex);
    }
}