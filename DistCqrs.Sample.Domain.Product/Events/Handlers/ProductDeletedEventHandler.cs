using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Sample.Domain.Product.Events.Handlers
{
    public class
        ProductDeletedEventHandler : IEventHandler<Product, ProductDeletedEvent>
    {
        public async Task Apply(Product root, ProductDeletedEvent evt)
        {
            root.IsDeleted = true;
            await Task.CompletedTask;
        }
    }
}