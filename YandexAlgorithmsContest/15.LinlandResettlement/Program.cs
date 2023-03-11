using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _15.LinlandResettlement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            sr.ReadLine();
            (int index, int costOfLife)[] costOfLifePetTown = (sr.ReadLine() ?? "").Trim()
                .Split(" ")
                .Select((s, i) => (i, int.Parse(s) ))
                .ToArray();

            int[] targetTown = new int[costOfLifePetTown.Length];
            Stack<(int index, int costOfLife)> stack = new();
            stack.Push(costOfLifePetTown[0]);
            for(int i = 1; i < costOfLifePetTown.Length; i++)
            {
                while(stack.Count > 0 && stack.Peek().costOfLife > costOfLifePetTown[i].costOfLife) {
                    targetTown[stack.Pop().index] = costOfLifePetTown[i].index;
                }
                stack.Push(costOfLifePetTown[i]);
            }
            while (stack.Count > 0)
            {
                targetTown[stack.Pop().index] = -1;
            }

            File.WriteAllText("output.txt", string.Join(" ", targetTown));
        }
    }
}