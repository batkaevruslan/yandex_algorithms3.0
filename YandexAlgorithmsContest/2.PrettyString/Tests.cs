using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.PrettyString
{
    internal class Tests
    {
        [TestCase(0, "a", 1)]
        [TestCase(0, "ab", 1)]
        [TestCase(1, "ab", 2)]
        [TestCase(1, "aab", 3)]
        [TestCase(1, "aaa", 3)]
        [TestCase(1, "bba", 3)]
        [TestCase(0, "aaa", 3)]
        [TestCase(2, "cababa", 5)]
        [TestCase(2, "abcaz", 4)]
        [TestCase(2, "helto", 3)]
        public void CalculateMaxPrettieness(double replacementCount, string word, int expectedMaxPrettieness)
        {
            Program.GetMaxPrettiness(replacementCount, word).Should().Be(expectedMaxPrettieness);
        }
    }
}
