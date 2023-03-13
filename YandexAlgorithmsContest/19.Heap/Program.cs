using System.IO;
using System.Linq;
using System.Text;

namespace _19.Heap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);
            StringBuilder sb = new();
            MyHeap heap = new();
            int n = int.Parse(sr.ReadLine()!);
            for (int i = 0; i < n; i++)
            {
                string command = sr.ReadLine()!.Trim();
                if(command == "1")
                {
                    int value = heap.Extract();
                    sb.AppendLine(value.ToString());
                }
                else
                {
                    int value = command.Split(" ").Select(int.Parse).Last();
                    heap.Insert(value);
                }
            }

            File.WriteAllText("output.txt", sb.ToString());
        }

        public class MyHeap
        {
            private int[] _data = new int[100000];
            private int _lastUsedIndex = -1;
            public int Extract()
            {
                int max = _data[0];
                int parentIndex = 0;
                _data[parentIndex] = _data[_lastUsedIndex];
                int secondChildIndex = parentIndex * 2 + 2;
                int firstChildIndex = parentIndex * 2 + 1;
                while(parentIndex < _lastUsedIndex && firstChildIndex < _lastUsedIndex)
                {
                    if(secondChildIndex < _lastUsedIndex 
                        && _data[secondChildIndex] > _data[firstChildIndex])
                    {
                        firstChildIndex = secondChildIndex;
                    }
                    else if (_data[parentIndex] <= _data[firstChildIndex]) 
                    {
                        (_data[firstChildIndex], _data[parentIndex]) = (_data[parentIndex], _data[firstChildIndex]);
                        parentIndex = firstChildIndex;
                        secondChildIndex = parentIndex * 2 + 2;
                        firstChildIndex = parentIndex * 2 + 1;
                    } else
                    {
                        break;
                    }
                }
                _lastUsedIndex--;
                return max;
            }

            public void Insert(int value)
            {
                _data[++_lastUsedIndex] = value;
                int indexToSwap = _lastUsedIndex;
                int parentIndex = (indexToSwap - 1) / 2;
                while (parentIndex >= 0 && _data[indexToSwap] > _data[parentIndex])
                {
                    (_data[indexToSwap], _data[parentIndex]) = (_data[parentIndex], _data[indexToSwap]);
                    indexToSwap = parentIndex;
                    parentIndex = (indexToSwap - 1) / 2;
                }
            }
        }
    }
}