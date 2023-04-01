using System;
using System.Collections.Generic;
using System.Linq;

namespace Final.B
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            int taskCount = input[0];
            int periodLength = input[1];
            List<(int eventTime, EventType type, int eventNumber)> events = new();
            for (int i = 0; i < taskCount; i++)
            {
                int[] taskData = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
                events.Add(new(taskData[0], EventType.TaskStart, i + 1));
                events.Add(new(taskData[1] + taskData[0], EventType.TaskEnd, i + 1));
            }

            events = events.OrderBy(e => e.eventTime).ThenBy(e => e.type).ToList();
            int maxSimultaneousTasks = 0;
            int currentSimultaneousTasks = 0;
            foreach (var @event in events)
            {
                currentSimultaneousTasks += @event.type == EventType.TaskStart ? 1 : -1;
                maxSimultaneousTasks = Math.Max(maxSimultaneousTasks, currentSimultaneousTasks);
            }

            Console.WriteLine(maxSimultaneousTasks);
            Console.WriteLine(string.Join(" ", events.Where(e => e.type == EventType.TaskStart).Select(e => e.eventNumber).ToArray()));
        }
        private enum EventType
        {
            TaskStart = 2,
            TaskEnd = 1
        }
    }
}