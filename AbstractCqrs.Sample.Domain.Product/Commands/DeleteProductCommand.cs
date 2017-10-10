using System;

namespace AbstractCqrs.Sample.Domain.Product.Commands
{
    public class DeleteProductCommand : BaseCommand
    {
        public DeleteProductCommand(Guid rootId) : base(rootId)
        {
        }
    }
}