using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Card
    {
        public string CardNumber { get; set; }
        public string CardSuit { get; set; }
        public Card() { }
        public Card(string cn, string cs)
        {
            CardNumber = cn;
            CardSuit = cs;
        }

    }
}
