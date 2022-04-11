using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Cards.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cards.Controllers
{
    public class TraderJoesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProduct(int id)
        {
            QueryTraderDB queryTrader = new QueryTraderDB();

            if(id == 0)
            {
                List<Product> products = queryTrader.GetAllProducts();
                string jsonData = JsonSerializer.Serialize(products);

                return View((object)jsonData);
            }

            Product product = queryTrader.GetProduct(id);
            string JsonData = JsonSerializer.Serialize(product);

            return View((object)JsonData);
        }

        public IActionResult Search(string name)
        {
            QueryTraderDB queryTrader = new QueryTraderDB();

            List<Product> products = queryTrader.Search(name);
            string JsonData = JsonSerializer.Serialize(products);

            return View((object)JsonData);
        }
    }
}
