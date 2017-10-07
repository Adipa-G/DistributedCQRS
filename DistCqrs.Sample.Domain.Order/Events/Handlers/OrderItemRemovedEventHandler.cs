﻿using System.Linq;
using System.Threading.Tasks;
using DistCqrs.Core.Domain;

namespace DistCqrs.Sample.Domain.Order.Events.Handlers
{
    public class OrderItemRemovedEventHandler : IEventHandler<Order, OrderItemRemovedEvent>
    {
        public Task Apply(Order root, OrderItemRemovedEvent evt)
        {
            var orderItem = root.Items.Single(i => i.Id == evt.OrderItemId);

            root.Items.Remove(orderItem);

            var itemsToUpdateOrdinal =
                root.Items.Where(i => i.Ordinal > orderItem.Ordinal);
            foreach (var item in itemsToUpdateOrdinal)
            {
                item.Ordinal -= 1;
            }
            root.Total = root.Items.Sum(i => i.Amount) - root.Discount;

            return Task.CompletedTask;
        }
    }
}
