using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    class AllSort
    {
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
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (compare(list[i], list[j]) > 0)
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


    }
}
