using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19.Heap
{
    internal class Tests
    {
        [Test]
        public void ToDo()
        {
            Program.MyHeap heap = new();
            heap.Insert(100);
            heap.Extract().Should().Be(100);


            heap.Insert(100);
            heap.Insert(50);
            heap.Extract().Should().Be(100);

            heap.Insert(100);
            heap.Extract().Should().Be(100);

            heap.Insert(40);
            heap.Insert(30);
            heap.Insert(20);
            heap.Insert(10);
            heap.Insert(1);
            heap.Extract().Should().Be(50);
        }

        [Test]
        public void Test2()
        {
            Program.MyHeap heap = new();
            heap.Insert(1);
            heap.Insert(345);
            heap.Extract().Should().Be(345);
            heap.Insert(4346);
            heap.Extract().Should().Be(4346);
            heap.Insert(2435);
            heap.Extract().Should().Be(2435);
            heap.Insert(235);
            heap.Insert(5);
            heap.Insert(1);
            heap.Extract().Should().Be(235);
            heap.Extract().Should().Be(5);
            heap.Extract().Should().Be(1);
        }

        [Test]
        public void Test3()
        {
            Program.MyHeap heap = new();
            heap.Insert(-1);
            heap.Insert(-2);
            heap.Insert(0);
            heap.Extract().Should().Be(0);
        }

        [Test]
        public void Test4()
        {
            Program.MyHeap heap = new();
            heap.Insert(10);
            heap.Insert(10);
            heap.Insert(5);
            heap.Extract().Should().Be(10);
            heap.Extract().Should().Be(10);
            heap.Extract().Should().Be(5);
        }

        [Test]
        public void Test5()
        {
            Program.MyHeap heap = new();
            heap.Insert(10);
            heap.Insert(5);
            heap.Extract().Should().Be(10);
            heap.Extract().Should().Be(5);
        }

        [Test]
        public void Test6()
        {
            Program.MyHeap heap = new();
            heap.Insert(1);
            heap.Insert(10);
            heap.Insert(2);
            heap.Insert(3);
            heap.Insert(4);
            heap.Insert(20);
            heap.Insert(5);
            heap.Insert(6);
            heap.Insert(15);
            heap.Insert(7);
            heap.Insert(8);
            heap.Insert(9);
            heap.Extract().Should().Be(20);
            heap.Extract().Should().Be(15);
            heap.Extract().Should().Be(10);
            heap.Extract().Should().Be(9);
            heap.Extract().Should().Be(8);
            heap.Extract().Should().Be(7);
            heap.Extract().Should().Be(6);
            heap.Extract().Should().Be(5);
            heap.Extract().Should().Be(4);
            heap.Extract().Should().Be(3);
            heap.Extract().Should().Be(2);
            heap.Extract().Should().Be(1);
        }
    }
}
