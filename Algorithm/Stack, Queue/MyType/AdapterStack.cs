using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    // 어댑터 패턴 (Adapter Pattern)

    // 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환

    internal class AdapterStack<T>
    {
        List<T> container;

        public AdapterStack()
        {
            container = new List<T>();
        }

        public AdapterStack(int capacity)
        {
            container = new List<T>(capacity);
        }

        public AdapterStack(IEnumerable<T> enumerable)
        {
            container = new List<T>(enumerable);
        }

        public void Push(T item)
        {
            container.Add(item);
        }

        public T Pop()
        {
            T item = container[container.Count - 1];
            container.RemoveAt(container.Count - 1);
            return item;
        }

        public T Peek()
        {
            return container[container.Count - 1];
        }

        public void TrimExcess()
        {
            container.TrimExcess();
        }
    }
}
