using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class IteratorStudy
    {
        // 반복기 (Enuerator(Iterator))
        // 자료구조에 저장되어있는 요소들을 순회하는 인터페이스

        public void Study()
        {

        }
    }

    class iterClass<T> : IEnumerable<T>, IEnumerator
    {
        public int[] its;
        private int position = -1;

        public object Current => this.position;

        public IEnumerator GetEnumerator()
        {
            foreach(var it in its)
                yield return it;
        }

        public bool MoveNext()
        {
            if(position >= its.Length)
                return false;
            position++;
            return true;
        }

        public void Reset()
        {
            position = -1;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
