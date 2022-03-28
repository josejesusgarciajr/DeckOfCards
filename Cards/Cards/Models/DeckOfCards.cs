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

        public List<Player> Players { get; set; }

        // vanessas hand
        public Card[]? Vanessa { get; set; }


        public DeckOfCards()
        {
            
        }

        public Card[] ReturnCardToDeck(string code)
        {
            /*
             * http://deckofcardsapi.com/api/deck/<<deck_id>>/return/?cards=AS,2S
             * http://deckofcardsapi.com/api/deck/<<deck_id>>/pile/<<pile_name>>/return/?cards=AS,2S
             */
            Card[] cards = new Card[1];
            Console.WriteLine($"Returning to Deck: {code}");

            string url =
                $"http://deckofcardsapi.com/api/deck/{deck_id}/pile/Vanessa/return/?cards={code}";

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

        public Card[] GetVanessasCards()
        {
            // list pile to show
            string url = $"http://deckofcardsapi.com/api/deck/{deck_id}/pile/Vanessa/list/";
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

            List<Card> d = new List<Card>();
            using (JsonDocument document = JsonDocument.Parse(responseFromServer))
            {
                JsonElement root = document.RootElement;
                JsonElement piles = root.GetProperty("piles");
                JsonElement vanessa = piles.GetProperty("Vanessa");
                JsonElement cards = vanessa.GetProperty("cards");
                Console.WriteLine("");
                Console.WriteLine($"Vanessa????: {cards}");

                foreach (JsonElement card in cards.EnumerateArray())
                {
                    string image = card.GetProperty("image").ToString();
                    string value = card.GetProperty("value").ToString();
                    string suit = card.GetProperty("suit").ToString();
                    string code = card.GetProperty("code").ToString();
                    d.Add(new Card(image, value, suit, code));
                }
            }

            return d.ToArray();
        }

        public void AddToPile(DeckOfCards? deckOfCards, string pile = "Vanessa")
        {
            // add to vanessa pile ----------------------------------------------------------------
            string cardsForVanessa = CardsToAddToPile(deckOfCards.cards);
            string vanessaURL =
                    $"http://deckofcardsapi.com/api/deck/{deckOfCards.deck_id}/pile/{pile}/add/?cards={cardsForVanessa}";
            Console.WriteLine($"URL Vanessa: {vanessaURL}");
            WebRequest request = WebRequest.Create(vanessaURL);

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
        }

        public Card[] ListPile(DeckOfCards deckOfCards, string pile = "Vanessa")
        {
            string url = $"http://deckofcardsapi.com/api/deck/{deckOfCards.deck_id}/pile/{pile}/list/";
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

            List<Card> d = new List<Card>();
            using (JsonDocument document = JsonDocument.Parse(responseFromServer))
            {
                JsonElement root = document.RootElement;
                JsonElement piles = root.GetProperty("piles");
                JsonElement vanessa = piles.GetProperty("Vanessa");
                JsonElement cards = vanessa.GetProperty("cards");
                Console.WriteLine("");
                Console.WriteLine($"Vanessa????: {cards}");

                foreach (JsonElement card in cards.EnumerateArray())
                {
                    string image = card.GetProperty("image").ToString();
                    string value = card.GetProperty("value").ToString();
                    string suit = card.GetProperty("suit").ToString();
                    string code = card.GetProperty("code").ToString();
                    d.Add(new Card(image, value, suit, code));
                }
            }
            Console.WriteLine($"LIST: {d.Count}");

            return d.ToArray();
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
            Console.WriteLine($"Cards: {deckOfCards?.cards}");

            AddToPile(deckOfCards);
 
            // list pile to show
            deckOfCards.Vanessa = ListPile(deckOfCards);

            return ListPile(deckOfCards);

        }

        private string CardsToAddToPile(Card[] cards)
        {
            string s = "";
            foreach(Card card in cards)
            {
                s += card.code + ",";
            }
            Console.WriteLine($"list of cards for vanessa: {s.Substring(0, s.Length - 1)}");
            return s.Substring(0, s.Length - 1);
        }
    }
}
