using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class DFS
    {
        int[,] graph = new int[10,10];
        bool[] visited = new bool[10];

        public void Start()
        {
            int i = 1;
            // PreOrderTraversal
            PreOrderTravel(i);
            Console.WriteLine();
            // InOrderTraversal
            InOrderTravel(i);
            Console.WriteLine();
            // PostOrderTraversal
            PostOrderTravel(i);
            Console.WriteLine();
        }

        private void Search(int num)
        {
            visited[num] = true;

            Array.Sort(graph);
            for(int i = 0; i < graph.GetLength(num); i++)
            {
                int v = graph[num,i];
                if(!visited[v])
                    Search(v);
            }
        }

        // 전위 순회
        private void PreOrderTravel(int num)
        {
            if (num > 7) return;

            Console.Write($"{num} ");       // 자신
            PreOrderTravel(num * 2);        // 왼쪽 자식
            PreOrderTravel(num * 2 + 1);    // 오른쪽 자식
        }

        // 중위 순회
        private void InOrderTravel(int num)
        {
            if (num > 7) return;

            InOrderTravel(num * 2);         // 왼쪽 자식
            Console.Write($"{num} ");       // 자신
            InOrderTravel(num * 2 + 1);     // 오른쪽 자식
        }

        // 후위 순회
        private void PostOrderTravel(int num)
        {
            if (num > 7) return;

            PostOrderTravel(num * 2);
            PostOrderTravel(num * 2 + 1);
            Console.Write($"{num} ");
        }
    }
}
