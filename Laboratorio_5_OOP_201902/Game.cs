using Laboratorio_5_OOP_201902.Cards;
using Laboratorio_5_OOP_201902.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Laboratorio_5_OOP_201902
{
    public class Game
    {
        //Atributos
        private Player[] players;
        private Player activePlayer;
        private List<Deck> decks;
        private List<SpecialCard> captains;
        private Board boardGame;

        //Constructor
        public Game()
        {
            decks = new List<Deck>();
            captains = new List<SpecialCard>();

            Player player1 = new Player();
            Player player2 = new Player();

            players[0] = player1;
            players[1] = player2;

            Random rnd = new Random();
            int randomNumber = rnd.Next(0, 1);

            activePlayer = players[randomNumber];

            players[0].Board = boardGame;
            players[1].Board = boardGame;

            AddDecks();
            AddCaptains();
        }
        //Propiedades
        public Player[] Players
        {
            get
            {
                return this.players;
            }
        }
        public Player ActivePlayer
        {
            get
            {
                return this.activePlayer;
            }
            set
            {
                activePlayer = value;
            }
        }
        public List<Deck> Decks
        {
            get
            {
                return this.decks;
            }
        }
        public List<SpecialCard> Captains
        {
            get
            {
                return this.captains;
            }
        }
        public Board BoardGame
        {
            get
            {
                return this.boardGame;
            }
        }

        //Metodos
        public bool CheckIfEndGame()
        {
            if (players[0].LifePoints == 0 || players[1].LifePoints == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetWinner()
        {
            if (players[0].LifePoints == 0 && players[1].LifePoints > 0)
            {
                return 1;
            }
            else if (players[1].LifePoints == 0 && players[0].LifePoints > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public void Play()
        {
            Visualization.ShowProgramMessage("Player 1 select Deck and captain:");
            players[0].Deck = Visualization.ShowDecks(decks);
            players[0].ChooseCaptainCard(Visualization.ShowCaptains(captains));

            players[0].FirstHand();
            Visualization.ShowHand(players[0].Hand);

            bool playerOneSelecting = true;
            int numberOfTimesPlayerOne = 0;
            while (playerOneSelecting)
            {
                Visualization.ShowListOptions(new List<string> { "ShowListOptions", "Pass" }, "Change 3 cards or ready to play:");
                int userOption = Int32.Parse(System.Console.ReadLine());

                if(userOption == 1)
                {
                    playerOneSelecting = false;
                } else
                {
                    if(numberOfTimesPlayerOne < 3)
                    {
                        System.Console.WriteLine("Input the number of the card you wan't to change. To stop write -1");
                        int userCard = Int32.Parse(System.Console.ReadLine());

                        if(userCard == -1)
                        {
                            playerOneSelecting = false;
                        } else
                        {
                            players[0].Hand.Cards.RemoveAt(userCard);


                            Random rnd = new Random();
                            int randomNumber = rnd.Next(0, players[0].Deck.Cards.Count);

                            players[0].DrawCard(randomNumber);
                        }

                    }
                    else
                    {
                        Visualization.ConsoleError("Ya no puedes cambiar mas cartas");
                        playerOneSelecting = false;
                    }
                }
            }

            Visualization.ClearConsole();

            Visualization.ShowProgramMessage("Player 2 select Deck and captain:");
            players[1].Deck = Visualization.ShowDecks(decks);
            players[1].ChooseCaptainCard(Visualization.ShowCaptains(captains));

            players[1].FirstHand();
            Visualization.ShowHand(players[1].Hand);

            bool playerTwoSelecting = true;
            int numberOfTimesPlayerTwo = 0;
            while (playerTwoSelecting)
            {
                Visualization.ShowListOptions(new List<string> { "ShowListOptions", "Pass" }, "Change 3 cards or ready to play:");
                int userOption = Int32.Parse(System.Console.ReadLine());

                if (userOption == 1)
                {
                    playerTwoSelecting = false;
                }
                else
                {
                    if (numberOfTimesPlayerTwo < 3)
                    {
                        System.Console.WriteLine("Input the number of the card you wan't to change. To stop write -1");
                        int userCard = Int32.Parse(System.Console.ReadLine());

                        if (userCard == -1)
                        {
                            playerTwoSelecting = false;
                        }
                        else
                        {
                            players[1].Hand.Cards.RemoveAt(userCard);


                            Random rnd = new Random();
                            int randomNumber = rnd.Next(0, players[1].Deck.Cards.Count);

                            players[1].DrawCard(randomNumber);
                        }

                    }
                    else
                    {
                        Visualization.ConsoleError("Ya no puedes cambiar mas cartas");
                        playerTwoSelecting = false;
                    }
                }
            }
            Visualization.ClearConsole();
        }
        public void AddDecks()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Files\Decks.txt";
            StreamReader reader = new StreamReader(path);
            int deckCounter = 0;
            List<Card> cards = new List<Card>();


            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string [] cardDetails = line.Split(",");

                if (cardDetails[0] == "END")
                {
                    decks[deckCounter].Cards = new List<Card>(cards);
                    deckCounter += 1;
                }
                else
                {
                    if (cardDetails[0] != "START")
                    {
                        if (cardDetails[0] == nameof(CombatCard))
                        {
                            cards.Add(new CombatCard(cardDetails[1], (EnumType) Enum.Parse(typeof(EnumType),cardDetails[2]), cardDetails[3], Int32.Parse(cardDetails[4]), bool.Parse(cardDetails[5])));
                        }
                        else
                        {
                            cards.Add(new SpecialCard(cardDetails[1], (EnumType)Enum.Parse(typeof(EnumType), cardDetails[2]), cardDetails[3]));
                        }
                    }
                    else
                    {
                        decks.Add(new Deck());
                        cards = new List<Card>();
                    }
                }

            }
            
        }
        public void AddCaptains()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Files\Captains.txt";
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] cardDetails = line.Split(",");
                captains.Add(new SpecialCard(cardDetails[1], (EnumType)Enum.Parse(typeof(EnumType), cardDetails[2]), cardDetails[3]));
            }
        }
    }
}
