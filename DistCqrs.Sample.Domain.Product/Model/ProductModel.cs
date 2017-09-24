using System;

namespace DistCqrs.Sample.Domain.Product.Model
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double UnitPrice { get; set; }
    }
}
