using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _5.GoodString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            byte letterCount = byte.Parse(sr.ReadLine()!);
            double[] countPerLetter = new double[letterCount];
            for (int i = 0; i < letterCount; i++)
            {
                countPerLetter[i] = double.Parse(sr.ReadLine()!);
            }
            var result = ProblemSolver.GetGoodness(0, countPerLetter).ToString();
            File.WriteAllText("output.txt", result);
        }

        public class ProblemSolver
        {
            internal static double GetGoodness(double usedLetters, double[] countPerLetter)
            {
                double minLetterCountInCurrentRange = countPerLetter.Min();
                double goodness = (minLetterCountInCurrentRange - usedLetters) * (countPerLetter.Length - 1);
                List<double> countPerLetterList = countPerLetter.ToList();
                int lastStartIndex = 0;
                int lastEndIndex = 0;
                int nextMinLetterIndex = countPerLetterList.IndexOf(minLetterCountInCurrentRange);
                while (nextMinLetterIndex > -1)
                {
                    if (nextMinLetterIndex > lastStartIndex)
                    {
                        goodness += GetGoodness(minLetterCountInCurrentRange, countPerLetter[lastStartIndex..nextMinLetterIndex]);
                    }
                    lastStartIndex = nextMinLetterIndex + 1;
                    lastEndIndex = nextMinLetterIndex;
                    nextMinLetterIndex = countPerLetterList.IndexOf(minLetterCountInCurrentRange, lastStartIndex);
                }
                if (lastEndIndex + 1 < countPerLetter.Length - 1)
                {
                    goodness += GetGoodness(minLetterCountInCurrentRange, countPerLetter[(lastEndIndex + 1)..]);
                }
                return goodness;
            }
        }
    }
}