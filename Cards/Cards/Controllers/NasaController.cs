using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cards.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cards.Controllers
{
    public class NasaController : Controller
    {

        /*
         *  SET UP NASA API
         */
        private static Nasa_API Nasa = new Nasa_API();
        private static List<NasaPhoto> LastNasaPhotoFetch = new List<NasaPhoto>();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NasaPhotoInfo(int photoID)
        {
            NasaPhoto nasaPhoto = new NasaPhoto();
            foreach(NasaPhoto nasaPhotoQuery in LastNasaPhotoFetch)
            {
                if(nasaPhotoQuery.id == photoID)
                {
                    nasaPhoto = nasaPhotoQuery;
                }
            }

            return View(nasaPhoto);
        }

        public IActionResult DisplayMarsRoverImages(string rover, string camera, int page)
        {

            string url
                = $"https://api.nasa.gov/mars-photos/api/v1/rovers/{rover}/photos?sol=1000";
            string nasa_api_key = "";
            try
            {
                nasa_api_key = Environment.GetEnvironmentVariable("NASA_API_KEY");
            } catch(Exception)
            {
                nasa_api_key = "DEMO_KEY";
            }
            


            string api_key = $"&api_key={nasa_api_key}";

            if (!camera.Equals("ALL"))
            {
                Console.WriteLine("CAMERA SELECTED");
                url
                    += $"&camera={camera}";
            }

            if(page != -1)
            {
                Console.WriteLine("PAGE SELECTED");
                url
                    += $"&page={page}";
            }

            url += api_key;
            Console.WriteLine($"NASA URL: {url}");

            NasaPhoto[] nasaPhotos = Nasa.GetAllImages(url);
            LastNasaPhotoFetch = nasaPhotos.ToList<NasaPhoto>();

            return View(nasaPhotos);
        }
    }
}
