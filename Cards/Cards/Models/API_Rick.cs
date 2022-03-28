using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Cards.Models
{
    public class API_Rick
    {

        public API_Rick()
        {
        }

        public Character[] Search(string character)
        {
            Console.WriteLine($"Charcter: {character}");
            string url =
                $"https://rickandmortyapi.com/api/character/?name={character}";
            Console.WriteLine($"URL: {url}");

            // get json data from server
            string jsonData = GetJSonData(url);
            Console.WriteLine($"RESPONSE FROM SERVER: {jsonData}");

            // read json data
            if(jsonData.Equals("No Characters"))
            {
                return null;
            }

            Character[] characters = ReadJsonDataArray(jsonData);
            Console.WriteLine($"Number of Characters: {characters.Length}");

            return characters;
        }

        public Character GetCharacter(int id)
        {
            string url
                = $"https://rickandmortyapi.com/api/character/{id}";
            Console.WriteLine($"GET CHARACTER URL: {url}");

            string jsonData = GetJSonData(url);

            Character character = ReadJsonDataSingleCharacter(jsonData);

            return character;
        }

        private string GetJSonData(string url)
        {
            WebRequest request = WebRequest.Create(url);

            // Get the response.
            try
            {
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
                Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            } catch(Exception)
            {
                return "No Characters";
            }
        }

        private Character ReadJsonDataSingleCharacter(string jsonData)
        {
            Character character = new Character();

            using(JsonDocument document = JsonDocument.Parse(jsonData))
            {
                JsonElement root = document.RootElement;
                int id = root.GetProperty("id").GetInt32();
                string name = root.GetProperty("name").ToString();
                string image = root.GetProperty("image").ToString();
                string status = root.GetProperty("status").ToString();
                string gender = root.GetProperty("gender").ToString();
                string species = root.GetProperty("species").ToString();

                // origin
                JsonElement originInfo = root.GetProperty("origin");
                string origin = originInfo.GetProperty("name").ToString();

                character.ID = id;
                character.Name = name;
                character.Image = image;
                character.Status = status;
                character.Gender = gender;
                character.Species = species;
                character.Origin = origin;
            }

            return character;
        }

        private Character[] ReadJsonDataArray(string jsonData)
        {
            List<Character> characters = new List<Character>();

            using (JsonDocument document = JsonDocument.Parse(jsonData))
            {
                JsonElement root = document.RootElement;
                JsonElement charactersElement = root.GetProperty("results");

                foreach(JsonElement character in charactersElement.EnumerateArray())
                {
                    int id = character.GetProperty("id").GetInt32();
                    string name = character.GetProperty("name").ToString();
                    string image = character.GetProperty("image").ToString();

                    characters.Add(new Character(id, name, image));
                }

            }

            return characters.ToArray();
        }
    }
}
