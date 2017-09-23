using System;

namespace DistCqrs.Sample.Domain.Product.Events
{
    public class ProductUpdatedEvent : BaseEvent<Product>
    {
        public ProductUpdatedEvent(Guid rootId,
            string code,
            string name,
            double unitPrice) : base(rootId)
        {
            Code = code;
            Name = name;
            UnitPrice = unitPrice;
        }

        public string Code { get; }

        public string Name { get; }

        public double UnitPrice { get; }
    }
}