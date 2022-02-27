using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class PokerBalance
    {
        public double Balance { get; set; }
        public PokerBalance() { }
        public PokerBalance(double b)
        {
            Balance = b;
        }
        public double AddBalance(double amt)
        {
            Balance += amt;
            return Balance;
        }
        public double DeductBalance(double amt)
        {
            Balance -= amt;
            return Balance;
        }
    }
}
