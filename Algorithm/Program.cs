using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Algorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 자료구조 알고리즘 설명란.
            {
                /* 알고리즘 (Algorithm)
                 * 
                 * 문제를 해결하기 위해 정해진 진행절차나 방법
                 * 컴퓨터에서 알고리즘은 어떠한 행동을 하기 위해서 만들어진 프로그램 명령어의 집합 */

                // < 알고리즘 조건 >
                // 1. 입출력 : 정해진 입력과 출력이 존재해야 함
                // 2. 명확성 : 각 단계마다 단순하고 모호하지 않아야 함
                // 3. 유한성 : 특성 수의 작업 이후에 정지해야 함
                // 4. 효과성 : 모든 과정은 수행 가능해야 함

                // < 알고리즘 성능 >
                // 1. 정확성 : 정확하게 동작하는가?
                // 2. 단순성 : 얼마나 단순한가?
                // 3. 최적성 : 더 이상 개선할 여지가 없을 만큼 최적화되어 있는가?
                // * 4. 작업량 : 얼마나 적은 연산을 수행하는가?
                // * 5. 메모리 사용량 : 얼마나 적은 메모리를 사용하는가?

                /* 자료구조 (DataStructure)
                 * 
                 * 프로그래밍에서 데이터를 효율적인 접근 및 수정을 가능하게 하는 자료의 조직, 관리, 저장을 의미
                 * 데이터 값의 모임, 또 데이터 간의 관계, 그리고 데이터에 적용할 수 있는 함수나 명령을 의미 */

                // < 자료구조 형태 >
                // 선형구조 : 자료간 관계가 1대 1인 구조 ( 배열, 연결리스트, 스택, 큐, 덱 )
                // 비선형구조 : 자료간 관계가 1대 n 혹은 n대 n인 구조 ( 트리, 그래프 )

                // < 알고리즘 & 자료구조 평가 >
                // 컴퓨터에서 알고리즘과 자료구조의 평가는 시간과 공간 두 자원을 얼마나 소모하는지가 효율성의 중점
                // 평균적인 상황에서 최악의 상황에서 자원 소모량이 기준이 됨
                // 일반적으로 시간을 위해 공간이 희생되는 경우가 많음
                // 시간복잡도 : 알고리즘의 시간적 자원 소모량
                // 공간복잡도 : 알고리즘의 공간적 자원 소모량

                // <Big-O 표기법>
                // 알고리즘의 복잡도를 나타내는 점근 표기법
                // 가장 높은 차수의 계수와 나머지 모든 항을 제거하고표기
                // 알고리즘의 대략적인 효율을 파악할 수 있는 수단.
            }

            // 사용자 LinkedList 반복자 사용
            int[] arr = { 5, 4, 6, 8, 3, 2 };
            MyLinkedList<int> list = new MyLinkedList<int>(arr);

            // MyLinkedList Enumerator 생성
            IEnumerator<int> enumerator = list.GetEnumerator();
            
            // 반복자(Enumerator)을 사용한 foreach 구문 사용.
            foreach (int item in list)
                Console.Write($"{item} ");
            
            Console.WriteLine();

            // 반복자(Enumerator)을 사용한 while 구문 사용.
            while (enumerator.MoveNext())
                Console.Write($"{enumerator.Current} ");

            Console.WriteLine();
            /////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine();

            // 사용자 List 반복자 사용
            int[] arr2 = { 1, 6, 8, 43, 21, 12 };
            MyList<int> list2 = new MyList<int>(arr2);

            // MyList Enumerator 생성
            IEnumerator<int> enumerator1 = list2.GetEnumerator();

            // 반복, 인덱스를 통한 for 구문 사용.
            for (int i = 0; i < list2.Count; i++)
                Console.Write($"{list2[i]} ");

            Console.WriteLine();

            // 반복자(Enumerator)을 사용한 foreach 구문 사용.
            foreach (int item in list2)
                Console.Write($"{item} ");

            Console.WriteLine();

            // 반복자(Enumerator)을 사용한 while 구문 사용.
            while (enumerator1.MoveNext())
                Console.Write($"{enumerator1.Current} ");

            Console.WriteLine();
            /////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine();

            // 배열(Array), 와 선형리스트(List) 둘 모두 정렬 가능한 사용자 Sort 사용
            MyList<int> newlist = new MyList<int>() { 3, 4, 5, 76, 7, 98 };

            // 선형 리스트(List) 오름차순 정렬
            Sort(newlist, AscendingSort);
            foreach(int item in newlist)
                Console.Write($"{item} ");

            Console.WriteLine();

            // 선형 리스트(List) 내림차순 정렬
            Sort(newlist, DescendingSort);
            foreach (int item in newlist)
                Console.Write($"{item} ");

            Console.WriteLine();

            // 배열(Array) 오름차순 정렬
            Sort(arr, AscendingSort);
            foreach (int item in arr)
                Console.Write($"{item} ");

            Console.WriteLine();

            // 배열(Array) 내림차순 정렬
            Sort(arr, DescendingSort);
            foreach (int item in arr)
                Console.Write($"{item} ");

            Console.WriteLine();
            /////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine();

            // int 자료구조면 다 평균을 구할수 있는 Average(자료구조) 사용자 Average 사용

            // MyLinkedList<int> 평균 구하기
            Console.WriteLine(Average(list));

            // MyList<int> 평균 구하기
            Console.WriteLine(Average(list2));

            // int 배열 평균 구하기
            Console.WriteLine(Average(arr));

            // LinkedList<int> 평균 구하기
            LinkedList<int> lls = new LinkedList<int>(arr2);
            Console.WriteLine(Average(lls));

            // List<int> 평균 구하기
            List<int> ls = new List<int>(arr);
            Console.WriteLine(Average(ls));

            // Stack<int> 평균 구하기
            Stack<int> stack = new Stack<int>(arr);
            Console.WriteLine(Average(stack));

            // Queue<int> 평균 구하기
            Queue<int> queue = new Queue<int>(arr);
            Console.WriteLine(Average(queue));
        }

        /// 비교 해주는 델리게이트 지정자 선언
        public delegate int Compare<T>(T left, T right);

        /// <summary>
        /// 지정자를 이용해 모든 배열, 리스트를 정렬해주는 메서드
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="compare"></param>
        public static void Sort<T>(IList<T> list, Compare<T> compare)
        {
            for(int i = 0; i < list.Count; i++)
            {
                for(int j = 0; j < list.Count; j++)
                {
                    if(compare(list[i], list[j]) > 0)
                    {
                        T temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }    
        }
        
        /// <summary>
        /// 오름차순을 위한 델리게이트 메서드
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int AscendingSort<T>(T left, T right) where T : IComparable<T>
        {
            if (left.CompareTo(right) > 0)
                return -1;
            else if (left.CompareTo(right) < 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// 내림차순을 위한 델리게이트 메서드
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int DescendingSort<T>(T left, T right) where T : IComparable<T>
        {
            if (left.CompareTo(right) > 0)
                return 1;
            else if (left.CompareTo(right) < 0)
                return -1;
            else
                return 0;
        }

        /// <summary>
        /// IEnumerable<int> 한 모든 자료구조, 자료형을 받아 평균을 구해주는 메서드
        /// int 자료구조의 평균을 구하는 메서드
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static double Average(IEnumerable<int> enumerable)
        {
            int sum = 0, count = 0;
            IEnumerator<int> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            { sum += enumerator.Current; count++; }
            return sum / (double)count;
        }
    }
}
