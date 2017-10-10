using AbstractCqrs.Core.Command;
using AbstractCqrs.Core.Command.Impl;
using AbstractCqrs.Core.Domain;
using AbstractCqrs.Core.Exceptions;
using AbstractCqrs.Core.Resolve;
using AbstractCqrs.Core.Resolve.Helpers;
using AbstractCqrs.Core.Services.Impl;
using AbstractCqrs.Sample.Domain.Order.View;
using AbstractCqrs.Sample.Domain.Product.View;
using AbstractCqrs.Sample.Service;
using AbstractCqrs.Sample.Service.Log;
using AbstractCqrs.Sample.Service.Order;
using AbstractCqrs.Sample.Service.Order.View;
using AbstractCqrs.Sample.Service.Product;
using AbstractCqrs.Sample.Service.Product.View;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AbstractCqrs.Sample.WebApi.Combined
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

            var productView =
                app.ApplicationServices.GetService<IProductView>() as
                    ProductDbContext;
            productView.Database.Migrate();

            var locator =
                app.ApplicationServices.GetService<IServiceLocator>() as
                    ServiceLocator;
            locator.Init(app.ApplicationServices);

            locator.Register(new InternalBus(Service.Order.Constants.BusId));
            locator.Register(new InternalBus(Service.Product.Constants.BusId));

            var orderService =
                app.ApplicationServices.GetService<IOrderService>();
            locator.Register(orderService);
            orderService.Init();

            var productService =
                app.ApplicationServices.GetService<IProductService>();
            locator.Register(productService);
            productService.Init();
        }

        private static void ConfigureIOC(IServiceCollection services)
        {
            var assemblies = new[]
                             {
                                 typeof(CommandProcessor).Assembly,
                                 typeof(Domain.Order.Order).Assembly,
                                 typeof(Domain.Product.Product).Assembly,
                                 typeof(Startup).Assembly,
                                 typeof(OrderService).Assembly,
                                 typeof(ProductService).Assembly,
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