using System;
using System.Threading.Tasks;
using DistCqrs.Sample.Domain.Order.Model;

namespace DistCqrs.Sample.Domain.Order.View
{
    public interface IOrderView
    {
        Task<OrderModel> GetById(Guid id);
    }
}