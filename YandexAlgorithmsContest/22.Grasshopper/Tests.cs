using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22.Grasshopper
{
    internal class Tests
    {
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(8, 2, 21)]
        [TestCase(6, 3, 13)]
        [TestCase(6, 4, 15)]
        [TestCase(1, 10, 1)]
        public void ReturnsPossibleHopCount(int nuberOfCells, int longestHop, int expectedHopCount)
        {
            Program.GetPossibleHopCount(nuberOfCells, longestHop).Should().Be(expectedHopCount);
        }
    }
}
