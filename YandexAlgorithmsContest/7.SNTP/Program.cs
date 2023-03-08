using System;
using System.IO;

namespace _7.SNTP
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            using FileStream fs = File.OpenRead("input.txt");
            using StreamReader sr = new StreamReader(fs);

            Time sendTime = Time.Parse(sr.ReadLine());
            Time serverTime = Time.Parse(sr.ReadLine());
            Time receiveTime = Time.Parse(sr.ReadLine());

            File.WriteAllText("output.txt", GetAdjustedTime(sendTime, serverTime, receiveTime).ToString());
        }

        public static Time GetAdjustedTime(Time sendTime, Time serverTime, Time receiveTime)
        {
            int requestLatency = receiveTime.GetTotalSeconds() - sendTime.GetTotalSeconds();
            if (requestLatency < 0)
            {
                requestLatency = requestLatency + new Time(24, 0, 0).GetTotalSeconds();
            }
            int adjustedTimeTotalSeconds = serverTime.GetTotalSeconds() + (int)Math.Round(requestLatency / 2.0, MidpointRounding.ToPositiveInfinity);
            int seconds = adjustedTimeTotalSeconds % 60;
            int minutes = ((adjustedTimeTotalSeconds - seconds) / 60) % 60;
            int hours = (adjustedTimeTotalSeconds / 60 / 60) % 24;
            return new Time(hours, minutes, seconds);
        }

        public class Time
        {
            public Time(int h, int m, int s)
            {
                Hour = h;
                Minute = m;
                Second = s;
            }

            public int Hour { get; }
            public int Minute { get; }
            public int Second { get; }

            public static Time Parse(string input)
            {
                var stringParts = input.Split(":");
                int h = int.Parse(stringParts[0]);
                int m = int.Parse(stringParts[1]);
                int s = int.Parse(stringParts[2]);
                return new Time(h, m, s);
            }

            public override string ToString()
            {
                return $"{Hour:d2}:{Minute:d2}:{Second:d2}";
            }

            public int GetTotalSeconds()
            {
                return Hour * 60 * 60 + Minute * 60 + Second;
            }
        }
    }
}