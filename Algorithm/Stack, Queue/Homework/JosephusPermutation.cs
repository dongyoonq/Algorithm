using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    public class JosephusPermutation
    {
        Queue<int> queue;
        // 전체 수
        private int n;
        // 순서 m 번째
        private int m;

        /// <summary>
        /// 요세푸스 순열 구하는 메서드이다.
        /// </summary>
        public void Start()
        {
            Input();

            Init();

            Calculate();

            Render();
        }

        /// <summary>
        /// 사용자 입력을 받기위한 메서드이다
        /// n까지의 수와 m번째 순서를 입력 받는다.
        /// </summary>
        private void Input()
        {
            Console.Write("전체 수 n 을 입력하세요 : ");
            n = int.Parse(Console.ReadLine());
            Console.Write("순서 m 번째를 입력하세요 : ");
            m = int.Parse(Console.ReadLine());
            Console.WriteLine();
        }

        /// <summary>
        /// n으로 전체 1부터 n까지 수로 Queue를 초기화 시키는 메서드
        /// </summary>
        private void Init()
        {
            queue = new Queue<int>();
            for (int i = 1; i <= n; i++)
                queue.Enqueue(i);
        }

        /// <summary>
        /// 요세푸스 순열 계산하는 메서드
        /// </summary>
        private void Calculate()
        {
            Console.WriteLine("요세푸스 순열 ({0}, {1}) 결과", m, n);

            // 큐에 남은 수가 m보다 작아질 때까지 계속 반복
            while (queue.Count > m - 1)
            {
                // 삭제한 수의 개수
                int cnt = 0;
                // 큐의 현재 크기
                int count = queue.Count;
                // 큐의 현재 크기
                int purpose = count / m;

                // 큐에 있는 수 개수만큼 반복
                for (int i = 1; i <= count; i++)
                {
                    // 목표로 하는 삭제 수를 다 달성하면 중지
                    if (cnt == purpose)
                        break;

                    // 삭제 대상이면 큐에서 삭제하고 개수 증가
                    if (i % m == 0)
                    {
                        Console.Write($"{queue.Dequeue()}, ");
                        cnt++;
                        continue;
                    }

                    // 삭제 대상이 아니면 큐의 맨 뒤로 이동
                    queue.Enqueue(queue.Dequeue());
                }
                Console.WriteLine();
            }

            // 큐에 남은 수 출력
            foreach (int item in queue)
                Console.Write($"{item}, ");
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// 큐에서 남은 수를 하나씩 빼서 출력시키는 메서드
        /// </summary>
        private void Render()
        {
            Console.WriteLine("마지막에 남은 수");
            while (queue.Count != 0)
                Console.Write($"{queue.Dequeue()} ");
        }
    }
}