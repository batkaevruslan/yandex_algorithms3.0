using System.IO;
using System.Linq;

namespace _8.MinimalRectangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            byte pointCount = byte.Parse(sr.ReadLine()!);


            (double leftMost, double highest) = ParseCoordinates(sr.ReadLine()!);
            (double rightMost, double lowest) = (leftMost, highest);

            for (int i = 1;  i < pointCount; i++)
            {
                (double x, double y) = ParseCoordinates(sr.ReadLine() !);
                if(leftMost > x)
                {
                    leftMost = x;
                }
                if(rightMost < x)
                {
                    rightMost = x;
                }
                if(highest < y)
                {
                    highest = y;
                }
                if(lowest > y)
                {
                    lowest = y;
                }
            }

            File.WriteAllText("output.txt", $"{leftMost} {lowest} {rightMost} {highest}");
        }

        private static (double x, double y) ParseCoordinates(string s)
        {
            var coordinates = s.Split(' ').Select(double.Parse).ToArray();
            return (coordinates[0], coordinates[1]);
        }
    }
}