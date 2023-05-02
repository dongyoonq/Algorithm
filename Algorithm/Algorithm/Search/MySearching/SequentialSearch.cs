using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class SequentialSearch
    {
        public bool Search<T>(IList<T> list, T item) where T : IComparable<T>
        {
            // 요소랑 일치하는것이 리스트에 있다면 True반환
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].CompareTo(item) == 0)
                    return true;
            }

            return false;
        }
    }
}
