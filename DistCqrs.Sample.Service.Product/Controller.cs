using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistCqrs.Sample.Service.Product
{
    [Route("api/product")]
    public class Controller
    {
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return new OkResult();
        }
    }
}
