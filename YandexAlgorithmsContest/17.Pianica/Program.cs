using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _17.Pianica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            Queue<int> firstDeck = new Queue<int>(sr.ReadLine().Trim().Split(" ").Select(int.Parse));
            Queue<int> secondDeck = new Queue<int>(sr.ReadLine().Trim().Split(" ").Select(int.Parse));

            (string? player, int? round) winner = GetWinner(firstDeck, secondDeck);
            if (winner.player == null)
            {
                File.WriteAllText("output.txt", "botva");
            }
            else
            {
                File.WriteAllText("output.txt", $"{winner.player} {winner.round}");
            }
        }

        private static (string? player, int? round) GetWinner(Queue<int> firstDeck, Queue<int> secondDeck)
        {
            for (int i = 1; i <= Math.Pow(10, 6); i++)
            {
                (int firstCard, int secondCard) = (firstDeck.Dequeue(), secondDeck.Dequeue());
                if ((firstCard == 0 && secondCard == 9)
                    || (firstCard > secondCard && (firstCard != 9 || secondCard != 0)))
                {
                    firstDeck.Enqueue(firstCard);
                    firstDeck.Enqueue(secondCard);
                }
                else
                {
                    secondDeck.Enqueue(firstCard);
                    secondDeck.Enqueue(secondCard);
                }

                if (firstDeck.Count == 0)
                {
                    return ("second", i);
                }
                if (secondDeck.Count == 0)
                {
                    return ("first", i);
                }
            }
            return (null, null);
        }
    }
}