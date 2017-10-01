using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.View;
using DistCqrs.Sample.Domain.Product.View;
using DistCqrs.Sample.Service.Product.View;
using Microsoft.EntityFrameworkCore;

namespace DistCqrs.Sample.WebApi.Product
{
    [ServiceRegistration(ServiceRegistrationType.Scope)]
    public class ViewWriter : IViewWriter
    {
        public async Task UpdateView(IRoot root)
        {
            using (var context = new ProductDbContext())
            {
                var set = context.Set<Domain.Product.Product>();

                var existing = await set.SingleOrDefaultAsync(p => p.Id == root.Id);
                if (existing != null)
                {
                    set.Remove(existing);
                    await context.SaveChangesAsync();
                }

                await context.Set<Domain.Product.Product>()
                    .AddAsync((Domain.Product.Product) root);
                await context.SaveChangesAsync();
            }
        }
    }
}
