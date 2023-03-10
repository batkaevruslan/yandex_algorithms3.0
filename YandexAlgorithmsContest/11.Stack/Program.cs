using System;
using System.IO;
using System.Text;

namespace _11.Stack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            string line;
            StringBuilder sb = new();
            MyStack stack = new();
            while(( line = sr.ReadLine())!= "exit")
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
                    else if (line == "back")
                    {
                        sb.AppendLine(stack.Back().ToString());
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

        public class MyStack
        {
            private int[] _data;
            private int _currentIndex;
            public MyStack()
            {
                _data = new int[1];
                _currentIndex = -1;
            }

            public void Push(int n)
            {
                if(_currentIndex == _data.Length - 1) {
                    Resize();
                }
                _currentIndex++;
                _data[_currentIndex] = n;
            }

            public int Pop()
            {
                if(_currentIndex == -1)
                {
                    throw new Exception("Stack is empty");
                }
                int result = _data[_currentIndex];
                _currentIndex--;
                return result;
            }

            public int Back()
            {
                if (_currentIndex == -1)
                {
                    throw new Exception("Stack is empty");
                }
                return _data[_currentIndex];
            }

            public void Clear()
            {
                _currentIndex = -1;
            }

            private void Resize()
            {
                var newData = new int[2 * _data.Length];
                for(int i = 0; i < _data.Length; i++)
                {
                    newData[i] = _data[i];
                }
                _data = newData;
            }

            internal int Size()
            {
                return _currentIndex + 1;
            }
        }
    }
}