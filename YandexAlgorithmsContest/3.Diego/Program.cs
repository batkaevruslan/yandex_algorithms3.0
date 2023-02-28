using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _3.Diego
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            int diegoCardsCount = int.Parse(sr.ReadLine()!);
            int[] diegoCards = sr.ReadLine()!.Split(' ').Distinct().Select(c => int.Parse(c)).OrderBy(x => x).ToArray();
            int friendsCount = int.Parse(sr.ReadLine()!);
            Friend[] friends = sr.ReadLine()!
                                             .Split(' ')
                                             .Select((c, index) => new Friend(NonDesiredMinCardNumber: int.Parse(c), OriginalIndex: index))
                                             .OrderBy(f => f.NonDesiredMinCardNumber)
                                             .ToArray();
            Dictionary<int, int> diegoCardByFriendIndex = new();
            int currentFriendSuitableCardsCount = 0;
            int currentDiegoCardIndex = 0;

            for (int j = 0; j < friendsCount; j++)
            {
                for (int i = currentDiegoCardIndex; i < diegoCards.Length; i++)
                {
                    if (friends[j].NonDesiredMinCardNumber > diegoCards[i])
                    {
                        currentFriendSuitableCardsCount++;
                        currentDiegoCardIndex = i + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                diegoCardByFriendIndex[friends[j].OriginalIndex] = currentFriendSuitableCardsCount;
            }

            StringBuilder sb = new();
            for (int i = 0; i < friendsCount; i++)
            {
                sb.AppendLine(diegoCardByFriendIndex[i].ToString());
            }

            File.WriteAllText("output.txt", sb.ToString());
        }

       /* public static int[] Sort(int[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards.Length - 1 - i; j++)
                {
                    if (cards[j + 1] < cards[j])
                    {
                        (cards[j + 1], cards[j]) = (cards[j], cards[j + 1]);
                    }
                }
            }
            return cards;
        }*/
        record struct Friend(int NonDesiredMinCardNumber, int OriginalIndex);
    }
}