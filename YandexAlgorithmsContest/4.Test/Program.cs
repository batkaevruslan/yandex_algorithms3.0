using System;
using System.IO;

namespace _4.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            double pupilCount = double.Parse(sr.ReadLine()!);
            double testCount = double.Parse(sr.ReadLine()!);
            double petyaRow = double.Parse(sr.ReadLine()!);
            int petyaColumn = int.Parse(sr.ReadLine()!);

            string result = ProblemSolver.FindPlaceForVasya(pupilCount, testCount, petyaRow, petyaColumn);
            
            File.WriteAllText("output.txt", result);
        }
        public static class ProblemSolver
        {

            public static string FindPlaceForVasya(double pupilCount, double testCount, double petyaRow, int petyaColumn)
            {
                double petyaPlace = (petyaRow - 1) * 2 + petyaColumn;

                double bestPlaceBehind = petyaPlace + testCount;
                double bestPlaceInFront = petyaPlace - testCount;
                double possibleVasyaPlace = bestPlaceBehind;
                if (bestPlaceBehind > pupilCount)
                {
                    if (bestPlaceInFront <= 0)
                    {
                        return "-1";
                    }
                    possibleVasyaPlace = bestPlaceInFront;
                }
                else if (bestPlaceInFront > 0 
                    && (petyaRow - GetRowForPlace(bestPlaceInFront)) < (GetRowForPlace(bestPlaceBehind) - petyaRow))
                {
                    possibleVasyaPlace = bestPlaceInFront;
                }

                double vasyaRow = GetRowForPlace(possibleVasyaPlace);
                double vasyaColumn = possibleVasyaPlace % 2 == 0 ? 2 : 1;
                return $"{vasyaRow} {vasyaColumn}";

            }

            private static double GetRowForPlace(double placeNumber)
            {
                return placeNumber % 2 == 0 ? Math.Floor(placeNumber / 2) : Math.Floor(placeNumber / 2) + 1;
            }
        }
    }
}