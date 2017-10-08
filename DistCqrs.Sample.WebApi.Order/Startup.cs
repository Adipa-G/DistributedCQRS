using DistCqrs.Core.Command;
using DistCqrs.Core.Command.Impl;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.Resolve.Helpers;
using DistCqrs.Core.Services.Impl;
using DistCqrs.Sample.Domain.Order.View;
using DistCqrs.Sample.Service;
using DistCqrs.Sample.Service.Log;
using DistCqrs.Sample.Service.Order;
using DistCqrs.Sample.Service.Order.View;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DistCqrs.Sample.WebApi.Order
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureIOC(services);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();

            var log = app.ApplicationServices.GetService<ILogStart>();
            log.Init();

            var orderView =
                app.ApplicationServices.GetService<IOrderView>() as
                    OrderDbContext;
            orderView.Database.Migrate();

            var locator =
                app.ApplicationServices.GetService<IServiceLocator>() as
                    ServiceLocator;
            locator.Init(app.ApplicationServices);
            locator.Register(new InternalBus(Constants.BusId));

            var orderService =
                app.ApplicationServices.GetService<IOrderService>();
            locator.Register(orderService);
            orderService.Init();
        }

        private static void ConfigureIOC(IServiceCollection services)
        {
            var assemblies = new[]
                             {
                                 typeof(CommandProcessor).Assembly,
                                 typeof(Domain.Order.Order).Assembly,
                                 typeof(Startup).Assembly,
                                 typeof(OrderService).Assembly,
                                 typeof(Config).Assembly
                             };

            var allRegs = ResolveUtils.GetDependencies(assemblies);
            foreach (var reg in allRegs)
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

            var commandHandlerMappings =
                ResolveUtils.GetCommandHandlerMappings(assemblies);
            foreach (var mapping in commandHandlerMappings)
            {
                var cmdHanderInterface =
                    typeof(ICommandHandler<,>).MakeGenericType(
                        mapping.EntityType, mapping.CommandType);
                services.AddScoped(cmdHanderInterface,
                    mapping.CommandHandlerType);
            }

            var eventHandlerMappings =
                ResolveUtils.GetEventHandlerMappings(assemblies);
            foreach (var mapping in eventHandlerMappings)
            {
                var eventHandlerInterface =
                    typeof(IEventHandler<,>).MakeGenericType(
                        mapping.EntityType, mapping.EventType);
                services.AddScoped(eventHandlerInterface,
                    mapping.EventHandlerType);
            }
        }
    }
}