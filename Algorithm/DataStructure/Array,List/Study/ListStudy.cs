using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm.Array_List
{
    internal class ListStudy
    {
        // 리스트(동적배열 : Dynamic Array)
        // 런타임 중 크기를 확장할 수 있는 배열 기반의 자료구조
        // 배열요소의 갯수를 특정할 수 없는 경우 사용

        static void List()
        {
            List<int> list = new List<int>();

            // 배열 요소 삽입
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            Console.WriteLine(list.FindIndex(x => x == 2));

        }
    }
}
