using System;
using System.Collections.Generic;
using System.Text;
using Logic.Entities;
using Microsoft.Extensions.Configuration;

namespace Logic.Managers
{
    public class ProductManager
    {
        private List<Product> _products;
        private IConfiguration _configuration;
        private int deleted;
        
        public ProductManager(IConfiguration configuration)
        {
            deleted = 0;
            _configuration = configuration;
            _products = new List<Product>();
            _products.Add(new Product() { Name = "Pelota Adidas", Type = "SOCCER", Stock = 10, Code = "", Price = 0 });
            _products.Add(new Product() { Name = "Polera MESSI PSG", Type = "SOCCER", Stock = 15, Code = "", Price = 0 });
            _products.Add(new Product() { Name = "Polera Lebron James LAKERS ", Type = "BASKET", Stock = 7, Code = "", Price = 0 });
        }

        public List<Product> GetProducts()
        {
            foreach (Product prod in _products)
            {
                if (prod.Code.Equals(""))
                {
                    prod.Code = $"{prod.Type}-{_products.IndexOf(prod)+deleted}"; //add deleted elements to avoid code conflict
                }
                //aqui definir precio usando backing service para los productos sin precio definido
            }
            return _products;
        }
        public Product CreateProduct(string name, string type, int stock)
        {
            Product nprod = new Product() { Name = name, Type = type, Stock = stock, Code = "", Price = 0 };
            nprod.Code = $"{nprod.Type}-{_products.Count+deleted}"; //add deleted elements to avoid code conflict
            //aqui definir precio usando backing service
            _products.Add(nprod);
            return nprod;
        }
        public int UpdateProuct(string name, int stock, string code)
        {
            int res = 0;
            int indProduct = _products.FindIndex(p => p.Code.Equals(code));
            if (indProduct < 0)
            {
                res = indProduct;
            }
            else
            {
                Product fproduct = _products[indProduct];
                fproduct.Name = name;
                fproduct.Stock = stock;
            }
            return res;
        }
        public int DeleteProduct(string code)
        {
            int res = 0;
            int indProduct = _products.FindIndex(p => p.Code.Equals(code));
            if (indProduct < 0)
            {
                res = indProduct;
            }
            else
            {
                _products.RemoveAt(indProduct);
                deleted++;
            }
            return res;
        }
    }
}
