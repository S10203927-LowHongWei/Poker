using System;
using System.Collections.Generic;
using System.Linq;
using VisioForge.MediaFramework.DSP;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> Deck = new List<Card>();//List that contains all 52 cards in a normal deck of cards
            List<Card> PlayersCardsList = new List<Card>();//List to contain all cards given to players
            List<Card> RiverList = new List<Card>();//List to contain the cards of the river
            List<Card> DealerCards = new List<Card>();//List to contain all the dealers cards
            List<int> PlayerList = new List<int>();//List to contain the player seat number
            List<Card> PlayersPersonalCard = new List<Card>();//Individual players cards
            IDictionary<int, int> Playershighestcard = new Dictionary<int, int>();//Dictionary to store the players highest card
            List<string> stringnum = new List<string>();//Making the A K Q J into numbers in string format
            List<int> intnum = new List<int>();//Converts Stringnum into int format
            PokerBalance PB = new PokerBalance();//Balance Object
            int pot = 0;
            while (true)
            {
                int n = 4;
                MainMenu();
                Console.Write("Your choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    AddToDeck(Deck);
                    Console.Write("Enter the amount of players: ");
                    int players = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < players; i++) { PlayerList.Add(i); }
                    Hands(Deck, PlayersCardsList, players, DealerCards);
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
                            PlayersPersonalCard.Add(p1c1);
                            PlayersPersonalCard.Add(p1c2);
                            PlayersCard(p1c1, p1c2);
                        }
                    }
                    River(RiverList, Deck);
                    while (true)
                    {
                        n++;
                        Options();
                        Console.Write("Option Picked: ");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        if (n > 7)
                        {
                            Card c1 = PlayersPersonalCard[0];
                            Card c2 = PlayersPersonalCard[1];
                            int h1 = Scoring(RiverList, c1, c2, (Dictionary<int, int>)Playershighestcard, stringnum, intnum);
                            HowTheyWon(h1);
                            Reset(Deck, PlayersCardsList, RiverList, DealerCards, PlayerList, PlayersPersonalCard, Playershighestcard, n, stringnum, intnum);
                            break;
                        }
                        if (choice == 1)
                        {
                            Console.WriteLine(n);
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
                        else if (choice == 3) { break; }
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
        static void HowTheyWon(int i)
        {
            if(i == 1) { Message("Royal Flush"); }
            else if(i == 2) { Message("Straight Flush"); }
            else if (i == 3) { Message("Quadriples"); }
            else if (i == 4) { Message("Full House"); }
            else if (i == 5) { Message("Flush"); }
            else if (i == 6) { Message("Straight"); }
            else if (i == 7) { Message("Triples"); }
            else if (i == 8) { Message("Two Pair"); }
            else if (i == 9) { Message("Pair"); }
            else if (i == 10) { Message("High Card"); }
        }
        static void Message(string msg)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("You have won by {0}", msg);
            Console.WriteLine("---------------------------------");
        }
        static void Reset(List<Card> Deck, List<Card> PlayersCardsList, List<Card> RiverList, List<Card> DealerCards, List<int> PlayerList, List<Card> PlayersPersonalCard, IDictionary<int, int> Playershighestcard, int n, List<string> stringnum, List<int> intnum)//Function to reset the Lists that contains the cards
        {
            Deck.Clear();
            PlayersCardsList.Clear();
            RiverList.Clear();
            DealerCards.Clear();
            PlayerList.Clear();
            PlayersPersonalCard.Clear();
            Playershighestcard.Clear();
            stringnum.Clear();
            intnum.Clear();
        }
        public static class Globals//Global variable so it wont reset
        {
            public static int RunningNumber = 1;
        }
        static int Scoring(List<Card> River, Card c1, Card c2, Dictionary<int, int> PlayersHighestCard, List<string> stringnum, List<int> intnum)//Decides who will win the pot
        {
            //Lists to store the cards based on their suits
            List<string> spades = new List<string>();//Cards that are spades
            List<string> hearts = new List<string>();//Cards that are hearts
            List<string> clubs = new List<string>();//Cards that are clubs
            List<string> diamonds = new List<string>();//Cards that are diamonds
            List<string> num = new List<string>();//to store all the numbers that is out
            List<Card> Straight = new List<Card>();//List to store the straight list
            List<int> Pair = new List<int>();//store pairs
            List<int> Trips = new List<int>();//store cards that has appeared 3 times
            List<int> Quads = new List<int>();//store cards that has appeared 4 times
            List<int> FullHse = new List<int>();//store full house
            List<Card> flush = new List<Card>();//List to store the flush list
            foreach (var c in River)//foreach loop to categorise where the cards will go
            {
                if (c.CardSuit == "Spades") { spades.Add(c.CardNumber); }
                else if (c.CardSuit == "Hearts") { hearts.Add(c.CardNumber); }
                else if (c.CardSuit == "Diamonds") { diamonds.Add(c.CardNumber); }
                else if (c.CardSuit == "Clubs") { clubs.Add(c.CardNumber); }
                num.Add(c.CardNumber);
            }
            AddToSuit(c1, spades, hearts, clubs, diamonds, num);//adds the cards of the players to their respective suit list
            AddToSuit(c2, spades, hearts, clubs, diamonds, num);
            ChangeCardStringtoInt(num, stringnum, intnum);
            int HN = CheckStraight(num, stringnum, intnum);
            //checks if theres a straight. If there isn't it returns 0. If there is a straight it returns the highest number in the straight.
            CheckPairTripsQuadsFullHse(intnum, Pair, Trips, Quads, FullHse);
            Flush(spades, hearts, clubs, diamonds, flush);
            if ((Pair.Count == 0) && (Trips.Count == 0) && (Quads.Count == 1)) { return 3; }
            else if(FullHse.Count > 0) { return 4; }
            else if (flush.Count > 0) { return 5; }
            else if (HN != 0) { PlayersHighestCard.Add(Globals.RunningNumber, HN); Globals.RunningNumber++; return 6; }
            //if there is a straight, it add the highest card and a running number to a dictonary for comparison to other players straight
            else if ((Pair.Count == 0) && (Trips.Count == 1) && (Quads.Count == 0)) { return 7; }
            else if ((Pair.Count == 2) && (Trips.Count == 0) && (Quads.Count == 0)) { return 8; }
            else if ((Pair.Count == 1) && (Trips.Count == 0) && (Quads.Count == 0)) { return 9; }
            else { return 10; }
        }
        static bool StraightFlush(List<string> SuitList, List<Card> flush)
        {
            List<string> SuitListString = new List<string>();
            List<int> SuitListNum = new List<int>();
            ChangeCardStringtoInt(SuitList, SuitListString, SuitListNum);
            if(CheckStraight(SuitList, SuitListString, SuitListNum) == 0) { return false; }
            if()
        }
        static void Flush(List<string> spades, List<string> hearts, List<string> clubs, List<string> diamonds, List<Card> flush)
        {
            if (CheckFlush(spades, flush, "Spades") == true) { return; }
            else if (CheckFlush(hearts, flush, "Hearts") == true) { return; }
            else if (CheckFlush(clubs, flush, "Clubs") == true) { return; }
            else if (CheckFlush(diamonds, flush, "Diamonds") == true) { return; }
        }
        static bool CheckFlush(List<string> SuitList, List<Card> Flush, string suit)
        {
            int count = 0;
            for(int i = 0; i < SuitList.Count; i++)
            {
                count++;
            }
            if(count >= 5)
            {
                SuitList.Sort();
                SuitList.Reverse();
                for(int x = 0; x < 5; x++)
                {
                    Card f = new Card(SuitList[x], suit);
                    Flush.Add(f);
                }
                return true;
            }
            return false;
        }
        static void CheckPairTripsQuadsFullHse(List<int> intnum, List<int> Pair, List<int> Trips, List<int> Quads, List<int> FullHse)
        {
            for(int g = 0; g < intnum.Count; g++)
            {
                if (intnum[g] == 1) { intnum.Remove(1); }
            }
            for (int i = 0; i < intnum.Count; i++)
            {
                int count = 0;
                for (int x = 0; x < intnum.Count; x++)
                {
                    if (intnum[i] == intnum[x]) { count++; }
                }
                if (count == 2 && !(Pair.Contains(intnum[i])) && !(Trips.Contains(intnum[i])) && !(Quads.Contains(intnum[i]))) { Pair.Add(intnum[i]); }
                else if (count == 3 && !(Trips.Contains(intnum[i])) && !(Quads.Contains(intnum[i]))) { Trips.Add(intnum[i]); }
                else if (count == 4 && !(Quads.Contains(intnum[i]))) { Quads.Add(intnum[i]); }
            }
            if (Trips.Count == 2)
            {
                for (int t = 0; t < 3; t++) { FullHse.Add(Trips.Max()); }
                for (int p = 0; p < 2; p++) { FullHse.Add(Trips.Min()); }
            }
            if (Trips.Count > 0 && Pair.Count > 0)
            {
                for (int t = 0; t < 3; t++) { FullHse.Add(Trips[0]); }
                for (int p = 0; p < 2; p++) { FullHse.Add(Pair.Max()); }
            }
        }
        static int CheckStraight(List<string> num, List<string> stringnum, List<int> intnum){
            int count = 0;
            bool straight = false;
            intnum.Sort();
            intnum = intnum.Distinct().ToList();
            int Next = intnum[0];
            for(int v = 1; v<intnum.Count; v++)
            {
                if (count == 4) { straight = true;  break; }
                else if(CheckInFront(Next,intnum[v]) == true) { Next = intnum[v]; count++; }
                else if(CheckInFront(Next, intnum[v]) == false) { Next = intnum[v]; count=0; }
            }
            int index = intnum.IndexOf(Next);
            for (int i = index + 1; i < intnum.Count; i++)
            {
                if (Next + 1 == intnum[i]) { Next = intnum[i]; }
            }
            if (straight == true) { return Next; }
            else { return 0; }
        }
        static bool CheckInFront(int num1, int num2)
        {
            if(num2 == num1 + 1) { return true; }
            else { return false; }
        }
        static void ChangeCardStringtoInt(List<string> num, List<string> stringnum, List<int> intnum)
        {
            foreach (var n in num)
            {
                if (n == "J") { stringnum.Add("11"); }
                else if (n == "Q") { stringnum.Add("12"); }
                else if (n == "K") { stringnum.Add("13"); }
                else if (n == "A") { stringnum.Add("14"); stringnum.Add("1"); }
                else { stringnum.Add(n); }
            }
            foreach (var n in stringnum) { intnum.Add(int.Parse(n)); }
        }
        static void AddToSuit(Card c, List<string> spades, List<string> hearts, List<string> clubs, List<string> diamonds, List<string> num)
        {
            if (c.CardSuit == "Spades") { spades.Add(c.CardNumber); }
            else if (c.CardSuit == "Hearts") { hearts.Add(c.CardNumber); }
            else if (c.CardSuit == "Diamonds") { diamonds.Add(c.CardNumber); }
            else if (c.CardSuit == "Clubs") { clubs.Add(c.CardNumber); }
            num.Add(c.CardNumber);
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
        static void River(List<Card> River, List<Card> Deck)
        {
            for (int x = 0; x < 5; x++)
            {
                Card R = RandomCard(Deck);
                River.Add(R);
            }
        }
        static void Hands(List<Card> Deck, List<Card> PlayersCardsList, int players, List<Card> Dealercards)
        {
            Card dc1 = RandomCard(Deck);
            Card dc2 = RandomCard(Deck);
            Dealercards.Add(dc1);
            Dealercards.Add(dc2);
            for (int i = 0; i < players; i++)
            {
                Card pc1 = RandomCard(Deck);
                Card pc2 = RandomCard(Deck);
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
        static Card RandomCard(List<Card> Deck)
        {
            Random Rndm = new Random();
            Card Card1 = Deck[Rndm.Next(Deck.Count)];
            Deck.Remove(Card1);
            return Card1;
        } 
        static void AddToDeck(List<Card> Deck)
        {
            List<string> NonNumber = new List<string>();
            NonNumber.Add("A");
            NonNumber.Add("J");
            NonNumber.Add("Q");
            NonNumber.Add("K");
            for(int i = 2; i<11; i++)
            {
                string s = i.ToString();
                Card s1 = new Card("Spades",s);
                Card c1 = new Card("Clubs",s);
                Card d1 = new Card("Diamonds",s);
                Card h1 = new Card("Hearts",s);
                Deck.Add(s1);
                Deck.Add(c1);
                Deck.Add(d1);
                Deck.Add(h1);
            }
            for(int x = 0; x < 4; x++)
            {
                Card s1 = new Card("Spades", NonNumber[x]);
                Card c1 = new Card("Clubs", NonNumber[x]);
                Card d1 = new Card("Diamonds", NonNumber[x]);
                Card h1 = new Card("Hearts", NonNumber[x]);
                Deck.Add(s1);
                Deck.Add(c1);
                Deck.Add(d1);
                Deck.Add(h1);
            }
            
        }
    }
}
