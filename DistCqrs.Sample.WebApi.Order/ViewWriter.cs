using System.Threading.Tasks;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.View;
using DistCqrs.Sample.Service.Order.View;
using Microsoft.EntityFrameworkCore;

namespace DistCqrs.Sample.WebApi.Order
{
    [ServiceRegistration(ServiceRegistrationType.Scope)]
    public class ViewWriter : IViewWriter
    {
        public async Task UpdateView(IRoot root)
        {
            using (var context = new OrderDbContext())
            {
                var set = context.Set<Domain.Order.Order>();

                var existing =
                    await set.SingleOrDefaultAsync(p => p.Id == root.Id);
                if (existing != null)
                {
                    set.Remove(existing);
                    await context.SaveChangesAsync();
                }

                await context.Set<Domain.Order.Order>()
                    .AddAsync((Domain.Order.Order) root);
                await context.SaveChangesAsync();
            }
        }
    }
}