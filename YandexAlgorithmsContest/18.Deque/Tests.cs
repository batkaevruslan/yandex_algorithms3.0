using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18.Deque
{
    internal class Tests
    {
        [Test]
        public void PushesToFront()
        {
            var deque = new Program.MyDeque();

            deque.PushFront(1);
            deque.Front().Should().Be(1);

            deque.PushFront(2);
            deque.Front().Should().Be(2);

            deque.PushFront(3);
            deque.Front().Should().Be(3);

            deque.PushFront(4);
            deque.Front().Should().Be(4);

            deque.PushFront(5);
            deque.Front().Should().Be(5);

            deque.PushFront(6);
            deque.Front().Should().Be(6);

            deque.PushFront(7);
            deque.Front().Should().Be(7);
        }

        [Test]
        public void ReturnsSize()
        {
            var deque = new Program.MyDeque();

            deque.Size().Should().Be(0);

            deque.PushFront(1);
            deque.Size().Should().Be(1);

            deque.PushFront(2);
            deque.Size().Should().Be(2);

            deque.PushFront(3);
            deque.Size().Should().Be(3);

            deque.PushFront(4);
            deque.Size().Should().Be(4);

            deque.PushFront(5);
            deque.Size().Should().Be(5);

            deque.PushFront(6);
            deque.Size().Should().Be(6);

            deque.PushFront(7);
            deque.Size().Should().Be(7);
        }

        [Test]
        public void PopsFront()
        {
            var deque = new Program.MyDeque();

            deque.PushFront(1);
            deque.PopFront();
            deque.Size().Should().Be(0);

            deque.PushFront(1);
            deque.PopFront();
            deque.Size().Should().Be(0);

            deque.PushFront(1);
            deque.PushFront(2);
            deque.PopFront();
            deque.Size().Should().Be(1);
            deque.Front().Should().Be(1);

            deque.PushFront(2);
            deque.PushFront(3);
            deque.PopFront();
            deque.Size().Should().Be(2);
            deque.Front().Should().Be(2);

            deque.PushFront(3);
            deque.PushFront(4);
            deque.PopFront();
            deque.Size().Should().Be(3);
            deque.Front().Should().Be(3);
        }

        [Test]
        public void PushesToBack()
        {
            var deque = new Program.MyDeque();

            deque.PushBack(1);
            deque.Back().Should().Be(1);

            deque.PushBack(2);
            deque.Back().Should().Be(2);

            deque.PushBack(3);
            deque.Back().Should().Be(3);

            deque.PushBack(4);
            deque.Back().Should().Be(4);

            deque.PushBack(5);
            deque.Back().Should().Be(5);

            deque.PushBack(6);
            deque.Back().Should().Be(6);

            deque.PushBack(7);
            deque.Back().Should().Be(7);
        }

        [Test]
        public void PopsBack()
        {
            var deque = new Program.MyDeque();

            deque.PushBack(1);
            var back = deque.PopBack();
            back.Should().Be(1);
            deque.Size().Should().Be(0);

            deque.PushBack(1);
            back = deque.PopBack();
            back.Should().Be(1);
            deque.Size().Should().Be(0);

            deque.PushBack(1);
            deque.PushBack(2);
            back = deque.PopBack();
            back.Should().Be(2);
            deque.Size().Should().Be(1);
            deque.Back().Should().Be(1);

            deque.PushBack(2);
            deque.PushBack(3);
            back = deque.PopBack();
            back.Should().Be(3);
            deque.Size().Should().Be(2);
            deque.Back().Should().Be(2);

            deque.PushBack(3);
            deque.PushBack(4);
            back = deque.PopBack();
            back.Should().Be(4);
            deque.Size().Should().Be(3);
            deque.Back().Should().Be(3);
        }
    }
}
