using System.Collections.Generic;
using System.Linq;
using System;

namespace _32.ConnectedComponents
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

            int[] connectedComponentNumberPerVertex = GetVertexesInConnectedComponent(graph);

            IGrouping<int, int>[] components = connectedComponentNumberPerVertex
                                                         .Select((componentNumber, vertexNumber) => (componentNumber, vertexNumber))
                                                         .GroupBy(x => x.componentNumber, x => x.vertexNumber)
                                                         .Where(g => g.Key != 0)
                                                         .ToArray();
            Console.WriteLine(components.Length);
            foreach (var component in components)
            {
                Console.WriteLine(component.Count());
                Console.WriteLine(string.Join(" ", component));
            }
        }

        private static int[] GetVertexesInConnectedComponent(List<int>[] graph)
        {
            int connectedComponentNumber = 0;
            int[] connectedComponentNumberPerVertex = new int[graph.Length];
            for (int vertexNumber = 1; vertexNumber < graph.Length; vertexNumber++)
            {
                if (connectedComponentNumberPerVertex[vertexNumber] == 0)
                {
                    connectedComponentNumber++;
                    DepthFirstSearch(graph, connectedComponentNumberPerVertex, vertexNumber, connectedComponentNumber);
                }
            }
            return connectedComponentNumberPerVertex;
        }

        private static void DepthFirstSearch(List<int>[] graph, int[] connectedComponent, int vertexNumber, int connectedComponentNumber)
        {
            connectedComponent[vertexNumber] = connectedComponentNumber;
            foreach (int neighbourVertex in graph[vertexNumber])
            {
                if (connectedComponent[neighbourVertex] == 0)
                {
                    DepthFirstSearch(graph, connectedComponent, neighbourVertex, connectedComponentNumber);
                }
            }
        }
    }
}