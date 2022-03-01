using System;
using System.Collections.Generic;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 4;
            List<Card> CardList = new List<Card>();
            List<Card> PlayersCardsList = new List<Card>();
            List<Card> RiverList = new List<Card>();
            List<Card> DealerCards = new List<Card>();
            List<int> PlayerList = new List<int>();
            PokerBalance PB = new PokerBalance();
            int pot = 0;
            while (true)
            {
                MainMenu();
                Console.Write("Your choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    Card(CardList);
                    Console.Write("Enter the amount of players: ");
                    int players = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < players; i++) { PlayerList.Add(i); Console.WriteLine(i); }
                    Hands(CardList, PlayersCardsList, players);
                    Console.Write("Which player would you want to be: ");
                    int playing = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < players; i++)
                    {
                        if (i == playing - 1)
                        {
                            int index = (playing * 2) - 2;
                            int index2 = (playing * 2) - 1;
                            Card p1c1 = PlayersCardsList[index];
                            Card p1c2 = PlayersCardsList[index2];
                            PlayersCard(p1c1, p1c2);
                        }
                    }
                    River(RiverList, CardList);
                    while (true)
                    {
                        n++;
                        Options();
                        Console.Write("Option Picked: ");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        if (n > 6) { break; }
                        if (choice == 1)
                        {
                            Console.WriteLine("The River is ");
                            Console.WriteLine("--------------");
                            for (int r = 0; r < n - 2; r++)
                            {
                                Console.Write("|   {0} of {1}   ", RiverList[r].CardNumber, RiverList[r].CardSuit);
                            }
                            Console.WriteLine("|");
                            Console.WriteLine("");
                        }
                        else if (choice == 2)
                        {
                            Console.Write("How much do you want to raise: ");
                            int Raiseamt = Convert.ToInt32(Console.ReadLine());
                            PB.DeductBalance(Raiseamt);
                            pot += Raiseamt;
                        }
                        else if (choice == 3){break;}
                    }
                }
                else if (option == 2)
                {
                    Console.Write("How much would you like to buy in for?: ");
                    double buyin = Convert.ToDouble(Console.ReadLine());
                    PB.Balance = buyin;
                }
                else if (option == 3) { Console.WriteLine(PB.Balance); }
                else if (option == 4)
                {
                    Console.Write("How much would you like to top up?: ");
                    double topup = Convert.ToDouble(Console.ReadLine());
                    PB.AddBalance(topup);
                }
                else if (option == 5) { break; }
            }
        }
        static void MainMenu()
        {
            Console.WriteLine("Welcome to Poker Night! Please pick an option");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("[1] Play Poker");
            Console.WriteLine("[2] Set Balance");
            Console.WriteLine("[3] Check Balance");
            Console.WriteLine("[4] Top up balance");
            Console.WriteLine("[5] Exit");
            Console.WriteLine("---------------------------------------------");
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
