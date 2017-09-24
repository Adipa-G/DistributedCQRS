using System;

namespace DistCqrs.Sample.Domain.Product.Events
{
    public class ProductDeletedEvent : BaseEvent<Product>
    {
        public ProductDeletedEvent(Guid rootId) : base(rootId)
        {
        }
    }
}