using System;
using System.Collections.Generic;
using System.Linq;

namespace _40.Subway
{
    internal class Program
    {
        private static int _stationCount = 0;
        static void Main(string[] args)
        {
            _stationCount = int.Parse(Console.ReadLine());
            int lineCount = int.Parse(Console.ReadLine());
            Dictionary<NeigbourStation, List<Transition>> subway = new();
            for (int line = 1; line <= lineCount; line++)
            {
                int[] row = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
                int lineStations = row[0];
                for (int ls = 1; ls <= lineStations - 1; ls++)
                {
                    NeigbourStation firstStation = new NeigbourStation(row[ls], line);
                    NeigbourStation secondStation = new NeigbourStation(row[ls + 1], line);
                    if (!subway.ContainsKey(firstStation))
                    {
                        subway[firstStation] = new List<Transition>();
                    }
                    if (!subway.ContainsKey(secondStation))
                    {
                        subway[secondStation] = new List<Transition>();
                    }
                    subway[firstStation].Add(new Transition(secondStation, 0));
                    subway[secondStation].Add(new Transition(firstStation, 0));
                    foreach(NeigbourStation station in subway.Keys.Where(s => s.StationNumber == firstStation.StationNumber && s.LineNumber != firstStation.LineNumber))
                    {
                        subway[station].Add(new Transition(firstStation, 1));
                        subway[firstStation].Add(new Transition(station, 1));
                    }
                    foreach (NeigbourStation station in subway.Keys.Where(s => s.StationNumber == secondStation.StationNumber && s.LineNumber != secondStation.LineNumber))
                    {
                        subway[station].Add(new Transition(secondStation, 1));
                        subway[secondStation].Add(new Transition(station, 1));
                    }
                }
            }
            int[] path = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            int startStation = path[0];
            int endStation = path[1];

            int result = GetChangeStationsOnTheWay(subway, startStation, endStation);

            Console.WriteLine(result);
        }

        record struct NeigbourStation(int StationNumber, int LineNumber);
        record struct Transition(NeigbourStation targetStation, int cost);

        private static int GetChangeStationsOnTheWay(Dictionary<NeigbourStation, List<Transition>> subway, int startStation, int endStation)
        {
            Dictionary<NeigbourStation, int> pathLengthFromStartStation = new ();
            PriorityQueue<NeigbourStation, int> stationsToVisit = new PriorityQueue<NeigbourStation, int>();
            foreach (var station in subway.Keys.Where(k => k.StationNumber == startStation))
            {
                stationsToVisit.Enqueue(station, 0);
            }
            VisitNextStation(subway, pathLengthFromStartStation, stationsToVisit);

            int[] possibleSolutions = pathLengthFromStartStation.Where(pair => pair.Key.StationNumber == endStation).Select(pair => pair.Value).ToArray();
            return possibleSolutions.Any() ? possibleSolutions.Min() : -1;
        }

        private static void VisitNextStation(Dictionary<NeigbourStation, List<Transition>> subway,
                                             Dictionary<NeigbourStation, int> pathLengthFromStartStationByStation,
                                             PriorityQueue<NeigbourStation, int> stationsToVisit)
        {
            stationsToVisit.TryDequeue(out NeigbourStation stationToVisit, out int pathLength);
            if (!pathLengthFromStartStationByStation.ContainsKey(stationToVisit))
            {
                pathLengthFromStartStationByStation[stationToVisit] = pathLength;
                foreach (var transition in subway[stationToVisit])
                {
                    if (!pathLengthFromStartStationByStation.ContainsKey(transition.targetStation))
                    {
                        stationsToVisit.Enqueue(transition.targetStation, pathLength + transition.cost);
                    }
                }
            }

            if (stationsToVisit.Count > 0)
            {
                VisitNextStation(subway, pathLengthFromStartStationByStation, stationsToVisit);
            }
        }
    }
}