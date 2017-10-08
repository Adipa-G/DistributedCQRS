using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Sample.Domain.Order.Events;

namespace DistCqrs.Sample.Domain.Order.Commands.Handlers
{
    public class UpdateOrderItemCommandHandler : ICommandHandler<Order,
        UpdateOrderItemCommand>
    {
        public Task<IList<IEvent<Order>>> Handle(Order root,
            UpdateOrderItemCommand cmd)
        {
            IList<IEvent<Order>> list = new List<IEvent<Order>>();
            if (root.Id == Guid.Empty)
            {
                throw new DomainException($"Order {root.Id} does not exists.");
            }

            if (root.Items.All(i => i.Id != cmd.OrderItemId))
            {
                throw new DomainException($"Order item {cmd.OrderItemId} does not exists.");
            }

            if (root.IsDeleted)
            {
                throw new DomainException($"Order {root.Id} is deleted.");
            }

            list.Add(new OrderItemUpdatedEvent(cmd.RootId, 
                cmd.OrderItemId,
                cmd.Ordinal,
                cmd.ProductText,
                cmd.Qty,
                cmd.Amount));
            return Task.FromResult(list);
        }
    }
}