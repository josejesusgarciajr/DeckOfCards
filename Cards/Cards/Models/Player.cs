using System;
using System.Collections.Generic;

namespace Cards.Models
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> cards { get; set; }

        public Player()
        {
            cards = new List<Card>();
        }

    }
}
