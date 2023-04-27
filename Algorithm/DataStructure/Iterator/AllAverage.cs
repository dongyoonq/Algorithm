using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    class AllAverage
    {
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
