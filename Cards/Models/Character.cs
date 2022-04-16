using System;
using System.Collections.Generic;

namespace Cards.Models
{
    public class Character
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Gender { get; set; }
        public string Origin { get; set; }
        public string[] Episodes { get; set; }

        public List<string> EpisodeList { get; set; }

        public Character(){ EpisodeList = new List<string>(); }

        public Character(int id, string name, string image)
        {
            ID = id;
            Name = name;
            Image = image;
            EpisodeList = new List<string>();
        }

        public Character(int id, string name, string image,
            string status, string species, string gender, string origin)
        {
            ID = id;
            Name = name;
            Image = image;
            Status = status;
            Species = species;
            Gender = gender;
            Origin = origin;
            EpisodeList = new List<string>();
        }
    }
}
