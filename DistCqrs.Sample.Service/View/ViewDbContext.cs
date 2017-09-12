using DistCqrs.Sample.Domain.Product;
using DistCqrs.Sample.Service.View.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DistCqrs.Sample.Service.View
{
    public class ViewDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ProductMapping().Map(modelBuilder.Entity<Product>());
        }
    }
}
