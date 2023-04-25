using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class BFS
    {
        int[,] graph = new int[10, 10];
        bool[] visited = new bool[10];

        private void Search(int num)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(num);
            visited[num] = true;

            while(queue.Count > 0)
            {
                int p = queue.Dequeue();
                Array.Sort(graph);

                for (int i = 0; i < graph.GetLength(p); i++)
                {
                    int v = graph[p,i];
                    if(!visited[v])
                    {
                        visited[v] = true;
                        queue.Enqueue(v);
                    }
                }
            }
        }
    }
}
