using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbstractCqrs.Sample.Service.Product.View.Mappings
{
    internal interface IMapping<T> where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}