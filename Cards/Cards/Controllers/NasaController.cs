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

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DisplayMarsRoverImages(string rover, string camera, int page)
        {

            string url
                = $"https://api.nasa.gov/mars-photos/api/v1/rovers/{rover}/photos?sol=1000";
            string api_key = "&api_key=DEMO_KEY";

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

            return View(nasaPhotos);
        }
    }
}
