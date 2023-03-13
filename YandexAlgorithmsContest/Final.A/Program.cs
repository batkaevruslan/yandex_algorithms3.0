using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Final.A
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);
            StringBuilder sb = new();

            Stack<(int goodId, double goodCount)> stack = new();
            int n = int.Parse(sr.ReadLine());
            Dictionary<string, double> vagonCountByGoodType = new();
            Dictionary<string, int> goodIdByGoodType = new();
            Dictionary<int, string> goodTypeByGoodId = new();
            for (int i = 0; i < n; i++)
            {
                string[] words = sr.ReadLine()!.Trim().Split(" ").ToArray();
                if (words[0] == "add")
                {
                    string goodType = words[2];
                    if (!goodIdByGoodType.TryGetValue(goodType, out int goodId))
                    {
                        goodId = (int)goodIdByGoodType.Count;
                        goodIdByGoodType.Add(goodType, goodId);
                        goodTypeByGoodId.Add(goodId, goodType);
                    }
                    else
                    {
                        goodId = goodIdByGoodType[goodType];
                    }
                    double count = double.Parse(words[1]);
                    stack.Push((goodId, count));
                    if (!vagonCountByGoodType.ContainsKey(goodType))
                    {
                        vagonCountByGoodType[goodType] = 0;
                    }

                    vagonCountByGoodType[goodType] = vagonCountByGoodType[goodType] + count;

                }
                if (words[0] == "delete")
                {
                    double countToRemove = double.Parse(words[1]);
                    while(countToRemove > 0)
                    {
                        (int goodId, double count) = stack.Pop();
                        string goodType = goodTypeByGoodId[goodId];
                        if (countToRemove >= count)
                        {
                            vagonCountByGoodType[goodType] = vagonCountByGoodType[goodType] - count;
                            countToRemove -= count;
                        } else
                        {
                            vagonCountByGoodType[goodType] = vagonCountByGoodType[goodType] - countToRemove;
                            stack.Push((goodId, count - countToRemove));
                            countToRemove = 0;
                        }
                    }
                }
                if (words[0] == "get")
                {
                    vagonCountByGoodType.TryGetValue(words[1], out double count);
                    Console.WriteLine(count.ToString());
                    //sb.AppendLine(count.ToString());
                }
            }

            //File.WriteAllText("output.txt", sb.ToString());
        }

    }
}