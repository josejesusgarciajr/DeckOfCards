using System;
namespace Cards.Models
{
    public class Episode
    {
        // must match json
        public int id { get; set; }
        public string name { get; set; }
        public string air_date { get; set; }
        public string episode { get; set; }
        public string[]? characters { get; set; }

        public Character[] CharactersArray { get; set; }

        public Episode()
        {
        }
    }
}
