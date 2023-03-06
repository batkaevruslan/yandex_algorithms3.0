using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.GoodString
{
    internal class Tests
    {
        private static TestCaseData[] _cases = new TestCaseData[]
        {
            new TestCaseData(new double[]{1}, 0),
            new TestCaseData(new double[]{1, 1}, 1),
            new TestCaseData(new double[]{1, 1, 1}, 2),
            new TestCaseData(new double[]{3, 4}, 3),
            new TestCaseData(new double[]{1, 0, 1}, 0),
            new TestCaseData(new double[]{2, 0, 2}, 0),
            new TestCaseData(new double[]{1, 1, 0, 1, 1}, 2),
            new TestCaseData(new double[]{1, 1, 0, 0, 1, 1}, 2),
            new TestCaseData(new double[]{0, 1, 1, 0}, 1),
            new TestCaseData(new double[]{2, 3, 1, 2, 3, 1, 2, 3}, 10),
        };

        [TestCaseSource(nameof(_cases))]
        public void ReturnsGoodness(double[] countPerLetter, double expectedGoodness)
        {
            var result = Program.ProblemSolver.GetGoodness(0, countPerLetter);

            Assert.That(result, Is.EqualTo(expectedGoodness));
        }
    }
}
