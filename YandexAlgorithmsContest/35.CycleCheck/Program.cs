using System;
using System.Collections.Generic;
using System.Linq;

namespace _35.CycleCheck
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int vertexCount = int.Parse(Console.ReadLine());
            int[][] graph = new int[vertexCount][];
            for (int i = 0; vertexCount > i; i++)
            {
                graph[i] = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            }

            FindCycle(graph, vertexCount);

            Console.WriteLine(_cycleVertexes.Count > 1 ? "YES" : "NO");
            if(_cycleVertexes.Count > 1)
            {
                Console.WriteLine(_cycleVertexes.Count);
                Console.WriteLine(string.Join(" ", _cycleVertexes.Select(v => v + 1)));
            }
        }

        private static void FindCycle(int[][] graph, int vertexCount)
        {
            Color[] coloredVertexes = new Color[vertexCount];
            for (int vertex = 0; vertex < vertexCount; vertex++)
            {
                if (_cycleFound && _cycleRembembered)
                {
                    return;
                }
                if (coloredVertexes[vertex] == Color.None)
                {
                    Dfs(graph, vertex, previousVertex: null, coloredVertexes);
                }
            }
        }
        private static readonly List<int> _cycleVertexes = new List<int>();
        private static int _cycleStartVertex = -1;
        private static bool _cycleFound = false;
        private static bool _cycleRembembered = false;
        private static void Dfs(int[][] graph, int vertex, int? previousVertex, Color[] coloredVertexes)
        {
            coloredVertexes[vertex] = Color.Gray;
            for (int possibleNeigbour = 0; possibleNeigbour < graph[vertex].Length; possibleNeigbour++)
            {
                if (_cycleFound && _cycleRembembered)
                {
                    return;
                }
                if (graph[vertex][possibleNeigbour] == 1 && possibleNeigbour != previousVertex)
                {
                    if (coloredVertexes[possibleNeigbour] == Color.Gray) {
                        _cycleVertexes.Add(vertex);
                        _cycleStartVertex = possibleNeigbour;
                        _cycleFound = true;
                        break;
                    } else if (coloredVertexes[possibleNeigbour] == Color.None)
                    {
                        Dfs(graph, possibleNeigbour, vertex, coloredVertexes);
                        if(_cycleFound && !_cycleRembembered)
                        {
                            _cycleVertexes.Add(vertex);
                            _cycleRembembered = _cycleStartVertex == vertex;
                            return;
                        }
                    }
                }
            }
            coloredVertexes[vertex] = Color.Black;
        }

        enum Color
        {
            None = 0,
            Gray,
            Black
        }
    }
}