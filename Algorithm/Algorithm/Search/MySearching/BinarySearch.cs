using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// 정렬되어있어야만 사용가능하다.
    /// </summary>
    internal class BinarySearch
    {
        public bool Search<T>(IList<T> list, T item, int startIndex, int count) where T : IComparable<T>
        {
            if (startIndex < 0 || startIndex + count > list.Count)
                throw new IndexOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();

            int low = startIndex;
            int high = startIndex + count - 1;

            while (low <= high)
            {
                int middle = low + (high - low) / 2;
                int compare = list[middle].CompareTo(item);

                // 배열의 중간값이 요소보다 작다면 큰부분요소 부터 다시 탐색
                if (compare < 0)
                    low = middle + 1;
                // 배열의 중간값이 요소보다 크다면 작은부분요소 부터 다시 탐색
                else if (compare > 0)
                    high = middle - 1;
                // 배열의 중간값이 요소랑 같으면 True반환
                else
                    return true;
            }

            return false;
        }
    }
}
