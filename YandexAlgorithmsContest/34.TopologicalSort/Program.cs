using System.Collections.Generic;
using System;
using System.Linq;

namespace _34.TopologicalSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            int vertexCount = input[0];
            int edgeCount = input[1];
            List<int>[] graph = new List<int>[vertexCount + 1];
            for (int v = 1; v <= vertexCount; v++)
            {
                graph[v] = new List<int>();
            }
            for (int i = 0; i < edgeCount; i++)
            {
                int[] vertexes = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
                int v1 = vertexes[0];
                int v2 = vertexes[1];
                graph[v1].Add(v2);
            }

            int[] sortedVertexes = Sort(graph);

            Console.WriteLine(sortedVertexes.Length == vertexCount ? string.Join(" ", sortedVertexes) : "-1");
        }

        private static int[] Sort(List<int>[] graph)
        {
            Color[] coloredVertexes = new Color[graph.Length];
            List<int> sortedVertexes = new();
            for(int vertex = 1; vertex <  graph.Length; vertex++)
            {
                if (coloredVertexes[vertex] == Color.None)
                {
                    ColorVertex(graph, coloredVertexes, vertex, sortedVertexes);
                }
            }
            sortedVertexes.Reverse();
            return sortedVertexes.ToArray();
        }

        private static void ColorVertex(List<int>[] graph, Color[] coloredVertexes, int vertex, List<int> sortedVertexes)
        {
            coloredVertexes[vertex] = Color.Gray;
            foreach(int neighbour in graph[vertex])
            {
                if (coloredVertexes[neighbour] == Color.Gray)
                {
                    return;
                } else if (coloredVertexes[neighbour] == Color.None)
                {
                    ColorVertex(graph, coloredVertexes, neighbour, sortedVertexes);
                }
            }
            coloredVertexes[vertex] = Color.Black;
            sortedVertexes.Add(vertex);
        }

        enum Color
        {
            None = 0,
            Gray = 1,
            Black = 2
        }
    }
}