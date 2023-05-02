using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class BFS
    {
        List<List<int>> graph = new List<List<int>>();

        int[] parent = new int[9];
        bool[] visited = new bool[9];

        bool[,] matrixGraph1 = new bool[9, 9]
        {
            { false, true, true, true, false, false, false, false, false },
            { true, false, false, false, true, false, false, false, false  },
            { true, false, false, false, false, true, true, false, false },
            { true, false, false, false, false, false, false, false, false },
            { false, true, false, false, false, false, false, true, true },
            { false, false, true, false, false, false, false, false, false },
            { false, false, true, false, false, false, false, false, false },
            { false, false, false, false, true, false, false, false, false },
            { false, false, false, false, true, false, false, false, false },
        };

        const int INF = int.MaxValue;


        int[,] matrixGraph2 = new int[9, 9]
        {
            {  0, 1, 1, 1, INF, INF, INF, INF, INF },
            {  1, 0, INF, INF, 1, INF, INF, INF, INF },
            {  1, INF, 0, INF, INF, 1, 1, INF, INF },
            {  1, INF, INF, 0, INF, INF, INF, INF, INF },
            {  INF, 1, INF, INF, 0, INF, INF, 1, 1 },
            {  INF, INF, 1, INF, INF, 0, INF, INF, INF },
            {  INF, INF, 1, INF, INF, INF, 0, INF, INF },
            {  INF, INF, INF, INF, 1, INF, INF, 0, INF },
            {  INF, INF, INF, INF, 1, INF, INF, INF, 0 },
        };


        public void Start()
        {
            Console.Write("찾을라는 요소를 입력해주세요 : ");
            int input = int.Parse(Console.ReadLine());
            ListInit();
            //ArrayInit();
            //ListSearch(0, input);
            ListSearch(0, input);
        }

        private void ArrayInit()
        {
            for (int i = 1; i <= 9; i++)
                parent[i - 1] = i;
        }

        private void ListInit()
        {

            for (int i = 0; i < 9; i++)
                graph.Add(new List<int>());

            graph[0].Add(1);
            graph[0].Add(2);
            graph[0].Add(3);
            graph[1].Add(0);
            graph[1].Add(4);
            graph[2].Add(0);
            graph[2].Add(5);
            graph[2].Add(6);
            graph[3].Add(0);
            graph[4].Add(1);
            graph[4].Add(7);
            graph[4].Add(8);
            graph[7].Add(4);
            graph[8].Add(4);
        }

        private void ListSearch(int num, int element)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(num);
            visited[num] = true;

            while (queue.Count > 0)
            {
                int p = queue.Dequeue();

                Console.WriteLine($"현재 탐색 요소 {p + 1}");

                if (p + 1 == element)
                {
                    Console.WriteLine($"찾으실려는 요소 {element}를 찾았습니다.");
                    break;
                }
                else
                    Console.WriteLine($"찾으실려는 요소를 찾지 못했습니다. 계속탐색합니다..");

                for (int i = 0; i < graph[p].Count; i++)
                {
                    int v = graph[p][i];
                    if (matrixGraph1[p, v] && !visited[v])
                    {
                        visited[v] = true;
                        queue.Enqueue(v);
                    }
                }
            }
        }

        private void MatrixSearch(int num, int element)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(num);
            visited[num] = true;

            while (queue.Count > 0)
            {
                int p = queue.Dequeue();

                Console.WriteLine($"현재 탐색 요소 {p + 1}");

                if (p + 1 == element)
                {
                    Console.WriteLine($"찾으실려는 요소 {element}를 찾았습니다.");
                    break;
                }
                else
                    Console.WriteLine($"찾으실려는 요소를 찾지 못했습니다. 계속탐색합니다..");

                for (int i = 0; i < parent.Length; i++)
                {
                    int v = parent[i];
                    if (matrixGraph1[p, v - 1] && !visited[v - 1])
                    {
                        visited[v - 1] = true;
                        queue.Enqueue(v - 1);
                    }
                }
            }
        }
    }
}
