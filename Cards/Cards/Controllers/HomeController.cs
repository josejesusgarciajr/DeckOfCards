using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cards.Models;
using System.Net;
using System.IO;
using System.Text.Json;

namespace Cards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*
         * https://docs.microsoft.com/en-us/dotnet/api/system.net.webrequest?view=net-6.0
         */
        public IActionResult ShuffleDeck()
        {
            WebRequest request =
                WebRequest.Create("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine($"Response: {response}");
            Console.WriteLine(response.StatusDescription);

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            // json data
            DeckOfCards? deckOfCards =
                JsonSerializer.Deserialize<DeckOfCards>(responseFromServer);

            Console.WriteLine("");
            Console.WriteLine($"Success: {deckOfCards?.success}");
            Console.WriteLine($"DeckID: {deckOfCards?.deck_id}");
            Console.WriteLine($"Shuffled: {deckOfCards?.shuffled}");
            Console.WriteLine($"Remaing: {deckOfCards?.remaining}");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
