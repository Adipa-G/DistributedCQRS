using DistCqrs.Sample.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DistCqrs.Sample.Service.View.Mappings
{
    public class ProductMapping  : IMapping<Product>
    {
        public void Map(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(e => e.Id);
        }
    }
}
