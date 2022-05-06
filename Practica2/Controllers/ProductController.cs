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
            return Ok(_productMgr.GetProducts());
        }
        [HttpPost]
        public IActionResult CreateProduct(string name, string type, int stock)
        {
            IActionResult res;
            if ((type.ToUpper().Equals("SOCCER")) || (type.ToUpper().Equals("BASKET")))
            {
                Product createdProduct = _productMgr.CreateProduct(name, type.ToUpper(), stock);
               
                res = Ok(createdProduct);
                
            }
            else
            {
                res = BadRequest();
            }
            return res;
        }
        [HttpPut]
        public IActionResult UpdateProduct(string codeToFind, string newName, int newStock)
        {
            IActionResult res;
            if (_productMgr.UpdateProuct(newName, newStock, codeToFind) < 0)
            {
                res = NotFound();
            }
            else
            {
                res = Ok();
            }
            return res;
        }
        [HttpDelete]
        public IActionResult DeleteProduct(string codeToFind)
        {
            IActionResult res;
            if (_productMgr.DeleteProduct(codeToFind) < 0)
            {
                res = NotFound();
            }
            else
            {
                res = Ok();
            }
            return res;
        }
    }
}