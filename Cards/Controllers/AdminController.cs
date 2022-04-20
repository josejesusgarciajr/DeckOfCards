using Microsoft.AspNetCore.Mvc;
using Cards.Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cards.Controllers
{
    public class AdminController : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            QueryTraderDB queryTrader = new QueryTraderDB();
            List<Product> products = queryTrader.GetAllProducts();

            return View(products);
        }

        public IActionResult AddTJProductView()
        {
            return View();
        }

        public IActionResult AddTJProduct(Product product)
        {
            QueryTraderDB queryTrader = new QueryTraderDB();
            queryTrader.AddProduct(product);

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult DeleteTJProduct(int productID)
        {
            QueryTraderDB queryTrader = new QueryTraderDB();
            queryTrader.DeleteProduct(productID);

            return RedirectToAction("Index", "Admin");
        }

    }
}
