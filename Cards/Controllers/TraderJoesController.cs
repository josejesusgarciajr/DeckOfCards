using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public string GetProduct(int id)
        {
            QueryTraderDB queryTrader = new QueryTraderDB();

            // Get all products
            if (id == 0)
            {
                List<Product> products = queryTrader.GetAllProducts();
                string jsonData = JsonSerializer.Serialize(products);

                return jsonData;
            }

            Product product = queryTrader.GetProduct(id);
            string JsonData = JsonSerializer.Serialize(product);

            return JsonData;
        }

        public string Search(string name)
        {
            QueryTraderDB queryTrader = new QueryTraderDB();

            List<Product> products = queryTrader.Search(name);
            string JsonData = JsonSerializer.Serialize(products);

            return JsonData;
        }

        // TESTING FETCHING JSon Data From Server
        public string FetchCharacter()
        {
            string url = "https://localhost:5001/TraderJoes/GetProduct/1";
            WebRequest request = WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine($"RESPONSE FROM SERVER: {responseFromServer}");
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        // TESTING FETCHING JSon Data From Server BY NAME
        public string FetchByName()
        {
            string url = "https://localhost:5001/TraderJoes/Search?name=Lady";
            WebRequest request = WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine($"RESPONSE FROM SERVER: {responseFromServer}");
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

    }
}
