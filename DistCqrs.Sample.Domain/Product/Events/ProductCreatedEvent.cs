using System;

namespace DistCqrs.Sample.Domain.Product.Events
{
    public class ProductCreatedEvent : BaseEvent<Product>
    {
        public ProductCreatedEvent(Guid rootId,
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
