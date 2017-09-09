using System.IO;
using Microsoft.Extensions.Configuration;

namespace DistCqrs.Sample.Service.EventStore
{
    public class Config
    {
        private static IConfigurationRoot configuration; 

        public Config()
        {
            if (configuration != null)
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
        }
    }
}
