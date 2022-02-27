using System;
using System.Collections.Generic;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> CardList = new List<Card>();
            List<Card> PlayersCardsList = new List<Card>();
            List<Card> River = new List<Card>();
            List<Card> DealerCards = new List<Card>();
            Card(CardList);
            Console.Write("Enter the amount of players: ");
            int players = Convert.ToInt32(Console.ReadLine());
            Hands(CardList, PlayersCardsList,players);
            Options();
            Console.Write("Option Picked: ");
            int choice =Convert.ToInt32(Console.ReadLine());
            if(choice == 1)
            {

            }
            else if(choice == 2)
            {

            }
            else if(choice == 3)
            {

            }
        }
        static void River(List<Card> River, List<Card> CardList)
        {
            for (int x = 0; x < 5; x++)
            {
                Card R = RandomCard(CardList);
                River.Add(R);
            }
        }
        static void Hands(List<Card> CardList, List<Card> PlayersCardsList, int players)
        {
            Card dc1 = RandomCard(CardList);
            Card dc2 = RandomCard(CardList);
            PlayersCardsList.Add(dc1);
            PlayersCardsList.Add(dc2);
            for (int i = 0; i < players; i++)
            {
                Card pc1 = RandomCard(CardList);
                Card pc2 = RandomCard(CardList);
                PlayersCardsList.Add(pc1);
                PlayersCardsList.Add(pc2);
            }
        }
        static void PlayersCard(Card p1c1, Card p1c2)
        {
            Console.WriteLine("Your Cards");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("{0} of {1} and {2} of {3}", p1c1.CardNumber, p1c1.CardSuit, p1c2.CardNumber, p1c2.CardSuit);
            Console.WriteLine("--------------------------------");
        }
        static void Options()
        {
            Console.WriteLine("Chose one of the options:");
            Console.WriteLine("------------------------");
            Console.WriteLine("[1] Check");
            Console.WriteLine("[2] Raise");
            Console.WriteLine("[3] Fold");
            Console.WriteLine("------------------------");
        }
        static Card RandomCard(List<Card> CL)
        {
            Random Rndm = new Random();
            Card Card1 = CL[Rndm.Next(CL.Count)];
            CL.Remove(Card1);
            return Card1;
        } 
        static void Card(List<Card> CL)
        {
            List<string> NonNumber = new List<string>();
            NonNumber.Add("A");
            NonNumber.Add("J");
            NonNumber.Add("Q");
            NonNumber.Add("K");
            for(int i = 2; i<11; i++)
            {
                string s = i.ToString();
                Card s1 = new Card(s, "Spades");
                Card c1 = new Card(s, "Clubs");
                Card d1 = new Card(s, "Diamonds");
                Card h1 = new Card(s, "Hearts");
                CL.Add(s1);
                CL.Add(c1);
                CL.Add(d1);
                CL.Add(h1);
            }
            for(int x = 0; x < 4; x++)
            {
                Card s1 = new Card(NonNumber[x], "Spades");
                Card c1 = new Card(NonNumber[x], "Clubs");
                Card d1 = new Card(NonNumber[x], "Diamonds");
                Card h1 = new Card(NonNumber[x], "Hearts");
                CL.Add(s1);
                CL.Add(c1);
                CL.Add(d1);
                CL.Add(h1);
            }
            
        }
    }
}
