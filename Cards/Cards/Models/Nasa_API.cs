using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Cards.Models
{
    public class Nasa_API
    {
        
        public Nasa_API()
        {

        }

        public NasaPhoto[] GetAllImages(string url)
        {
            string jsonData = GetJsonDataFromNasa(url);
            NasaPhoto[] nasaPhotos = GetAllImagesFrom(jsonData);

            return nasaPhotos;
        }

        private string GetJsonDataFromNasa(string url)
        {
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
            Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        private NasaPhoto[] GetAllImagesFrom(string jsonData)
        {
            List<NasaPhoto> nasaPhotos = new List<NasaPhoto>();
            // read json data
            using(JsonDocument document = JsonDocument.Parse(jsonData))
            {
                JsonElement root = document.RootElement;
                JsonElement nasaJsonPhotosArray = root.GetProperty("photos");

                foreach(JsonElement nasaJsonPhoto in nasaJsonPhotosArray.EnumerateArray())
                {
                    // deserialize each NASA PHOTO
                    NasaPhoto? nasaPhoto
                        = JsonSerializer.Deserialize<NasaPhoto>(nasaJsonPhoto.ToString());

                    nasaPhotos.Add(nasaPhoto);
                }
            }

            return nasaPhotos.ToArray();
        }
    }
}
