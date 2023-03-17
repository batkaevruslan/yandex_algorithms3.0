using System;
using System.Linq;

namespace _22.Grasshopper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            int numberOfCells = input[0];
            int longestHop = input[1];
            int result = GetPossibleHopCount(numberOfCells, longestHop);
            Console.WriteLine(result);
        }

        public static int GetPossibleHopCount(int numberOfCells, int longestHop)
        {
            int[] dp = new int[Math.Max(2, numberOfCells)];
            dp[0] = 1;
            dp[1] = 1;
            for (int i = 2; i < numberOfCells; i++)
            {
                for (int j = Math.Max(1, i - longestHop); j < i; j++)
                {
                    dp[i] += dp[j];
                }
                if (i <= longestHop)
                {
                    dp[i]++;
                }
                
            }

            return dp[numberOfCells - 1];

        }
    }
}