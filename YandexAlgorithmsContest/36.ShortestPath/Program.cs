using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _36.ShortestPath
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

            int pathLength = FindShortestPath(graph, vertexCount, startVertex, endVertex);

            Console.WriteLine(pathLength);
        }

        private static int FindShortestPath(int[][] graph, int vertexCount, int startVertex, int endVertex)
        {
            int[] pathLengthToStartVertex = new int[vertexCount];
            for(int i = 0; i < vertexCount; i++)
            {
                pathLengthToStartVertex[i] = -1;
            }
            Queue<int> vertexToVisit = new Queue<int>();
            vertexToVisit.Enqueue(startVertex - 1);
            pathLengthToStartVertex[startVertex - 1] = 0;
            BreadthFirstSearch(graph, pathLengthToStartVertex, vertexToVisit);
            return pathLengthToStartVertex[endVertex - 1];
        }

        private static void BreadthFirstSearch(int[][] graph, int[] pathLengthToStartVertex, Queue<int> vertexToVisit)
        {
            int vertexIndex = vertexToVisit.Dequeue();
            for (int neighbour = 0; neighbour < graph[vertexIndex].Length; neighbour++)
            {
                if (graph[vertexIndex][neighbour] == 1)
                {
                    if (pathLengthToStartVertex[neighbour] == -1)
                    {
                        pathLengthToStartVertex[neighbour] = pathLengthToStartVertex[vertexIndex] + 1;
                        vertexToVisit.Enqueue(neighbour);
                    }
                }
            }
            if (vertexToVisit.Any())
            {
                BreadthFirstSearch(graph, pathLengthToStartVertex, vertexToVisit);
            }
        }
    }
}