using System;
namespace Cards.Models
{
    public class Card
    {
        public string Image { get; set; }
        public string Value { get; set; }
        public string Suit { get; set; }
        public string Code { get; set; }

        public Card(){ }

        public Card(string image, string value, string suit, string code)
        {
            Image = image;
            Value = value;
            Suit = suit;
            Code = code;
        }


    }
}
