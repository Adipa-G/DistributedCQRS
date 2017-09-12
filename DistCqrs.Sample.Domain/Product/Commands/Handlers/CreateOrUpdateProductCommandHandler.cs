using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;
using DistCqrs.Sample.Domain.Product.Events;

namespace DistCqrs.Sample.Domain.Product.Commands.Handlers
{
    public class CreateOrUpdateProductCommandHandler : ICommandHandler<Product,CreateOrUpdateProductCommand>
    {
        public Task<IList<IEvent<Product>>> Handle(Product root, CreateOrUpdateProductCommand cmd)
        {
            IList<IEvent<Product>> list = new List<IEvent<Product>>();
            if (root.Id == Guid.Empty)
            {
                list.Add(new ProductCreatedEvent(cmd.RootId, cmd.Code, cmd.Name,
                    cmd.UnitPrice));
            }
            else
            {
                list.Add(new ProductUpdatedEvent(cmd.RootId, cmd.Code, cmd.Name,
                    cmd.UnitPrice));
            }
            return Task.FromResult(list);
        }
    }
}
