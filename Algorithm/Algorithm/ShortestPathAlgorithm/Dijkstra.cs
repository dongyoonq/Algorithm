using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class Dijkstra
    {
        bool[,] matrixGraph1 = new bool[9, 9]
        {
            { false, true, true, true, false, false, false, false, false },
            { true, false, false, false, true, false, false, false, false  },
            { true, false, false, false, false, true, true, false, false },
            { true, false, false, false, false, false, true, false, false },
            { false, true, false, false, false, false, false, true, true },
            { false, false, true, false, false, false, false, false, false },
            { false, false, true, true, false, false, false, false, false },
            { false, false, false, false, true, false, false, false, false },
            { false, false, false, false, true, false, false, false, false },
        };

        const int INF = 99999;

        int[,] matrixGraph2 = new int[9, 9]
        {
            {  0, 1, 3, 5, INF, INF, INF, INF, INF },
            {  1, 0, INF, INF, 1, INF, INF, INF, INF },
            {  3, INF, 0, INF, INF, 1, 6, INF, INF },
            {  5, INF, INF, 0, INF, INF, 5, INF, INF },
            {  INF, 1, INF, INF, 0, INF, INF, 1, 1 },
            {  INF, INF, 1, INF, INF, 0, INF, INF, INF },
            {  INF, INF, 6, 5, INF, INF, 0, INF, INF },
            {  INF, INF, INF, INF, 1, INF, INF, 0, INF },
            {  INF, INF, INF, INF, 1, INF, INF, INF, 0 },
        };

        public void Start()
        {
            int[] distance;
            int[] path;

            ShortestPath(matrixGraph2, 0, out distance, out path);

            Console.WriteLine("Vertx  Distance  Path");
            for(int i = 0; i < matrixGraph2.GetLength(0); i++)
            {
                Console.WriteLine($"  {i+1}       {distance[i]}       {path[i]}");
            }
        }

        private void ShortestPath(int[,] graph, int start, out int[] distance, out int[] path)
        {
            int size = graph.GetLength(0);
            bool[] visited = new bool[size];

            distance = new int[size];
            path = new int[size];

            for(int i = 0; i < size; i++)
            {
                distance[i] = graph[start, i];
                path[i] = graph[start, i] < INF ? start : -1;
            }

            for(int i = 0; i < size; i++)
            {
                // 1. 방문하지 않은 정점 중 가장 가까운 정점부터 탐색
                int next = -1;
                int minCost = INF;
                for (int j = 0; j < size; j++)
                {
                    if (!visited[j] && distance[j] < minCost)
                    {
                        next = j;
                        minCost = distance[j];
                    }
                }

                // 2. 직접연결된 거리보다 거쳐서 더 짧아진다면 갱신
                for(int j = 0; j < size; j++)
                {
                    // distance[j] : 목적지까지 직접 연결된 거리
                    // distance[next] : 탐색중인 정점까지 거리
                    // graph[next, j] : 탐색중인 정점부터 목적지의 거리
                    if (distance[j] > distance[next] + graph[next, j]) 
                    {
                        distance[j] = distance[next] + graph[next, j];
                        path[j] = next;
                    }
                }

                visited[next] = true;
            }


        }
    }
}
