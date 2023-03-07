using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _6.OperatingSystem.Program;

namespace _6.OperatingSystem
{
    internal class Tests
    {
        private static TestCaseData[] _cases = new TestCaseData[]
        {
            new TestCaseData(
                new OS[]{new (1, 1)},
                new OS[]{new OS(1,1)}),
            new TestCaseData(
                new OS[]{new (0, 0), new (1, 1)},
                new OS[]{new OS(0,0), new (1, 1)}),
            new TestCaseData(
                new OS[]{new (0, 0), new (0, 0)},
                new OS[]{new OS(0,0)}),
            new TestCaseData(
                new OS[]{new (0, 2), new (1, 3)},
                new OS[]{new OS(1, 3)}),
            new TestCaseData(
                new OS[]{new (1, 1), new (0, 2)},
                new OS[]{new OS(0, 2)}),
            new TestCaseData(
                new OS[]{new (0, 2), new (1, 1)},
                new OS[]{new OS(1, 1)}),
            new TestCaseData(
                new OS[]{new (10, 5), new (6, 7)},
                new OS[]{new (10, 5), new (6, 7)}),
            new TestCaseData(
                new OS[]{new (6, 7), new (10, 5)},
                new OS[]{new (6, 7), new (10, 5)}),
            new TestCaseData(
                new OS[]{new (10, 5), new (9, 6)},
                new OS[]{new (9, 9)}),
            new TestCaseData(
                new OS[]{new OS(1, 3), new (4, 7), new (3, 4) },
                new OS[]{new (3, 4)})
        };

        [TestCaseSource(nameof(_cases))]
        public void ReturnsWorkingOS(OS[] installedOS, OS[] expectedWorkingOS)
        {
            List<OS> result = Program.GetWorkingOS(installedOS.ToList(), 20);

            result.Should().BeEquivalentTo(expectedWorkingOS);
        }
    }
}
