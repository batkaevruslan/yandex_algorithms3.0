using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _14.TrainSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            sr.ReadLine();
            int[] trains = (sr.ReadLine() ?? "").Trim().Split(" ").Select(int.Parse).ToArray();
            Stack<int> deadEnd = new();
            int lastSortedTraint = 0;
            foreach(int train in trains)
            {
                if (train == lastSortedTraint + 1)
                {
                    lastSortedTraint = train;
                    while (deadEnd.Count > 0 && deadEnd.Peek() == lastSortedTraint + 1)
                    {
                        lastSortedTraint = deadEnd.Pop();
                    }
                }
                else
                {
                    deadEnd.Push(train);
                }
            }

            File.WriteAllText("output.txt", deadEnd.Count == 0 ? "YES" : "NO");
        }
    }
}