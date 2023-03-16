using System;
using System.IO;
using System.Linq;

namespace _20.HeapSort
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);
            sr.ReadLine();
            double[] array = sr.ReadLine().Trim().Split(" ").Select(double.Parse).ToArray();
            //int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            //double[] array = { 1, 10, 6, 2, 7, 11, 12, 13, 3, 15 };
            //Print(array);
            Sort(array);
            //Print(array);

            File.WriteAllText("output.txt", string.Join(" ", array));
        }

        private static void Print(double[] array)
        {
            int treeHeight = (int)Math.Ceiling(Math.Log2(array.Length));
            for (int i = 0; i < treeHeight; i++)
            {
                int elementsCountForCurrentLevel = (int)Math.Pow(2, i);
                Console.Write(string.Concat(Enumerable.Repeat("    ", (int)Math.Pow(2, treeHeight - i - 1) - 1)));

                for (int j = 0; j < elementsCountForCurrentLevel; j++)
                {
                    int nextLementIndex = (int)Math.Pow(2, i) - 1 + j;
                    if (nextLementIndex < array.Length)
                    {
                        Console.Write(array[nextLementIndex]);
                    }
                    Console.Write(string.Concat(Enumerable.Repeat("  ", (int)Math.Pow(2, treeHeight - i + 1))));
                }
                Console.WriteLine();
            }
            Console.WriteLine(string.Join(" ", array.Select(x => x.ToString())));
        }

        public static void Sort(double[] array)
        {
            Heapify(array);
            int heapSize = array.Length;
            for (int i = 0; i < array.Length; i++)
            {
                double max = array[0];
                array[0] = array[heapSize - 1];
                array[heapSize - 1] = max;
                heapSize--;
                if (heapSize > 1)
                {
                    SeepDown(array, 0, heapSize);
                }
            }
        }

        private static void Heapify(double[] array)
        {
            int nonLeafNodeCount = array.Length / 2;

            for (int parentIndex = nonLeafNodeCount - 1; parentIndex >= 0; parentIndex--)
            {
                SeepDown(array, parentIndex, array.Length);
                //Print(array);
            }
        }

        private static void SeepDown(double[] array, int parentIndex, int heapSize)
        {
            int nonLeafNodeCount = heapSize / 2;
            int leftChildIndex = parentIndex * 2 + 1;
            int rightChildIndex = parentIndex * 2 + 2;
            int largestChildIndex = rightChildIndex >= heapSize
                                    ? leftChildIndex
                                    : array[leftChildIndex] > array[rightChildIndex]
                                        ? leftChildIndex
                                        : rightChildIndex;
            if (array[parentIndex] < array[largestChildIndex])
            {
                (array[parentIndex], array[largestChildIndex]) = (array[largestChildIndex], array[parentIndex]);
                if (largestChildIndex <= nonLeafNodeCount - 1)
                {
                    SeepDown(array, largestChildIndex, heapSize);
                }
            }
        }
    }
}