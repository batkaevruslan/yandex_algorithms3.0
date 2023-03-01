using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Test
{
    internal class Tests
    {
        [TestCase(25, 2, 1, 2, "2 2")]
        [TestCase(25, 13, 7, 1, "-1")]
        [TestCase(2, 2, 1, 1, "-1")]
        [TestCase(2, 2, 1, 2, "-1")]
        [TestCase(5, 4, 2, 1, "-1")]
        [TestCase(5, 4, 3, 1, "1 1")]
        [TestCase(15, 5, 3, 1, "5 2")]
        [TestCase(15, 5, 5, 2, "3 1")]
        [TestCase(15, 5, 5, 1, "7 2")]
        [TestCase(15, 5, 8, 1, "5 2")]
        [TestCase(15, 15, 8, 1, "-1")]
        public void FindsVasyaPlace(double pupilCount, double testCount, double petyaRow, int petyaColumn, string expectedResult)
        {
            string result = Program.ProblemSolver.FindPlaceForVasya(pupilCount, testCount, petyaRow, petyaColumn);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
