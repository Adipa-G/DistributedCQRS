using System;

namespace AbstractCqrs.Core.Services
{
    public interface ILog
    {
        void LogVerbose(string message);

        void LogDebug(string message);

        void LogInformation(string message);

        void LogError(string message);

        void LogException(string message, Exception ex);
    }
}