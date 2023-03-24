using System;
using System.Linq;

namespace _26.CheapestPath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] dimensions = Console.ReadLine()
                                        .Trim()
                                        .Split(' ')
                                        .Select(int.Parse)
                                        .ToArray();
            int n = dimensions[0];
            int m = dimensions[1];
            int[][] field = new int[n][];
            for (int i = 0; i < n; i++)
            {
                field[i] = Console.ReadLine()
                                  .Trim()
                                  .Split(' ')
                                  .Select(int.Parse)
                                  .ToArray();
            }
            int[,] cost = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int upperCellCost = i == 0 ? int.MaxValue : cost[i - 1, j];
                    int leftCellCost = j == 0 ? int.MaxValue : cost[i, j - 1];
                    cost[i, j] = field[i][j] 
                                    + (i == 0 && j == 0
                                        ? 0
                                        : Math.Min(upperCellCost, leftCellCost));
                }
            }
            Console.WriteLine(cost[n - 1, m - 1]);
        }
    }
}