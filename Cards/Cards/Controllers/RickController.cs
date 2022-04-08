using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cards.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cards.Controllers
{
    public class RickController : Controller
    {
        private static API_Rick API = new API_Rick();
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string character)
        {
            ViewData["CharacterName"] = character;
            if(character == null)
            {
                 character = "ALL CHARACTERS";
            }
            
            Character[] characters = API.Search(character);

            return View(characters);
        }

        public IActionResult ShowCharacter(int characterID)
        {
            Character character = API.GetCharacter(characterID);

            return View(character);
        }

        public IActionResult ShowEpisode(int episodeID)
        {
            Episode episode = API.GetEpisode(episodeID);

            return View(episode);
        }
    }
}
