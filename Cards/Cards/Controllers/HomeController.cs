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
         * 
         * HOW TO GET A SPECIFIC SUBSECTION OF A JSON DATA
         * https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-6-0#deserialize-subsections-of-a-json-payload
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
            Console.WriteLine("CLOSED EVERYTHING");
            /*
             * Deserialize JsonData to Object
             */
            Console.WriteLine("BEFORE DESERIALZIE");
            deckOfCards =
                JsonSerializer.Deserialize<DeckOfCards>(responseFromServer);
            Console.WriteLine("DESERIALIZE");

            Console.WriteLine("");
            Console.WriteLine($"Success: {deckOfCards?.success}");
            Console.WriteLine($"DeckID: {deckOfCards.deck_id}");
            Console.WriteLine($"Shuffled: {deckOfCards?.shuffled}");
            Console.WriteLine($"Remaing: {deckOfCards?.remaining}");
            return View();
        }

        public IActionResult DrawCards(int numberOfCards = -1)
        {
            Console.WriteLine($"Number of Cards to Draw: {numberOfCards}");
            Card[] cards = new Card[1];

            if(numberOfCards == -1)
            {
                Console.WriteLine("RETURN CARD NOT FIRED");
               
                // get vanessas cards
                cards = deckOfCards.GetVanessasCards();
            } else
            {
                Console.WriteLine($"RETURN CARD FIRED");
                cards = deckOfCards?.DrawCard(numberOfCards);
            }

            return View(cards);
        }

        public IActionResult ReturnCardToDeck(string code)
        {
            deckOfCards?.ReturnCardToDeck(code);
            return RedirectToAction("DrawCards", "Home", -1);
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
