using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbstractCqrs.Sample.Service.Order.View.Mappings
{
    internal interface IMapping<T> where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}