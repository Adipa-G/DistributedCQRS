using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DistCqrs.Sample.Service.Order.View.Mappings
{
    public class OrderMapping : IMapping<Domain.Order.Order>
    {
        public void Map(EntityTypeBuilder<Domain.Order.Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Items)
                .WithOne(e => e.Order)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}