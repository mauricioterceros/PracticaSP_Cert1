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
        [Route("/calculate-prices")]
        public IActionResult GetProducts()
        {
            return Ok(_productMgr.GetProducts());
        }
        [HttpPost]
        public IActionResult CreateProduct(string name, string type, int stock)
        {
            Product createdProduct = _productMgr.CreateProduct(name, type.ToUpper(), stock);
            return Ok(createdProduct);
        }
        [HttpPut]
        public IActionResult UpdateProduct(string codeToFind, string newName, int newStock)
        {
            int res = _productMgr.UpdateProuct(newName, newStock, codeToFind);
            return Ok(res);
        }
        [HttpDelete]
        public IActionResult DeleteProduct(string codeToFind)
        {
            int res = _productMgr.DeleteProduct(codeToFind);
            return Ok(res);
        }
    }
}