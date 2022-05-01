using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Logic.Managers;
using Logic.Entities;

namespace Practica2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ProductManager _productMgr;
        public ProductController(ProductManager productManager)
        {
            _productMgr = productManager;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateProduct()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateProduct()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteProduct()
        {
            return Ok();
        }
    }
}