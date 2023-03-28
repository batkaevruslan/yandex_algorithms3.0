using System;
using System.Collections.Generic;
using System.Linq;

namespace _30.LCS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int seq1Size = int.Parse(Console.ReadLine());
            int[] seq1 = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            int seq2Size = int.Parse(Console.ReadLine());
            int[] seq2 = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();

            int[] result = FindLcs(seq1, seq2);

            Console.WriteLine(string.Join(" ", result));
        }

        private static int[] FindLcs(int[] seq1, int[] seq2)
        {
            int[,] dp = new int[seq1.Length + 1, seq2.Length + 1];

            for(int i = 1; i < seq1.Length + 1; i++)
            {
                for(int j = 1; j < seq2.Length + 1; j++)
                {
                    if (seq1[i - 1] == seq2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    } else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            int x = seq1.Length;
            int y = seq2.Length;
            List<int> result = new();
            while(x > 0 && y > 0)
            {
                if (dp[x, y] == dp[x-1, y])
                {
                    x--;
                } else if (dp[x, y -1] == dp[x, y])
                {
                    y--;
                } else
                {
                    result.Add(seq1[x - 1]);
                    x--;
                    y--;
                }
            }
            result.Reverse();
            return result.ToArray();
        }
    }
}