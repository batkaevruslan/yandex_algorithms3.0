using System.Collections.Generic;
using System;
using System.Linq;

namespace _37.ShortestPathVertexes
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

            int[] input = Console.ReadLine().Trim().Split(" ").Select(int.Parse).ToArray();
            int startVertex = input[0];
            int endVertex = input[1];

            int[] path = FindShortestPath(graph, vertexCount, startVertex, endVertex);

            if (path.Length == 0)
            {
                Console.WriteLine(-1);
            }
            else if (path.Length == 1)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(path.Length - 1);
                Console.WriteLine(string.Join(" ", path));
            }
        }

        private static int[] FindShortestPath(int[][] graph, int vertexCount, int startVertex, int endVertex)
        {
            int[] pathLengthToStartVertex = new int[vertexCount];
            int[] previousVertex = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                pathLengthToStartVertex[i] = -1;
                previousVertex[i] = -1;
            }
            Queue<int> vertexToVisit = new Queue<int>();
            vertexToVisit.Enqueue(startVertex - 1);
            pathLengthToStartVertex[startVertex - 1] = 0;
            BreadthFirstSearch(graph, pathLengthToStartVertex, vertexToVisit, previousVertex);
            List<int> vertexesOnShortestPath = new List<int>();
            int endOfpathIndex = endVertex - 1;
            while (endOfpathIndex != - 1 && pathLengthToStartVertex[endOfpathIndex] >= 0)
            {
                vertexesOnShortestPath.Add(endOfpathIndex + 1);
                endOfpathIndex = previousVertex[endOfpathIndex];
            }
            vertexesOnShortestPath.Reverse();
            return vertexesOnShortestPath.ToArray();
        }

        private static void BreadthFirstSearch(int[][] graph, int[] pathLengthToStartVertex, Queue<int> vertexToVisit, int[] previousVertex)
        {
            int vertexIndex = vertexToVisit.Dequeue();
            for (int neighbour = 0; neighbour < graph[vertexIndex].Length; neighbour++)
            {
                if (graph[vertexIndex][neighbour] == 1)
                {
                    if (pathLengthToStartVertex[neighbour] == -1)
                    {
                        pathLengthToStartVertex[neighbour] = pathLengthToStartVertex[vertexIndex] + 1;
                        previousVertex[neighbour] = vertexIndex;
                        vertexToVisit.Enqueue(neighbour);
                    }
                }
            }
            if (vertexToVisit.Any())
            {
                BreadthFirstSearch(graph, pathLengthToStartVertex, vertexToVisit, previousVertex);
            }
        }
    }
}