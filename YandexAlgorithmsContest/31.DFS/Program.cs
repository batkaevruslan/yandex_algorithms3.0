using System;
using System.Collections.Generic;
using System.Linq;

namespace _31.DFS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            int vertexCount = input[0];
            int edgeCount = input[1];
            List<int>[] graph = new List<int>[vertexCount + 1];
            for(int v  = 1; v <= vertexCount; v++)
            {
                graph[v] = new List<int>();
            }
            for(int i=0; i < edgeCount; i++)
            {
                int[] vertexes = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
                int v1 = vertexes[0];
                int v2 = vertexes[1];
                graph[v1].Add(v2);
                graph[v2].Add(v1);
            }

            int[] result = GetVertexesInConnectedComponent(graph, 1);

            Console.WriteLine(result.Length);
            Console.WriteLine(string.Join(" ", result));
        }

        private static int[] GetVertexesInConnectedComponent(List<int>[] graph, int vertexNumber)
        {
            int[] connectedComponent = new int[graph.Length];
            DepthFirstSearch(graph, connectedComponent, vertexNumber);
            return connectedComponent.Select((connectionBit, vertex) => (connectionBit, vertex))
                                     .Where(c => c.connectionBit == 1)
                                     .Select(c => c.vertex)
                                     .OrderBy(i => i)
                                     .ToArray();
        }

        private static void DepthFirstSearch(List<int>[] graph, int[] connectedComponent, int vertexNumber)
        {
            connectedComponent[vertexNumber] = 1;
            foreach(int neighbourVertex in graph[vertexNumber])
            {
                if(connectedComponent[neighbourVertex] == 0)
                {
                    DepthFirstSearch(graph, connectedComponent, neighbourVertex);
                }
            }
        }
    }
}