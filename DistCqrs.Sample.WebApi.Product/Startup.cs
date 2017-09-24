using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.Resolve.Helpers;
using DistCqrs.Core.Services.Impl;
using DistCqrs.Sample.Service;
using DistCqrs.Sample.Service.Log;
using DistCqrs.Sample.Service.Product;
using DistCqrs.Sample.Service.Resolve;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DistCqrs.Sample.WebApi.Product
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureIOC(services);

            InitLog(services);
            RegisterBuses(services);
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        private static void ConfigureIOC(IServiceCollection services)
        {
            var assemblies = new[]
                             {
                                 typeof(Startup).Assembly,
                                 typeof(ProductService).Assembly,
                                 typeof(Config).Assembly
                             };

            var allRegs = ResolveUtils.GetDependencies(assemblies);
            foreach (var reg in allRegs)
            {
                switch (reg.RegistrationType)
                {
                    case ServiceRegistrationType.Scope:
                        services.AddScoped(reg.Interface, reg.Implementation);
                        break;
                    case ServiceRegistrationType.Singleton:
                        services.AddSingleton(reg.Interface,
                            reg.Implementation);
                        break;
                    default:
                        throw new ServiceRegistrationException(
                            $"Unknown registration type {reg.RegistrationType}");
                }
            }
        }

        private static void InitLog(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var log = sp.GetService<ILogStart>();

            log.Init();
        }

        private static void RegisterBuses(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var register = sp.GetService<IServiceRegister>();

            register.Register(new InternalBus(Constants.BusId));
        }
    }
}