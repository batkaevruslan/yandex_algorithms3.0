using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace _18.Deque
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            string line;
            StringBuilder sb = new();
            MyDeque deque = new();
            while ((line = sr.ReadLine()) != "exit")
            {
                try
                {
                    if (line.StartsWith("push_front"))
                    {
                        int n = int.Parse(line.Substring("push_front ".Length));
                        deque.PushFront(n);
                        sb.AppendLine("ok");
                    }
                    if (line.StartsWith("push_back"))
                    {
                        int n = int.Parse(line.Substring("push_back ".Length));
                        deque.PushBack(n);
                        sb.AppendLine("ok");
                    }
                    else if (line == "pop_front")
                    {
                        sb.AppendLine(deque.PopFront().ToString());
                    }
                    else if (line == "pop_back")
                    {
                        sb.AppendLine(deque.PopBack().ToString());
                    }
                    else if (line == "front")
                    {
                        sb.AppendLine(deque.Front().ToString());
                    }
                    else if (line == "back")
                    {
                        sb.AppendLine(deque.Back().ToString());
                    }
                    else if (line == "size")
                    {
                        sb.AppendLine(deque.Size().ToString());
                    }
                    else if (line == "clear")
                    {
                        deque.Clear();
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
        public class MyDeque
        {
            private int[] _data;
            private int? _front;
            private int? _back;
            public MyDeque()
            {
                _data = new int[2];
            }
            internal int Back()
            {
                if (Size() == 0)
                {
                    throw new InvalidOperationException("Deque is empty");
                }
                return _data[_back.Value];
            }

            internal void Clear()
            {
                _back = null;
                _front = null;
            }

            internal int Front()
            {
                if (Size() == 0)
                {
                    throw new InvalidOperationException("Deque is empty");
                }
                return _data[_front.Value];
            }

            internal object PopBack()
            {
                int size = Size();
                if (size == 0)
                {
                    throw new InvalidOperationException("Deque is empty");
                }
                int result = _data[_back.Value];
                if (size > 1)
                {
                    _back = GetCircularBufferIndex(_back.Value - 1);
                }
                else
                {
                    Clear();
                }
                return result;
            }

            internal int PopFront()
            {
                int size = Size();
                if (size == 0)
                {
                    throw new InvalidOperationException("Deque is empty");
                }
                int result = _data[_front.Value];
                if (size > 1)
                {
                    _front = GetCircularBufferIndex(_front.Value + 1);
                }
                else
                {
                    Clear();
                }
                return result;
            }

            internal void PushBack(int n)
            {
                if (Size() == _data.Length)
                {
                    Resize();
                }
                _back = GetCircularBufferIndex((_back ?? 0) + 1);
                _data[_back.Value] = n;
                if (!_front.HasValue)
                {
                    _front = _back;
                }
            }

            internal void PushFront(int n)
            {
                if (Size() == _data.Length)
                {
                    Resize();
                }
                _front = GetCircularBufferIndex((_front ?? 0) - 1);
                _data[_front.Value] = n;
                if (!_back.HasValue)
                {
                    _back = _front;
                }
            }

            private void Resize()
            {
                var newData = new int[_data.Length * 2];
                int size = Size();
                for (int i = 0; i < size; i++)
                {
                    newData[i] = _data[GetCircularBufferIndex(_front.Value + i)];
                }
                _data = newData;
                _front = 0;
                _back = size - 1;
            }

            internal int Size()
            {
                return !_front.HasValue ? 0 : (_data.Length + _back.Value - _front.Value) % _data.Length + 1;
            }

            private int GetCircularBufferIndex(int index)
            {
                return (_data.Length + index) % _data.Length;
            }
        }
    }
}