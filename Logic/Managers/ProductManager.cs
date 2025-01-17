﻿using System;
using System.Collections.Generic;
using System.Text;
using Logic.Entities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.IO;
using Services;

namespace Logic.Managers
{
    public class ProductManager
    {
        private List<Product> _products;
        private IConfiguration _configuration;
        private NumberService _numberService;
        Number recivednumber;
        private int deleted;
        
        public ProductManager(IConfiguration configuration,NumberService numberService)
        {
            deleted = 0;
            _configuration = configuration;
            _numberService = numberService;
            
            string path = _configuration.GetSection("DBroute").Value;
            if (!File.Exists(path))
            {
                _products = new List<Product>();
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                _products.Add(new Product() { Name = "Pelota Adidas", Type = "SOCCER", Stock = 10, Code = "", Price = 0.0 });
                _products.Add(new Product() { Name = "Polera MESSI PSG", Type = "SOCCER", Stock = 15, Code = "", Price = 0.0 });
                _products.Add(new Product() { Name = "Polera Lebron James LAKERS ", Type = "BASKET", Stock = 7, Code = "", Price = 0.0 });
            }
            else
            {
                string dbjson = File.ReadAllText(path);
                
                _products = JsonConvert.DeserializeObject<List<Product>>(dbjson);
            }
            
        }

        public List<Product> GetProducts()
        {
            foreach (Product prod in _products)
            {
                if (prod.Code.Equals(""))
                {
                    prod.Code = $"{prod.Type}-{_products.IndexOf(prod)+deleted}";
                }
                while (prod.Price <= 0.0)
                {
                    recivednumber = _numberService.GetNumber().Result;
                    prod.Price = recivednumber.digit;
                }
            }
            string path = _configuration.GetSection("DBroute").Value;
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            string json = JsonConvert.SerializeObject(_products);
            System.IO.File.WriteAllText(path, json);
            return _products;
        }
        public Product CreateProduct(string name, string type, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidProductDataException("Invalid product name");
            }
            if (!(type.Equals("SOCCER")) || !(type.Equals("BASKET")))
            {
                throw new InvalidProductDataException("Invalid product type");
            }
            if (stock < 0)
            {
                throw new InvalidProductDataException("Invalid product stock");
            }
            Number recivednumber = _numberService.GetNumber().Result;
            Product nprod = new Product() { Name = name, Type = type, Stock = stock, Code = "", Price = 0.0 };
            nprod.Code = $"{nprod.Type}-{_products.Count+deleted}"; 
            _products.Add(nprod);
            string path = _configuration.GetSection("DBroute").Value;
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            string json = JsonConvert.SerializeObject(_products);
            System.IO.File.WriteAllText(path, json);
            return nprod;
        }
        public int UpdateProuct(string name, int stock, string code)
        {
            int res = 0;
            int indProduct = _products.FindIndex(p => p.Code.Equals(code));
            if (indProduct < 0)
            {
                res = indProduct;
                throw new ProductNotFoundException("Product with that code doesn't exist");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new InvalidProductDataException("Invalid product name");
                }
                if (stock < 0)
                {
                    throw new InvalidProductDataException("Invalid product stock");
                }
                Product fproduct = _products[indProduct];
                fproduct.Name = name;
                fproduct.Stock = stock;
            }
            string path = _configuration.GetSection("DBroute").Value;
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            string json = JsonConvert.SerializeObject(_products);
            System.IO.File.WriteAllText(path, json);
            return res;
        }
        public int DeleteProduct(string code)
        {
            int res = 0;
            int indProduct = _products.FindIndex(p => p.Code.Equals(code));
            if (indProduct < 0)
            {
                res = indProduct;
                throw new ProductNotFoundException("Product with that code doesn't exist");
            }
            else
            {
                _products.RemoveAt(indProduct);
                deleted++;
            }
            string path = _configuration.GetSection("DBroute").Value;
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            string json = JsonConvert.SerializeObject(_products);
            System.IO.File.WriteAllText(path, json);
            return res;
        }
        
    }
}
