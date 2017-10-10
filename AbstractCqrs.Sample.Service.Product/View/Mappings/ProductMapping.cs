using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbstractCqrs.Sample.Service.Product.View.Mappings
{
    public class ProductMapping : IMapping<Domain.Product.Product>
    {
        public void Map(EntityTypeBuilder<Domain.Product.Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(e => e.Id);
        }
    }
}