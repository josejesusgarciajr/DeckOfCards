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
        /*
         * HOW TO READ JSON DATA FROM AN API
         * https://docs.microsoft.com/en-us/dotnet/api/system.net.webrequest?view=net-6.0
         * 
         * HOW TO DESERIALIZE JSONDATA INTO AN OBJECT
         * https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0#how-to-read-json-as-net-objects-deserialize
         */
        private readonly ILogger<HomeController> _logger;
        private static DeckOfCards? deckOfCards { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShuffleDeck()
        {
            /*
             * Read JsonData
             */
            string url = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";
            WebRequest request =
                WebRequest.Create(url);

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

            /*
             * Deserialize JsonData to Object
             */
            deckOfCards =
                JsonSerializer.Deserialize<DeckOfCards>(responseFromServer);

            Console.WriteLine("");
            Console.WriteLine($"Success: {deckOfCards?.success}");
            Console.WriteLine($"DeckID: {deckOfCards?.deck_id}");
            Console.WriteLine($"Shuffled: {deckOfCards?.shuffled}");
            Console.WriteLine($"Remaing: {deckOfCards?.remaining}");

            return View();
        }

        public IActionResult DrawCards(int numberOfCards)
        {
            Console.WriteLine($"Number of Cards to Draw: {numberOfCards}");
            Card[] cards = deckOfCards?.DrawCard(numberOfCards);
            return View(cards);
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
