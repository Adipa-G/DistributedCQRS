using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Sample.Domain.Order.Events.Handlers
{
    public class OrderDeletedEventHandler : IEventHandler<Order, OrderDeletedEvent>
    {
        public Task Apply(Order root, OrderDeletedEvent evt)
        {
            root.IsDeleted = true;

            return Task.CompletedTask;
        }
    }
}
