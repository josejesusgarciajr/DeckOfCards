using System;
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

        public Character(){ }

        public Character(int id, string name, string image)
        {
            ID = id;
            Name = name;
            Image = image;
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
        }
    }
}
