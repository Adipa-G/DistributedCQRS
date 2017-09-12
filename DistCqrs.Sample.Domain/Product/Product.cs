namespace DistCqrs.Sample.Domain.Product
{
    public class Product : BaseRoot
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public double UnitPrice { get; set; }
    }
}
