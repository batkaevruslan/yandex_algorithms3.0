using System;
using System.Collections.Generic;
using System.Linq;

namespace _25.Nails
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            int[] coordinates = Console.ReadLine()
                                        .Trim()
                                        .Split(' ')
                                        .Select(int.Parse)
                                        .OrderBy(x => x)
                                        .ToArray();
            Dictionary<int, ThreadLength> bestThreadLengthByNailNumber = new();
            int infiniteThread = 100000;
            bestThreadLengthByNailNumber[0] = new ThreadLength(WhenConnectedToPrevious: 0, WhenConnectedToNext: infiniteThread);
            bestThreadLengthByNailNumber[coordinates.Length + 1] = new ThreadLength(WhenConnectedToPrevious: infiniteThread, WhenConnectedToNext: 0);

            for(int nailNumber = 1; nailNumber <= coordinates.Length; nailNumber++)
            {
                int distanceToPrevious = nailNumber == 1 ? infiniteThread : coordinates[nailNumber -1] - coordinates[nailNumber-2];
                int distanceToNext = nailNumber == coordinates.Length ? infiniteThread   : coordinates[nailNumber] - coordinates[nailNumber - 1];
                int bestLengthWhenConnectedToPrevious = Math.Min(bestThreadLengthByNailNumber[nailNumber - 1].WhenConnectedToPrevious + distanceToPrevious,
                    bestThreadLengthByNailNumber[nailNumber - 1].WhenConnectedToNext);
                int bestLengthWhenConnectedToNext = Math.Min(bestThreadLengthByNailNumber[nailNumber - 1].WhenConnectedToPrevious + distanceToNext,
                    bestThreadLengthByNailNumber[nailNumber - 1].WhenConnectedToNext + distanceToNext);
                bestThreadLengthByNailNumber[nailNumber] = new ThreadLength(bestLengthWhenConnectedToPrevious, bestLengthWhenConnectedToNext);
            }

            Console.WriteLine(bestThreadLengthByNailNumber[coordinates.Length].WhenConnectedToPrevious);
        }

        record struct ThreadLength(int WhenConnectedToPrevious, int WhenConnectedToNext);
    }
}