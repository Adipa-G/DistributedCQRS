using System;
using DistCqrs.Core.Resolve;
using Serilog;

namespace DistCqrs.Sample.Service.Log
{
    [ServiceRegistration(ServiceRegistrationType.Singleton)]
    public class Log : ILogStart
    {
        public void Init()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Is(Config.LogLevel)
                .WriteTo.File(Config.LogFilePath)
                .CreateLogger();
        }

        public void LogVerbose(string message)
        {
            Serilog.Log.Verbose(message);
        }

        public void LogDebug(string message)
        {
            Serilog.Log.Debug(message);
        }

        public void LogInformation(string message)
        {
            Serilog.Log.Information(message);
        }

        public void LogError(string message)
        {
            Serilog.Log.Error(message);
        }

        public void LogException(string message, Exception ex)
        {
            Serilog.Log.Error(ex, message);
        }
    }
}