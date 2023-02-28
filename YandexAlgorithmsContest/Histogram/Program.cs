using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Histogram
{
    // https://contest.yandex.ru/contest/45468/problems/1/
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> countByCharacter = ReadSymbols("input.txt");
            char[] characters = countByCharacter.Keys.ToArray();
            Sort(characters);
            int maxCount = countByCharacter.Values.Max();
            string result = GetResult(countByCharacter, characters, maxCount);
            File.WriteAllText("output.txt", result);
        }

        private static string GetResult(Dictionary<char, int> countByCharacter, char[] sortedCharacters, int maxCount)
        {
            StringBuilder sb = new();
            for (int i = maxCount; i > 0; i--)
            {
                foreach (char character in sortedCharacters)
                {
                    sb.Append(countByCharacter[character] < i ? " " : "#");
                }
                sb.AppendLine();
            }
            foreach (char character in sortedCharacters)
            {
                sb.Append(character);
            }
            return sb.ToString();
        }

        private static void Sort(char[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                for (int j = 0; j < chars.Length - i - 1; j++)
                {
                    if (chars[j + 1] < chars[j])
                    {
                        (chars[j], chars[j + 1]) = (chars[j + 1], chars[j]);
                    }
                }
            }
        }

        private static Dictionary<char, int> ReadSymbols(string filePath)
        {
            Dictionary<char, int> countByCharacter = new();
            foreach (var line in File.ReadLines(filePath))
            {
                foreach (char character in line)
                {
                    if (character == ' ')
                    {
                        continue;
                    }
                    if (!countByCharacter.TryGetValue(character, out int count))
                    {
                        countByCharacter[character] = 1;
                    }
                    else
                    {
                        countByCharacter[character] = count + 1;
                    }
                }
            }
            return countByCharacter;
        }
    }
}
