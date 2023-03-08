using System.IO;
using System.Linq;
using System.Text;

namespace _9.SumInRectangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            var firstLine = sr.ReadLine()!.Split(' ').Select(int.Parse).ToArray();
            int N = firstLine[0];
            int M = firstLine[1];
            int K = firstLine[2];

            double[,] matrix = new double[N, M + 1];
            for (int i = 0; i < N; i++)
            {
                matrix[i, 0] = 0;
                double[] line = sr.ReadLine()!.Split(' ').Where(x => x != "").Select(double.Parse).ToArray();
                for (int j = 0; j < M; j++)
                {
                    matrix[i, j + 1] = matrix[i, j] + line[j];
                }
            }

            StringBuilder sb = new();
            for (int i = 0; i < K; i++)
            {
                double sum = 0;
                (int x1, int y1, int x2, int y2) = ParseCoordinates(sr.ReadLine()!);
                for (int row = x1 - 1; row < x2; row++)
                {
                    sum += matrix[row, y2] - matrix[row, y1 - 1];
                }
                sb.AppendLine(sum.ToString());
            }

            File.WriteAllText("output.txt", sb.ToString());
        }

        private static (int x1, int y1, int x2, int y2) ParseCoordinates(string s)
        {
            var coordinates = s.Split(' ').Select(int.Parse).ToArray();
            return (coordinates[0], coordinates[1], coordinates[2], coordinates[3]);
        }
    }
}