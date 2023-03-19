using System;
using System.IO;
using System.Linq;

namespace _24.TicketPurchase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using FileStream fs = File.OpenRead("input.txt");
            //using StreamReader sr = new StreamReader(fs);
            int n = int.Parse(Console.ReadLine());
            (int a, int b, int c)[] times = new (int, int, int)[n + 3];
            times[0] = (int.MaxValue, int.MaxValue, int.MaxValue);
            times[1] = (int.MaxValue, int.MaxValue, int.MaxValue);
            times[2] = (int.MaxValue, int.MaxValue, int.MaxValue);
            for (int i = 0; i < n; i++)
            {
                int[] input = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();
                times[3 + i] = (input[0], input[1], input[2]);
            }
            int result = GetMinimalPurchaseTime(times);
            Console.WriteLine(result);
        }

        private static int GetMinimalPurchaseTime((int a, int b, int c)[] times)
        {
            int[] bestPurchaseTimePerPerson = new int[times.Length];
            bestPurchaseTimePerPerson[0] = 0;
            bestPurchaseTimePerPerson[1] = 0;
            bestPurchaseTimePerPerson[2] = 0;
            for (int i = 3; i < times.Length; i++)
            {
                bestPurchaseTimePerPerson[i] = Math.Min(Math.Min(bestPurchaseTimePerPerson[i - 3] + times[i - 2].c,
                                                        bestPurchaseTimePerPerson[i - 2] + times[i - 1].b),
                                                        bestPurchaseTimePerPerson[i - 1] + times[i].a);
            }
            return bestPurchaseTimePerPerson[^1];
        }
    }
}