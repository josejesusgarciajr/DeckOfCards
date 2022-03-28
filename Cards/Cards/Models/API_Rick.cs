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

        public Episode GetEpisode(int episodeID)
        {

            string url
                = $"https://rickandmortyapi.com/api/episode/{episodeID}";

            string jsonData = GetJSonData(url);

            Episode episode = ReadJsonDataForEpisode(jsonData);

            return episode;
        }

        public Character[] Search(string character)
        {
            string url =
                $"https://rickandmortyapi.com/api/character/?name={character}";
            Console.WriteLine($"URL: {url}");

            // get json data from server
            string jsonData = GetJSonData(url);

            // read json data
            if(jsonData.Equals("No Characters"))
            {
                return null;
            }

            Character[] characters = ReadJsonDataArray(jsonData);

            return characters;
        }

        public Character GetCharacter(int id)
        {
            string url
                = $"https://rickandmortyapi.com/api/character/{id}";

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
                Console.WriteLine($"Root: {root}");
                int id = root.GetProperty("id").GetInt32();
                string name = root.GetProperty("name").ToString();
                string image = root.GetProperty("image").ToString();
                string status = root.GetProperty("status").ToString();
                string gender = root.GetProperty("gender").ToString();
                string species = root.GetProperty("species").ToString();

                JsonElement episodeArray = root.GetProperty("episode");
                foreach(JsonElement episode in episodeArray.EnumerateArray())
                {
                    Console.WriteLine($"EPISODE: {episode.ToString()}");
                    character.EpisodeList.Add(episode.ToString());
                }

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
                character.Episodes = character.EpisodeList.ToArray();
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

        private Episode ReadJsonDataForEpisode(string jsonData)
        {

            Episode? episode =
                JsonSerializer.Deserialize<Episode>(jsonData);

            List<Character> characters = new List<Character>();
            foreach(string characterURL in episode.characters)
            {
                string[] data = characterURL.Split("https://rickandmortyapi.com/api/character/");
                int characterID = Int32.Parse(data[1]);

                characters.Add(GetCharacter(characterID));
            }

            episode.CharactersArray = characters.ToArray();

            return episode;
        }

    }
}
