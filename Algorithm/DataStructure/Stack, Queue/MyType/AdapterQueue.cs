using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    // 어댑터 패턴 (Adapter Pattern)
    // 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환

    /// LinkedList는 GC 발동이 잦아 C# 에서는 사용안함
    /// 따라서 Queue는 순환 배열을 이용해 구현

    /// LinkedList 클래스를 사용하여 Queue 기능을 제공하는 AdapterQueue 클래스
    /// T는 큐에 저장될 요소의 타입
    internal class AdapterQueue<T> : IEnumerable<T>, IEnumerable
    {
        // Property //

        /// 내부적으로 사용할 LinkedList 인스턴스를 저장한다.
        private LinkedList<T> container;

        /// <summary>
        /// 새로운 빈 LinkedList 생성하는 생성자
        /// </summary>
        public AdapterQueue()
        {
            container = new LinkedList<T>();
        }

        /// <summary>
        /// 기존의 IEnumerable<T> 컬렉션을 사용하여 새로운 LinkedList를 생성하는 생성자
        /// </summary>
        /// <param name="enumerable"></param>
        public AdapterQueue(IEnumerable<T> enumerable)
        {
            container = new LinkedList<T>(enumerable);
        }

        // Method //

        /// <summary>
        /// 큐의 맨 뒤에 새로운 요소를 추가하는 메서드
        /// 새로운 요소는 LinkedList의 AddLast 메서드를 통해 추가할 수 있다.
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            container.AddLast(item);
        }

        /// <summary>
        /// 큐의 맨 앞에서 요소를 제거하고 반환하는 메서드 
        /// 제거할 요소는 LinkedList의 First 프로퍼티를 통해 얻어지며, 제거는 RemoveFirst 메서드를 통해 이루어진다.
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            T item = container.First.Value;
            container.RemoveFirst();
            return item;
        }

        /// <summary>
        /// 큐의 맨 앞의 요소를 반환하는 메서드
        /// 이는 LinkedList의 First 프로퍼티를 통해 얻어지며, 반환은 그대로 이루어진다.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return container.First.Value;
        }

        //  Interface Overriding  //

        /// <summary>
        /// LinkedList의 GetEnumerator를 그대로 이용
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return container.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
