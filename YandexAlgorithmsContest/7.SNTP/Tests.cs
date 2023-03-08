using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _7.SNTP.Program;

namespace _7.SNTP
{
    internal class Tests
    {
        private static TestCaseData[] _cases = new TestCaseData[] {
            new (new Time(15, 1, 00), new Time(18, 09, 45), new Time(15, 1, 40), new Time(18, 10, 05)),
            new (new Time(23, 59, 50), new Time(12, 00, 00), new Time(0, 0, 10), new Time(12, 00, 10)),
            new (new Time(23, 59, 50), new Time(23, 59, 59), new Time(0, 0, 10), new Time(00, 00, 09)),
            new (new Time(23, 59, 59), new Time(23, 59, 59), new Time(23, 59, 59), new Time(23, 59, 59)),
            new (new Time(0, 0, 0), new Time(0, 0, 10), new Time(0, 0, 1), new Time(00, 00, 11)),
        };

        [TestCaseSourceAttribute(nameof(_cases))]
        public void AdjustsTime(Time sendTime, Time serverTime, Time receiveTime, Time expectedAdjustedTime)
        {
            GetAdjustedTime(sendTime, serverTime, receiveTime).Should().BeEquivalentTo(expectedAdjustedTime);
        }
    }
}
