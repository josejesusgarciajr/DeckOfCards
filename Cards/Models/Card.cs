using System;
namespace Cards.Models
{
    public class Card
    {
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
        public string code { get; set; }

        public Card(){ }

        public Card(string img, string val, string su, string c)
        {
            image = img;
            value = val;
            suit = su;
            code = c;
        }


    }
}
