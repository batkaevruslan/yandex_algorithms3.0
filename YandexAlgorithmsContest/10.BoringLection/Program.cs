using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _10.BoringLection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);
            string word = sr.ReadLine();
            StringBuilder sb = new();
            var countPerLetters = GetCountPerLetter(word);
            foreach (var countPerLetter in countPerLetters.OrderBy(p => p.Key))
            {
                sb.AppendLine($"{countPerLetter.Key}: {countPerLetter.Value}");
            }

            File.WriteAllText("output.txt", sb.ToString());
        }

        private static Dictionary<char, double> GetCountPerLetter(string word)
        {
            Dictionary<char, double> countByLetter = new();
            for(int i = 0; i < word.Length; i++)
            {
                countByLetter.TryGetValue(word[i], out double count);
                countByLetter[word[i]] = count + (i + 1) * (word.Length - (double)i);
            }
           
            return countByLetter;
        }
    }
}