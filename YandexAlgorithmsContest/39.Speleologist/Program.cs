using System;
using System.Collections;
using System.Collections.Generic;

namespace _39.Speleologist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int spaceSize = int.Parse(Console.ReadLine());

            int startX = -1;
            int startY = -1;
            int startZ = -1;
            int[,,] space = new int[spaceSize, spaceSize, spaceSize];
            for (int z = 0; z < spaceSize; z++)
            {
                Console.ReadLine();
                for (int x = 0; x < spaceSize; x++)
                {
                    string row = Console.ReadLine();
                    for (int y = 0; y < spaceSize; y++)
                    {
                        char symbol = row[y];
                        if (symbol == 'S')
                        {
                            startX = x;
                            startY = y;
                            startZ = z;
                        }
                        else if (symbol == '#')
                        {
                            space[x, y, z] = -2;
                        }
                        else
                        {
                            space[x, y, z] = -1;
                        }
                    }
                }
            }

            int result = FindShortestPath(space, spaceSize, startX, startY, startZ);

            Console.WriteLine(result);
        }

        private static int FindShortestPath(int[,,] space, int spaceSize, int startX, int startY, int startZ)
        {
            Queue<(int x, int y, int z)> cellsToVisit = new Queue<(int x, int y, int z)>();
            cellsToVisit.Enqueue(new(startX, startY, startZ));
            space[startX, startY, startZ] = 0;
            VisitCells(space, cellsToVisit, spaceSize);

            int shortestPath = int.MaxValue;
            for (int x = 0; x < spaceSize; x++)
            {
                for (int y = 0; y < spaceSize; y++)
                {
                    if (space[x, y, 0] >= 0)
                    {
                        shortestPath = Math.Min(shortestPath, space[x, y, 0]);
                    }
                }
            }
            return shortestPath;
        }

        private static void VisitCells(int[,,] space, Queue<(int x, int y, int z)> cellsToVisit, int spaceSize)
        {
            (int X, int Y, int Z) cell = cellsToVisit.Dequeue();
            int[] neighbourXShifts = new int[] { 0, 0, 1, -1, 0, 0 };
            int[] neighbourYShifts = new int[] { 1, -1, 0, 0, 0, 0 };
            int[] neighbourZShifts = new int[] { 0, 0, 0, 0, 1, -1 };
            for (int i = 0; i < 6; i++)
            {
                int neighbourX = cell.X + neighbourXShifts[i];
                int neighbourY = cell.Y + neighbourYShifts[i];
                int neighbourZ = cell.Z + neighbourZShifts[i];
                if (neighbourX >= 0 && neighbourX < spaceSize
                    && neighbourY >= 0 && neighbourY < spaceSize
                    && neighbourZ >= 0 && neighbourZ < spaceSize
                    && space[neighbourX, neighbourY, neighbourZ] == -1)
                {
                    cellsToVisit.Enqueue(new(neighbourX, neighbourY, neighbourZ));
                    space[neighbourX, neighbourY, neighbourZ] = space[cell.X, cell.Y, cell.Z] + 1;
                }
            }
            if (cellsToVisit.Count > 0)
            {
                VisitCells(space, cellsToVisit, spaceSize);
            }
        }
    }
}