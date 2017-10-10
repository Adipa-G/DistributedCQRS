using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbstractCqrs.Sample.Service.Order.View.Mappings
{
    public class OrderItemMapping : IMapping<Domain.Order.OrderItem>
    {
        public void Map(EntityTypeBuilder<Domain.Order.OrderItem> builder)
        {
            builder.ToTable("Orderitem");
            builder.HasKey(e => e.Id);
        }
    }
}