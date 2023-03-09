using System;
using System.IO;

namespace _2.PrettyString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            double replacementCount = double.Parse(sr.ReadLine());
            string word = sr.ReadLine();

            int maxPrettieness = GetMaxPrettiness(replacementCount, word);
            File.WriteAllText("output.txt", maxPrettieness.ToString());
        }

        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static int GetMaxPrettiness(double replacementCount, string word)
        {
            int maxPrettieness = 0;
            foreach (char letter in alphabet)
            {
                double replacementLeft = replacementCount;
                int currentLetterMaxPrettieness = 0;
                int currentMaxPrettiness = 0;
                int i = 0, j = 0;
                while (i < word.Length && j < word.Length)
                {
                    if (replacementLeft > 0)
                    {
                        if (word[j] == letter)
                        {
                            currentMaxPrettiness++;
                            j++;
                        }
                        else
                        {
                            currentMaxPrettiness++;
                            j++;
                            replacementLeft--;
                        }
                    }
                    else
                    {
                        if (word[j] == letter)
                        {
                            currentMaxPrettiness++;
                            j++;
                        }
                        else if (word[i] == letter)
                        {
                            currentMaxPrettiness--;
                            i++;
                        }
                        else
                        {
                            currentMaxPrettiness--;
                            replacementLeft++;
                            i++;
                        }
                    }
                    currentLetterMaxPrettieness = Math.Max(currentLetterMaxPrettieness, currentMaxPrettiness);
                }
                maxPrettieness = Math.Max(currentLetterMaxPrettieness, maxPrettieness);
            }
            return maxPrettieness;
        }
    }
}