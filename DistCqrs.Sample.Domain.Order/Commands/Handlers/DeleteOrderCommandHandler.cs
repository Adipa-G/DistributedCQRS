using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Sample.Domain.Order.Events;

namespace DistCqrs.Sample.Domain.Order.Commands.Handlers
{
    public class
        DeleteOrderCommandHandler : ICommandHandler<Order,DeleteOrderCommand>
    {
        public Task<IList<IEvent<Order>>> Handle(Order root,
            DeleteOrderCommand cmd)
        {
            IList<IEvent<Order>> list = new List<IEvent<Order>>();

            if (root.Id == Guid.Empty)
            {
                throw new DomainException($"Order {root.Id} does not exists.");
            }

            if (root.IsDeleted)
            {
                return Task.FromResult(list);
            }

            list.Add(new OrderDeletedEvent(cmd.RootId));
            return Task.FromResult(list);
        }
    }
}