using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Algorithm
{
    internal class ArrayStudy
    {
        // 배열
        // 연속적인 메모리상에 동일한 타입의 요소를 일렬로 저장하는 자료구조
        // 초기화때 정한 크기가 소멸까지 유지됨
        // 배열의 요소는 인덱스를 사용하여 직접적으로 엑세스 가능

        void Arrays()
        {
            int[] arr = new int[256];

            // 인덱스를 통한 접근
            arr[0] = 10;
            int val = arr[0];

        }

        // < 배열의 시간복잡도 >
        // 접근   탐색
        // 0(1)   0(N)

        // int 배열 20번째 자료 접근 : 20번째 자료 주소 = 배열의 주소 + int의 자료형 크기 * 19
        // 데이터가 n개 있을 때 탐색 : 
        public int Find(int[] arr, int data)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == data)
                    return i;
            return -1;
        }

    }
}
