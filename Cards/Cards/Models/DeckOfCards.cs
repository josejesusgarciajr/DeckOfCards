﻿using System;
namespace Cards.Models
{
    public class DeckOfCards
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public int remaining { get; set; }

        public DeckOfCards()
        {
        }
    }
}
