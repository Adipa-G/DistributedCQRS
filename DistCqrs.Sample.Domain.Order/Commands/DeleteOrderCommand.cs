using System;

namespace DistCqrs.Sample.Domain.Order.Commands
{
    public class DeleteOrderCommand : BaseCommand
    {
        public DeleteOrderCommand(Guid rootId) : base(rootId)
        {
        }
    }
}
