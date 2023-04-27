using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// Queue를 나타냅니다. 순환 배열(환영 배열)로 만들어져 있습니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MyQueue<T> : IEnumerable<T>, IEnumerable
    {
        // Enumerator Structure //

        /// <summary>
        /// IEnumerator 인터페이스를 상속받아 Queue를 반복시킬 수 있는 반복기 메서드 및 프로퍼티를 구현
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private MyQueue<T> _queue;
            private int _index;
            private T _current;

            public T Current { get { return _current; } }

            object IEnumerator.Current { get { return Current; } }

            // MyQueue 자신을 Enumerator가 알게해야 한다.
            public Enumerator(MyQueue<T> queue)
            {
                _queue = queue;
                _index = -1;
                _current = default(T);
            }

            public void Dispose()
            {

            }

            // 현재 인덱스의 값을 반환하며 다음 인덱스로 옮긴다.
            public bool MoveNext()
            {
                // 인덱스를 현재 상황(헤드)으로 갱신시켜줘야한다.
                if (_index == -1)
                {
                    _index = _queue.head;
                }
                else
                {
                    _index = (_index + 1) % _queue.array.Length;
                }

                // 인덱스가 Tail에 도착했을 때, Reset : 초기화
                if (_index == _queue.tail)
                {
                    Reset();
                    return false;
                }

                // 값 밚환,
                _current = _queue.array[_index];
                return true;
            }

            // 인덱스를 기존의 인덱스로 돌리고 Current를 초기화 시킨다.
            public void Reset()
            {
                _index = -1;
                _current = default(T);
            }
        }

        // Property //

        private const int DefaultCapacity = 4;

        private T[] array = new T[DefaultCapacity + 1];
        private int head = 0;
        private int tail = 0;

        /// <summary>
        /// Queue 요소의 개수
        /// </summary>
        public int Count
        {
            get
            {
                if (head <= tail)
                    return tail - head;
                else
                    return tail - head + array.Length;
            }
        }

        // Constructor //

        public MyQueue()
		{

		}

        public MyQueue(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Enqueue(item);
        }

        // Method //

        /// <summary>
        /// Queue의 tail 부분에 요소를 삽입하고, tail을 다음으로 옮긴다.
        /// Queue의 모든 요소가 다 차 있을경우 Queue의 크기를 늘려준다.
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {

            if (IsFull())
                Grow();

            array[tail] = item;
            MoveNext(ref tail);
        }

        /// <summary>
        /// Queue의 head 부분에 요소를 꺼내어 반환받고
        /// head를 다음으로 옮긴다. Queue 요소가 비어있을 경우 예외처리를 해준다.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Dequeue()
        {
            if (array == null || IsEmpty())
                throw new InvalidOperationException();

            T result = array[head];
            MoveNext(ref head);
            return result;
        }

        /// <summary>
        /// Dequeue를 시도해보고, array가 비어있을 경우 false와 실패한 결과값을 반환한다.
        /// 정상일 경우 정상적인 Dequeue를 실행한다.
        /// Queue 요소가 비어있을 경우 false를 반환하고 결과를 Default(T)로 전달한다.
        /// 아닐 경우 Queue의 head 부분에 요소를 꺼내어 반환받고 head를 다음으로 옮긴다. true를 반환한다.
        /// head를 다음으로 옮긴다.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryDequeue(out T result)
        {
            if (IsEmpty())
            {
                result = default(T);
                return false;
            }

            result = array[head];
            MoveNext(ref head);
            return true;
        }

        /// <summary>
        /// Queue의 head 부분에 요소를 꺼내어 반환받고
        /// Queue 요소가 비어있을 경우 예외처리를 해준다.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Peek()
        {
            if (array == null || IsEmpty())
                throw new InvalidOperationException();

            return array[head];
        }

        /// <summary>
        /// Peek을 시도해보고, array가 비어있을 경우 false와 실패한 결과값을 반환한다.
        /// 정상일 경우 정상적인 Peek을 실행한다.
        /// Queue 요소가 비어있을 경우 false를 반환하고 결과를 Default(T)로 전달한다.
        /// 아닐 경우 Queue의 head 부분에 요소를 꺼내어 반환받고 true를 반환한다.
        /// head를 다음으로 옮긴다.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryPeek(out T result)
        {
            if (array == null || IsEmpty())
            {
                result = default(T);
                return false;
            }

            result = array[head];
            return true;
        }

        /// <summary>
        /// Queue를 초기화 시켜준다. 원래 상태로 복구해준다.
        /// </summary>
        public void Clear()
        {
            array = new T[DefaultCapacity + 1];
            head = 0;
            tail = 0;
        }

        /// <summary>
        /// Queue는 순환 배열이므로 
        /// 참조로 받은 매개변수를 다음으로 넘어갈 때, 배열의 끝에 인덱스면
        /// 0으로, 아닐경우엔 인덱스를 증가시켜준다.
        /// </summary>
        /// <param name="index"></param>
        private void MoveNext(ref int index)
        {
            index = (index == array.Length - 1) ? 0 : ++index;
        }

        /// <summary>
        /// head와 tail이 같아진 시점에는 비어있는 큐로 판단하고
        /// True를 반환한다.
        /// </summary>
        /// <returns></returns>
        private bool IsEmpty()
        {
            return head == tail;
        }

        /// <summary>
        /// tail이 head보다 앞에있을 때 tail을 증가시켰을 때 head랑 같아진 상황에서는
        /// Queue에 전부 차있는 것으로 간주하고 true를 반환한다
        /// head가 tail보다 앞에있을 때는 head가 첫번째 인덱스이며, tail이 배열의 끝에
        /// 위치해 있을때 전부 차있는 것으로 간주하고 true를 반환한다.
        /// </summary>
        /// <returns></returns>
        private bool IsFull()
        {
            if (head > tail)
                return head == tail + 1;
            else
                return head == 0 && tail == array.Length - 1;
        }

        /// <summary>
        /// IsFull 상태 일 때, Grow를 진행시켜준다.
        /// 배열의 크기를 두배로 늘리고 만약 head가 tail보다 앞에있을 때 head부터 tail까지 그 요소들을 전부 그냥 복사하면 되지만
        /// tail이 head 보다 앞에있을 때는 head에 있는 요소부터 배열의 끝까지 새로운 배열의 0번(처음)인덱스부터
        /// 그 요소만큼 옮겨주고, 나머지 배열의 0번(처음)부터 tail까지의 요소를 그 붙여준 배열 뒤에 옮겨준다.
        /// </summary>
        private void Grow()
        {
            T[] newArray = new T[array.Length * 2];
            if (!IsEmpty())
            {
                if (head < tail)
                    Array.Copy(array, head, newArray, 0, tail);
                else
                {
                    Array.Copy(array, head, newArray, 0, array.Length - head);
                    Array.Copy(array, 0, newArray, array.Length - head, tail);
                }
            }
            head = 0;
            tail = Count;
            array = newArray;
        }

        /// <summary>
        /// 현재 배열의 0번째 부터 마지막 요소까지 새로운 array에 복사시켜준다.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayindex"></param>
        public void CopyTo(T[] array, int arrayindex)
        {
            if (!IsEmpty())
            {
                if (head < tail)
                    Array.Copy(this.array, head, array, arrayindex, tail);
                else
                {
                    Array.Copy(this.array, head, array, arrayindex, array.Length - head);
                    Array.Copy(this.array, 0, array, array.Length - head + arrayindex, tail);
                }
            }
        }

        /// <summary>
        /// 매개변수로 들어온 요소가 현재 배열에 있는지 확인해주는 메서드
        /// 있으면 true 없으면 false를 반환한다.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            foreach(T element in array)
                if(element.Equals(item))
                    return true;
            return false;
        }

        /// <summary>
        /// 요소보다 배열의 크기가 클 때 배열의 크기를
        /// 요소만큼으로 정리해준다.
        /// </summary>
        public void TrimExcess()
        {
            T[] newArray = new T[Count];
            Array.Copy(array, newArray, Count);
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
