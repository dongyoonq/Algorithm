using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class DFS
    {
        List<List<int>> graph = new List<List<int>>();
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
            Init();
            //ListSearch(0, input);
            MatrixSearch(0, input);
        }

        private void Init()
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
            Console.WriteLine($"현재 탐색 요소 {num + 1}");

            if (num + 1 == element)
            {
                Console.WriteLine($"찾으실려는 요소 {element}를 찾았습니다.");
                return;
            }

            visited[num] = true;

            for(int i = 0; i < graph[num].Count; i++)
            {
                int v = graph[num][i];

                if (!visited[v])
                    ListSearch(v, element);
            }
        }

        private void MatrixSearch(int num, int element)
        {
            Console.WriteLine($"현재 탐색 요소 {num + 1}");

            if (num + 1 == element)
            {
                Console.WriteLine($"찾으실려는 요소 {element}를 찾았습니다.");
                return;
            }

            visited[num] = true;

            for (int i = 0; i < graph[num].Count; i++)
            {
                int v = graph[num][i];

                if (matrixGraph1[num, v] && !visited[v])
                    MatrixSearch(v, element);
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
