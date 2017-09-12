using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DistCqrs.Sample.Service.View.Mappings
{
    interface IMapping<T> where T : class
    {
        void Map(EntityTypeBuilder<T> builder) ;
    }
}
