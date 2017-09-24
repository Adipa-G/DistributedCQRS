using System;
using System.Threading.Tasks;
using DistCqrs.Sample.Domain.Product.Model;

namespace DistCqrs.Sample.Domain.Product.View
{
    public interface IProductView
    {
        Task<ProductModel> GetById(Guid id);
    }
}
