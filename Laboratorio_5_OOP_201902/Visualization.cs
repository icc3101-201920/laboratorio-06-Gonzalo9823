using Laboratorio_5_OOP_201902.Cards;
using System;
using System.Collections.Generic;

namespace Laboratorio_5_OOP_201902
{
   static class Visualization
    {

        public void ShowHand(Hand hand)
        {
            int id = 0;

            foreach (Card card in hand.Cards)
            {
                Console.WriteLine("Hand:");

                if (card.GetType().Name == nameof(CombatCard))
                {
                    // Rojo
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"|({id}) {card.Name} ({card.Type}): ({card.AttackPoints})|");
                }
                else
                {
                    // Azul
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"|({id}) {card.Name} ({card.Type})|");
                }
                id++;
                Console.ResetColor();
            }
        }

        public void ShowDecks(List<Deck> decks)
        {
            Console.WriteLine("Select one Deck:");

            for(int i = 0; i < decks.Count(); i++)
            {
                Console.WriteLine($"({i}) Deck {i + 1}");
            }
        }

        public void ShowCaptains(List<SpecialCard> captains)
        {
            Console.WriteLine("Select one captain:");

            id = 0;
            foreach(SpecialCard card in captains)
            {
                Console.WriteLine($"({id}) {card.Name}: {card.Effect}");
                id++;
            }
        }

        public int GetUserInput(int maxInput, bool stopper = false)
        {
            bool shouldGetUserInput = true;
            // Para que si tiene un error el usuario pueda escribir de nuevo
            while (shouldGetUserInput)
            {
                int userInput = Convert.ToInt32(Console.ReadLine());

                if(stopper)
                {
                    if(userInput <= maxInput && userInput >= -1)
                    {
                        return userInput;
                    } else
                    {
                        ConsoleError($"Number must be between -1 and {maxInput}");
                    }
                } else
                {
                    if (userInput <= maxInput && userInput >= 0)
                    {
                        return userInput;
                    }
                    else
                    {
                        ConsoleError($"Number must be between 0 and {maxInput}");
                    }
                }
            }
        }

        public void ConsoleError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void ShowProgramMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void ShowListOptions(List<string> options, string message = null)
        {
            if(message != null)
            {
                Console.WriteLine(message);
            }

            int i = 0;
            foreach(string option in options)
            {
                Console.WriteLine($"({i}) {option}");
                i++;
            }
        }

        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
