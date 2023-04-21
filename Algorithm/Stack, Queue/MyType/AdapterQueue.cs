using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    // 어댑터 패턴 (Adapter Pattern)
    // 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환

    // GC 발동이 잦아 C# 에서는 사용안함

    internal class AdapterQueue<T>
    {
        private LinkedList<T> container;

        public AdapterQueue()
        {
            container = new LinkedList<T>();
        }

        public void Enqueue(T item)
        {
            container.AddLast(item);
        }

        public T Dequeue()
        {
            T item = container.First.Value;
            container.RemoveFirst();
            return item;
        }

        public T Peek()
        {
            return container.First.Value;
        }
    }
}
