using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class FindMiddleNumber
    {
        // 최소 힙
        private PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => a - b));
        // 최대 힙
        private PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));

        /// <summary>
        /// Test하기위해 임의로 Queue에 랜덤한 값을 들어오게 해준다.
        /// 1~10000 까지의 중간값을 구하는 메서드이다.
        /// </summary>
        public void Start()
        {
            Random random = new Random();
            bool[] check = new bool[10001];

            for (int i = 1; i <= 10000;)
            {
                int randNum = random.Next(1, 10001);
                if (!check[randNum])
                { 
                    check[randNum] = true;
                    InsertQueue(randNum);
                    i++; 
                }

            }

            // 중간값 계산 : 최대힙의 Top(Peek)
            Console.WriteLine($"1~10000값의 10000개의 중간값 (종복 X) : {maxHeap.Peek()}");
        }

        /// Test하기위해 임의로 Queue에 랜덤한 값을 들어오게 해준다.
        /// 1~10000 까지의 중간값(중복허용)을 구하는 메서드이다.
        public void TestCase1()
        {
            Random random = new Random();

            for (int i = 1; i <= 10000; i++)
            {
                int randNum = random.Next(1, 10001);
                InsertQueue(randNum);
            }

            // 중간값 계산 : 최대힙의 Top(Peek)
            Console.WriteLine($"(1~10000)숫자의 10000개의 중간값 (중복허용): {maxHeap.Peek()}");
        }

        /// Test하기위해 임의로 Queue에 랜덤한 값을 들어오게 해준다.
        /// 랜덤한 값 10000개의 중간값(중복허용)을 구하는 메서드이다.
        public void TestCase2()
        {
            Random random = new Random();

            for (int i = 1; i <= 10000; i++)
            {
                int randNum = random.Next();
                InsertQueue(randNum);
            }

            // 중간값 계산 : 최대힙의 Top(Peek)
            Console.WriteLine($"임의의 숫자의 10000개의 중간값 (중복허용): {maxHeap.Peek()}");
        }

        /// <summary>
        /// 최소힙과 최대힙에 랜덤한 값을 삽입 하면서 현재 중간값을 구하는 알고리즘이다.
        /// 
        /// 1. 제일 처음 최대 힙에 삽입
        /// 2. 최대 힙의 크기는 최소 힙의 크기와 같거나, 하나 더 큼
        /// 3. 최대 힙의 최대 원소는 최소 힙의 최소 원소보다 작거나 같음
        /// - 알고리즘에 맞지 않다면 최대 힙, 최소 힙의 가장 위의 값 swap
        /// 
        /// 4. 이 두가지 규칙을 유지해 준다면 항상 최대 힙 top값이 중간값
        /// /// </summary>
        /// <param name="num"></param>
        private void InsertQueue(int num)
        {
            // 1. 제일 처음 최대 힙에 삽입
            if (maxHeap.Count == 0)
            { maxHeap.Enqueue(num, num); return;  }

            // 2. 최대 힙의 크기는 최소 힙의 크기와 같거나, 하나 더 커야하는 조건, 이 힙이 깨지지 않기 위해
            // 다른말로 : 위 조건이 아니면, 최대힙의 크기가 최소힙보다 하나 더 크면 최소 힙에 추가, 같으면(거나 작으면) 최대 힙에 추가
            if (maxHeap.Count > minHeap.Count)
                minHeap.Enqueue(num, num);
            else
                maxHeap.Enqueue(num, num);

            // 3. 최대 힙의 최대 원소는 최소 힙의 최소 원소보다 작거나 같아야 한다. 이 힙이 깨지지 않기 위해
            // 위 조건이 아니면, 최대 힙, 최소 힙의 가장 위의 값을 swap해준다.
            if (!(maxHeap.Peek() <= minHeap.Peek()))
            {
                int tmp1 = maxHeap.Peek();
                int tmp2 = minHeap.Peek();

                maxHeap.Dequeue();
                minHeap.Dequeue();

                maxHeap.Enqueue(tmp2, tmp2);
                minHeap.Enqueue(tmp1, tmp1);
            }

            // 4.위 조건(규칙)이 유지되어, 최대 힙의 Top(Peek)이 중간 값이 됨
        }
    }
}
