using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _38.Fleas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            int fieldWidth = input[0];
            int fieldHeight = input[1];
            int foodCellX = input[2];
            int foodCellY = input[3];
            int fleaCount = input[4];
            int[,] field = new int[fieldWidth, fieldHeight];
            for (int f = 0; f < fleaCount; f++)
            {
                int[] fleaCoordinates = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
                field[fleaCoordinates[0] - 1, fleaCoordinates[1] - 1] = 1;
            }

            int[,] cellWithPathLength = BuildReachabilityMap(fieldWidth, fieldHeight, foodCellX - 1, foodCellY - 1);

            int result = GetTotalPathLength(fieldWidth, fieldHeight, field, cellWithPathLength);

            Console.WriteLine(result);
        }

        private static int GetTotalPathLength(int fieldWidth, int fieldHeight, int[,] field, int[,] cellWithPathLength)
        {
            int pathLengthSum = 0;
            for (int i = 0; i < fieldWidth; i++)
            {
                for (int j = 0; j < fieldHeight; j++)
                {
                    if (field[i, j] == 1)
                    {
                        if (cellWithPathLength[i, j] > -1)
                        {
                            pathLengthSum += cellWithPathLength[i, j];
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            return pathLengthSum;
        }

        private static int[,] BuildReachabilityMap(int fieldWidth, int fieldHeight, int foodCellX, int FoodCellY)
        {
            int[,] cellWithPathLength = new int[fieldWidth, fieldHeight];
            for (int i = 0; i < fieldWidth; i++)
            {
                for (int j = 0; j < fieldHeight; j++)
                {
                    cellWithPathLength[i, j] = -1;
                }
            }
            cellWithPathLength[foodCellX, FoodCellY] = 0;
            Queue<(int x, int y)> cellsToVisit = new Queue<(int x, int y)>();
            cellsToVisit.Enqueue((foodCellX, FoodCellY));
            VisitReachableCells(cellWithPathLength, cellsToVisit, fieldWidth, fieldHeight);
            return cellWithPathLength;
        }

        private static void VisitReachableCells(int[,] cellWithPathLength, Queue<(int x, int y)> cellsToVisit, int fieldWidth, int fieldHeight)
        {
            (int cellX, int cellY) cellToVisit = cellsToVisit.Dequeue();
            int[] neigbourXShift = new int[] { -2, -1, 1, 2, 2, 1, -1, -2 };
            int[] neighbourYShift = new int[] { -1, -2, -2, -1, 1, 2, 2, 1 };
            for(int neighbour = 0; neighbour < neigbourXShift.Length; neighbour++)
            {
                int neighbourX = cellToVisit.cellX + neigbourXShift[neighbour];
                int neighbourY = cellToVisit.cellY + neighbourYShift[neighbour];
                if(neighbourX >= 0 && neighbourX < fieldWidth 
                    && neighbourY >= 0 && neighbourY < fieldHeight
                    && cellWithPathLength[neighbourX, neighbourY] == -1)
                {
                    cellWithPathLength[neighbourX, neighbourY] = cellWithPathLength[cellToVisit.cellX, cellToVisit.cellY] + 1;
                    cellsToVisit.Enqueue(new(neighbourX, neighbourY));
                }
            }
            if(cellsToVisit.Count > 0)
            {
                VisitReachableCells(cellWithPathLength, cellsToVisit, fieldWidth, fieldHeight);
            }
        }
    }
}