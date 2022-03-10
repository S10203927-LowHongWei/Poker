using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Card
    {
        public string CardSuit { get; set; }
        public string CardNumber { get; set; }
        public Card() { }
        public Card(string cs, string cn)
        {
            CardSuit = cs;
            CardNumber = cn;
        }
        public override string ToString()
        {
            return "Card Suit: " + CardSuit;
        }
    }
}
