using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// Stack를 나타냅니다. 배열로 만들어져 있습니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MyStack<T> : IEnumerable<T>, IEnumerable
    {
        // Enumerator Structure //

        /// <summary>
        /// IEnumerator 인터페이스를 상속받아 Stack 반복시킬 수 있는 반복기 메서드 및 프로퍼티를 구현
        /// </summary>
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private MyStack<T> _stack;
            private int _index;
            private T _current;

            public T Current { get { return _current; } }

            object IEnumerator.Current { get { return Current; } }

            // MyStack 자신을 Enumerator가 알게해야 한다.
            public Enumerator(MyStack<T> stack)
            {
                _stack = stack;
                _index = -2;
                _current = default(T);
            }

            public void Dispose()
            {

            }

            // 현재 인덱스의 값을 반환하며 다음 인덱스로 옮긴다.
            public bool MoveNext()
            {
                // 인덱스를 현재 상황으로 갱신시켜줘야한다.
                if (_index == -2)
                {
                    _index = _stack.Count - 1;
                }

                // 인덱스가 -1에 도착했을 때, Reset : 초기화
                if (_index < 0)
                {
                    Reset();
                    return false;
                }

                // 값 밚환,
                _current = _stack.array[_index--];
                return true;
            }

            // 인덱스를 기존의 인덱스로 돌리고 Current를 초기화 시킨다.
            public void Reset()
            {
                _index = -2;
                _current = default(T);
            }
        }

        // Property //

        private T[] array;
        private int size;

        public int Count { get { return size; } }

        // Constructor //

        public MyStack(int capacity)
        {
            array = new T[capacity];
        }

        public MyStack(IEnumerable<T> collection)
        {
            foreach(T item in collection)
                Push(item);
            TrimExcess();
        }

        /// <summary>
        /// Push 메서드는 스택에 아이템을 추가하는 메서드입니다.
        /// 만약 배열이 null이라면 크기가 4인 배열을 생성하고, 아이템을 추가합니다.
        /// 만약 배열이 가득 차 있다면, 배열 크기를 두 배로 확장하고 아이템을 추가합니다.
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (array == null)
            { 
                array = new T[4]; array[size++] = item; 
                return; 
            }

            if (size >= array.Length)
                Grow();

            array[size++] = item;
        }

        /// <summary>
        /// Pop 메서드는 스택에서 아이템을 꺼내는 메서드입니다.
        /// 배열이 null이라면 InvalidOperationException 예외를 발생시킵니다.
        /// 배열의 마지막 아이템을 꺼내 반환합니다.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if(array == null)
                throw new InvalidOperationException();

            return array[size-- - 1];
        }

        /// <summary>
        /// Peek 메서드는 스택의 마지막 아이템을 반환하는 메서드입니다.
        /// 배열이 null이라면 InvalidOperationException 예외를 발생시킵니다.
        /// 배열의 마지막 아이템을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (array == null)
                throw new InvalidOperationException();

            return array[size - 1];
        }

        /// <summary>
        /// Peek 메서드는 스택의 마지막 아이템을 반환하는 메서드입니다.
        /// 배열이 null이라면 InvalidOperationException 예외를 발생시킵니다.
        /// 배열의 마지막 아이템을 반환합니다.
        /// </summary>
        private void Grow()
        {

            T[] newArray = new T[size * 2];
            Array.Copy(array, newArray, size);
            array = newArray;
        }

        /// <summary>
        /// TrimExcess 메서드는 배열 크기를 현재 스택 사이즈에 맞게 조정하는 메서드입니다.
        /// 현재 스택 사이즈와 같은 크기의 배열을 생성하고, 현재 배열의 내용을 새 배열에 복사합니다.
        /// </summary>
        public void TrimExcess()
        {
            T[] newArray = new T[size];
            Array.Copy(array, newArray, size);
            array = newArray;
        }

        //  Interface Overriding  //

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
