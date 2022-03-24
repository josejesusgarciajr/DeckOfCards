using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Cards.Models
{
    public class DeckOfCards
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public int remaining { get; set; }
        public Card[]? cards { get; set; }

        public DeckOfCards()
        {
            
        }

        public Card[] DrawCard(int count)
        {
            string url =
                $"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={count}";

            WebRequest request = WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
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

            // Deserialize JsonData into Object
            DeckOfCards? deckOfCards =
                JsonSerializer.Deserialize<DeckOfCards>(responseFromServer);

            Console.WriteLine("");
            Console.WriteLine($"Success: {deckOfCards?.success}");
            Console.WriteLine($"DeckID: {deckOfCards?.deck_id}");
            Console.WriteLine($"Shuffled: {deckOfCards?.shuffled}");
            Console.WriteLine($"Remaing: {deckOfCards?.remaining}");
            Console.WriteLine($"Cards: {cards?.Length}");

            return deckOfCards?.cards;

        }
    }
}
