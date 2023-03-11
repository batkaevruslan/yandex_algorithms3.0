using System;
using System.IO;
using System.Text;

namespace _16.Queue
{
    internal class Program
    {

        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            string line;
            StringBuilder sb = new();
            MyQueue stack = new();
            while ((line = sr.ReadLine()) != "exit")
            {
                try
                {
                    if (line.StartsWith("push"))
                    {
                        int n = int.Parse(line.Substring("push".Length));
                        stack.Push(n);
                        sb.AppendLine("ok");
                    }
                    else if (line == "pop")
                    {
                        sb.AppendLine(stack.Pop().ToString());
                    }
                    else if (line == "front")
                    {
                        sb.AppendLine(stack.Front().ToString());
                    }
                    else if (line == "size")
                    {
                        sb.AppendLine(stack.Size().ToString());
                    }
                    else if (line == "clear")
                    {
                        stack.Clear();
                        sb.AppendLine("ok");
                    }
                }
                catch
                {
                    sb.AppendLine("error");
                }
            }
            sb.AppendLine("bye");
            File.WriteAllText("output.txt", sb.ToString());
        }

        public class MyQueue
        {
            private int[] _data;
            private int _tail;
            private int _head;
            public MyQueue()
            {
                _data = new int[1];
                _tail = -1;
                _head = -1;
            }

            public void Push(int n)
            {
                if (_tail == _data.Length - 1)
                {
                    Resize();
                }
                _tail++;
                _data[_tail] = n;
                if(_head == -1)
                {
                    _head = _tail;
                }
            }

            public int Pop()
            {
                if (_head == -1)
                {
                    throw new Exception("Queue is empty");
                }
                int result = _data[_head];
                _head++;
                if(_head > _tail)
                {
                    _tail = -1;
                    _head = -1;
                }
                return result;
            }

            public int Front()
            {
                if (_head == -1)
                {
                    throw new Exception("Queue is empty");
                }
                return _data[_head];
            }

            public void Clear()
            {
                _head = -1;
                _tail = -1;
            }

            private void Resize()
            {
                var newData = new int[2 * _data.Length];
                for (int i = _head; i <= _tail; i++)
                {
                    newData[i - _head] = _data[i];
                }
                _data = newData;
                _tail = _tail - _head;
                _head = 0;
            }

            internal int Size()
            {
                return _head == -1 ? 0 : _tail - _head + 1;
            }
        }
    }
}