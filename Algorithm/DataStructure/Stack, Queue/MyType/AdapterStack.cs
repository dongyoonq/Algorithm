using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    // 어댑터 패턴 (Adapter Pattern)
    // 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환
    internal class AdapterStack<T> : IEnumerable<T>, IEnumerable
    {
        // Property //

        /// 내부적으로 사용할 List 인스턴스를 저장한다.
        private List<T> container;

        // Constructor //

        /// <summary>
        /// 새로운 빈 List를 생성하는 생성자
        /// </summary>
        public AdapterStack()
        {
            container = new List<T>();
        }

        /// <summary>
        /// 초기 용량을 지정하여 새로운 빈 List를 생성하는 생성자
        /// </summary>
        /// <param name="capacity"></param>
        public AdapterStack(int capacity)
        {
            container = new List<T>(capacity);
        }

        /// <summary>
        /// 기존의 IEnumerable<T> 컬렉션을 사용하여 새로운 List를 생성하는 생성자
        /// </summary>
        /// <param name="enumerable"></param>
        public AdapterStack(IEnumerable<T> enumerable)
        {
            container = new List<T>(enumerable);
        }

        // Method //

        /// <summary>
        /// 스택에 새 항목을 추가하는 메서드, List의 Add() 메서드를 호출하여 새 항목을 추가한다.
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            container.Add(item);
        }

        /// <summary>
        /// 스택에서 가장 위에 있는 항목을 제거하고 반환하는 메서드 
        /// List의 마지막 항목을 RemoveAt() 메서드를 호출하여 제거하고 해당 항목을 반환한다.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T item = container[container.Count - 1];
            container.RemoveAt(container.Count - 1);
            return item;
        }

        /// <summary>
        /// 스택에서 가장 위에 있는 항목을 반환하지만 제거하지 않는 메서드, List의 마지막 항목을 반환한다.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return container[container.Count - 1];
        }

        /// <summary>
        /// 스택의 용량을 현재 항목 수에 맞게 줄이는 메서드, List의 TrimExcess() 메서드를 호출하여 처리
        /// </summary>
        public void TrimExcess()
        {
            container.TrimExcess();
        }

        //  Interface Overriding  //

        /// <summary>
        /// List의 GetEnumerator를 그대로 이용
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
