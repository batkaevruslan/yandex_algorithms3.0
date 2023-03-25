using System;
using System.Linq;

namespace _28.KnightMove
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

            int result = GetPossiblePathCount(n, m);

            Console.WriteLine(result);
        }

        private static int GetPossiblePathCount(int n, int m)
        {
            int[,] squaresScore = new int[n + 2, m + 2];
            squaresScore[2, 2] = 1;
            int[] originRowShifts = new int[]{ -2, -1};
            int[] originColumnShifts = new int[]{ -1, -2};
            int possibleOriginCount = 2;
            for (int i = 2; i < n + 2; i++)
            {
                for (int j = 2; j < m + 2; j++)
                {
                    for(int k = 0; k < possibleOriginCount; k++){
                        int originScore = squaresScore[i + originRowShifts[k], j + originColumnShifts[k]];
                        if (originScore >= 0)
                        {
                            if (squaresScore[i, j] < 0)
                            {
                                squaresScore[i, j] = originScore;
                            }
                            else
                            {
                                squaresScore[i, j] += originScore;
                            }
                        }
                    }
                }
            }
            return squaresScore[n + 1, m + 1];
        }
    }
}