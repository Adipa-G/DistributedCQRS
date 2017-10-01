using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DistCqrs.Sample.Domain.Product.Model;
using Newtonsoft.Json;

namespace DistCqrs.Sample.Performance.PerformanceTest
{
    public class ProductTest
    {
        private string urlBase;
        private const int threads = 50;
        
        public ProductTest(string urlBase)
        {
            this.urlBase = urlBase;
        }

        public void Execute()
        {
            var threadList = new List<Thread>();

            for (int i = 0; i < threads; i++)
            {
                var thread = new Thread(RunInThread);
                thread.Start();
            }
        }

        public void RunInThread()
        {
            var client = new HttpClient();
            var random = new Random();

            for (int i = 0; i < 100000; i++)
            {
                var sameRepeat = random.Next(1, 10);
                var id = Guid.NewGuid();

                for (int j = 0; j < sameRepeat; j++)
                {
                    var product = new ProductModel()
                                  {
                                      Id = id,
                                      Code = "Code " + i,
                                      Name = "Name " + i,
                                      UnitPrice = i
                                  };

                    if (j == 0)
                    {
                        client.PostAsync($"{urlBase}/api/product",
                            new StringContent(JsonConvert.SerializeObject(product),
                                Encoding.UTF8, "application/json")).Wait();
                    }
                    else if (j < 5)
                    {
                        client.PutAsync($"{urlBase}/api/product/{id}",
                            new StringContent(JsonConvert.SerializeObject(product),
                                Encoding.UTF8, "application/json")).Wait();
                    }
                    else
                    {
                        client.DeleteAsync($"{urlBase}/api/product/{id}")
                            .Wait();
                    }
                }
            }
        }
    }
}
