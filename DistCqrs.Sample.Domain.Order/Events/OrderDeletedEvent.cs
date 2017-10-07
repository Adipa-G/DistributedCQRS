using System;

namespace DistCqrs.Sample.Domain.Order.Events
{
    public class OrderDeletedEvent : BaseEvent<Order>
    {
        public OrderDeletedEvent(Guid rootId) : base(rootId)
        {
        }
    }
}
