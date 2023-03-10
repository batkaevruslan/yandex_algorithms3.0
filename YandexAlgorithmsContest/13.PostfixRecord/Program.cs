using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _13.PostfixRecord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            string[] sequence = (sr.ReadLine() ?? "").Trim().Split(" ").ToArray();
            Stack<int> stack = new();

            foreach (string element in sequence)
            {
                switch (element)
                {
                    case "+": stack.Push(stack.Pop() + stack.Pop()); break;
                    case "-": stack.Push(-stack.Pop() + stack.Pop()); break;
                    case "*": stack.Push(stack.Pop() * stack.Pop()); break;
                    case "/": stack.Push(1 / stack.Pop() * stack.Pop()); break;
                    default: stack.Push(int.Parse(element)); break;
                }
            }

            File.WriteAllText("output.txt", stack.Pop().ToString());
        }
    }
}