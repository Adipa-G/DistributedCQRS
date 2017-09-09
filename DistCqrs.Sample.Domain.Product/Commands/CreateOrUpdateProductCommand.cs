using System;

namespace DistCqrs.Sample.Domain.Product.Commands
{
    public class CreateOrUpdateProductCommand : BaseCommand
    {
        public CreateOrUpdateProductCommand(Guid rootId,
            string code,
            string name,
            double unitPrice) : base(rootId)
        {
            Code = code;
            Name = name;
            UnitPrice = unitPrice;
        }

        public string Code { get;  }

        public string Name { get;  }

        public double UnitPrice { get;  }
    }
}
