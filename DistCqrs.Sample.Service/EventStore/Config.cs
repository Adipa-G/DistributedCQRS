using System.IO;
using Microsoft.Extensions.Configuration;

namespace DistCqrs.Sample.Service.EventStore
{
    public class Config
    {
        private const string ConnectionStringKey = "ConnectionString";

        private static readonly IConfigurationRoot configuration; 

        static Config() 
        {
            if (configuration != null)
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
        }

        public static string ConnectionString => configuration[ConnectionStringKey];
    }
}
