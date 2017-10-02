using System.IO;
using Microsoft.Extensions.Configuration;

namespace DistCqrs.Sample.Service
{
    public class Config
    {
        private const string ConnectionStringKey = "ConnectionString";
        private const string LogFilePathKey = "LogFilePath";

        private static IConfigurationRoot configuration;

        public static string ConnectionString
        {
            get
            {
                Init();
                return configuration[ConnectionStringKey];
            }
        }

        public static string LogFilePath
        {
            get
            {
                Init();
                return configuration[LogFilePathKey];
            }
        }

        private static void Init()
        {
            if (configuration == null)
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}