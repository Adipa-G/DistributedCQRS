using System.Threading.Tasks;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.View;
using DistCqrs.Sample.Domain.Order;
using DistCqrs.Sample.Domain.Product;
using DistCqrs.Sample.Service.Order.View;
using DistCqrs.Sample.Service.Product.View;
using Microsoft.EntityFrameworkCore;

namespace DistCqrs.Sample.WebApi.Combined
{
    [ServiceRegistration(ServiceRegistrationType.Scope)]
    public class ViewWriter : IViewWriter
    {
        public async Task UpdateView(IRoot root)
        {
            await IfOrder(root);
            await IfProduct(root);
        }

        private static async Task IfOrder(IRoot root)
        {
            var order = root as Order;
            if (order != null)
            {
                using (var context = new OrderDbContext())
                {
                    var set = context.Set<Order>();

                    var existing =
                        await set.SingleOrDefaultAsync(p => p.Id == order.Id);
                    if (existing != null)
                    {
                        set.Remove(existing);
                        await context.SaveChangesAsync();
                    }

                    await context.Set<Order>()
                        .AddAsync(order);
                    await context.SaveChangesAsync();
                }
            }
        }

        private static async Task IfProduct(IRoot root)
        {
            var product = root as Product;
            if (product != null)
            {
                using (var context = new ProductDbContext())
                {
                    var set = context.Set<Product>();

                    var existing =
                        await set.SingleOrDefaultAsync(p => p.Id == root.Id);
                    if (existing != null)
                    {
                        set.Remove(existing);
                        await context.SaveChangesAsync();
                    }

                    await context.Set<Product>()
                        .AddAsync((Product)root);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}