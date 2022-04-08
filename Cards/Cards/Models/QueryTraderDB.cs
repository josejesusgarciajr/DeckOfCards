using System;
using System.Collections.Generic;

namespace Cards.Models
{
    public class QueryTraderDB
    {
        private string CS { get; set; }

        public QueryTraderDB()
        {
            CS = Environment.GetEnvironmentVariable("TRADERJOESCS");
        }

        public Product GetProduct(int id)
        {
            //Product product = new Product();
            Product product = new Product(1, "Tortilla", "0000 0000", "image" , "OKAY", "GOOD");

            return product;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            return products;
        }

        public List<Product> Search(string name)
        {
            List<Product> products = new List<Product>();

            return products;
        }
    }
}
