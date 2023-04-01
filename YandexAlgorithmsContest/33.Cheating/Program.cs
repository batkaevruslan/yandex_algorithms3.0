using System;
using System.Collections.Generic;
using System.Linq;

namespace _33.Cheating
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
                graph[v2].Add(v1);
            }

            bool result = IsBipartiteGraph(graph);

            Console.WriteLine(result ? "YES" : "NO");
        }

        private static bool IsBipartiteGraph(List<int>[] graph)
        {
            Color[] visitedVertexes = new Color[graph.Length];
            bool hasNeigbourOfTheSameColor = false;
            for (int vertext = 1; vertext < graph.Length; vertext++)
            {
                if (visitedVertexes[vertext] == Color.None)
                {
                    hasNeigbourOfTheSameColor |= HasNeigbourOfTheSameColor(graph, visitedVertexes, vertext, Color.Red);
                }
            }

            return !hasNeigbourOfTheSameColor;
        }

        private static bool HasNeigbourOfTheSameColor(List<int>[] graph, Color[] visitedVertexes, int vertext, Color color)
        {
            visitedVertexes[vertext] = color;
            bool hasNeigbourOfTheSameColor = false;
            foreach (int neighbour in graph[vertext])
            {
                if (visitedVertexes[neighbour] == color)
                {
                    return true;
                }
                else if (visitedVertexes[neighbour] == Color.None)
                {
                    Color newColor = color == Color.Red ? Color.Black : Color.Red;
                    hasNeigbourOfTheSameColor |= HasNeigbourOfTheSameColor(graph, visitedVertexes, neighbour, newColor);
                }
            }
            return hasNeigbourOfTheSameColor;
        }

        enum Color
        {
            None = 0,
            Red,
            Black
        }
    }
}