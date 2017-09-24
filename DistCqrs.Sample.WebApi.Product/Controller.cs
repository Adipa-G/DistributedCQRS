using System;
using System.Threading.Tasks;
using DistCqrs.Core.Resolve;
using DistCqrs.Sample.Domain.Product.Commands;
using DistCqrs.Sample.Domain.Product.Model;
using DistCqrs.Sample.Domain.Product.View;
using DistCqrs.Sample.Service.Product;
using Microsoft.AspNetCore.Mvc;

namespace DistCqrs.Sample.WebApi.Product
{
    [Route("api/product")]
    public class Controller
    {
        private readonly IServiceLocator serviceLocator;
        private readonly IProductView productView;

        public Controller(IServiceLocator serviceLocator,
            IProductView productView)
        {
            this.serviceLocator = serviceLocator;
            this.productView = productView;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await productView.GetById(id);
            return new JsonResult(product);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(ProductModel model)
        {
            model.Id = Guid.NewGuid();
            
            var bus = serviceLocator.ResolveBus(Constants.BusId);
            await bus.Send(new CreateOrUpdateProductCommand(model.Id,
                model.Code, model.Name, model.UnitPrice));

            return new JsonResult(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,ProductModel model)
        {
            model.Id = id;

            var bus = serviceLocator.ResolveBus(Constants.BusId);
            await bus.Send(new CreateOrUpdateProductCommand(id,
                model.Code, model.Name, model.UnitPrice));

            return new JsonResult(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var bus = serviceLocator.ResolveBus(Constants.BusId);
            await bus.Send(new DeleteProductCommand(id));

            return new OkResult();
        }
    }
}
