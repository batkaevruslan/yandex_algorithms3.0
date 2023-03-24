using System;
using System.Linq;

namespace _27.MostExpensivePath
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
            Cell[,] cellsWithSum = new Cell[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        cellsWithSum[i, j] = new Cell(BestSum: field[i][j], DirectionFromPreviousCell: "");
                    }
                    else
                    {
                        int upperCellCost = i == 0 ? int.MinValue : cellsWithSum[i - 1, j].BestSum;
                        int leftCellCost = j == 0 ? int.MinValue : cellsWithSum[i, j - 1].BestSum;
                        if(upperCellCost > leftCellCost)
                        {
                            cellsWithSum[i, j] = new Cell(field[i][j] + upperCellCost, DirectionFromPreviousCell: "D ");
                        } else
                        {
                            cellsWithSum[i, j] = new Cell(field[i][j] + leftCellCost, DirectionFromPreviousCell: "R ");
                        }                        
                    }
                }
            }
            Console.WriteLine(cellsWithSum[n - 1, m - 1].BestSum);
            int a = n - 1;
            int b = m - 1;
            string path = "";
            while (a > 0 || b > 0)
            {
                string directionFromPreviousCell = cellsWithSum[a, b].DirectionFromPreviousCell;
                path = path.Insert(0, directionFromPreviousCell);
                if(directionFromPreviousCell == "D ")
                {
                    a--;
                } else
                {
                    b--;
                }
            }
            Console.WriteLine(path);
        }

        record struct Cell(int BestSum, string DirectionFromPreviousCell);
    }
}