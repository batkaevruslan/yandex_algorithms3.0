using System;
using System.Collections.Generic;
using System.IO;

namespace _12.Brackets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            string sequence = sr.ReadLine() ?? "";
            Stack<char> stack = new();
            foreach (char c in sequence)
            {
                if(stack.Count > 0 && (c == ')' || c == ']' || c == '}'))
                {
                    var lastChar = stack.Peek();
                    char oppositeBracket = c switch
                    {
                        ')' => '(',
                        ']' => '[',
                        '}' => '{',
                        _ => throw new NotImplementedException()
                    };
                    if(lastChar == oppositeBracket )
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(c);
                    }
                }
                else
                {
                    stack.Push(c);
                }
            }

            File.WriteAllText("output.txt", stack.Count > 0 ? "no" : "yes");
        }
    }
}