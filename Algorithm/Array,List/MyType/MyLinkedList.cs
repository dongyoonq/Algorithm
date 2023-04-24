using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// 양방향 연결 리스트(Linked List)를 나타냅니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyLinkedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISerializable, IDeserializationCallback
    {
        // Enumerator Structure //

        /// <summary>
        /// IEnumerator 인터페이스를 상속받아 메서드 및 프로퍼티를 구현
        /// </summary>
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            // Pointer 노드가 Head 부터 이동하며 MyLinkedList가 가진 노드를 하나씩 가져온다.
            private MyLinkedList<T> list;
            private MyLinkedListNode<T> pointer;
            private T _current;

            public T Current { get { return _current; } }

            object? IEnumerator.Current { get { return _current; } }

            // MyLinkedList 자신을 Enumerator가 알게해야 한다.
            public Enumerator(MyLinkedList<T> list)
            {
                this.list = list;
                _current = default(T);
                pointer = list.head;
            }

            public void Dispose()
            {

            }

            // 현재 노드의 값을 반환하며 Pointer를 다음 노드로 옮긴다.
            public bool MoveNext()
            {
                if (pointer == null)
                {
                    Reset();
                    return false;
                }

                _current = pointer.Value;
                pointer = pointer.Next;
                return true;
            }

            // Pointer를 기존의 헤드로 돌리고 Current를 초기화 시킨다.
            public void Reset()
            {
                pointer = list.head;
                _current = default(T);
            }

        }

        //  Property  //

        private int count = 0;
        private MyLinkedListNode<T>? head = null;
        private MyLinkedListNode<T>? tail = null;
        private SerializationInfo siInfo;

        /// <summary>
        /// LinkedList<T>의 첫 번째 노드를 가져옵니다.
        /// </summary>
        public MyLinkedListNode<T>? First { get { return head; } }

        /// <summary>
        /// LinkedList<T>의 마지막 노드를 가져옵니다.
        /// </summary>
        public MyLinkedListNode<T>? Last { get { return tail; } }

        /// <summary>
        /// LinkedList<T>에 실제로 포함된 노드의 수를 가져옵니다.
        /// </summary>
        public int Count { get { return count; } }

        /// <summary>
        /// Class를 읽기전용으로 만들어 진것인지 확인하는 프로퍼티, ICollection 인터페이스 일부
        /// </summary>
        public bool IsReadOnly { get; }

        //  Constructor  //

        /// <summary>
        /// 비어 있는 LinkedList<T> 클래스의 새 인스턴스를 초기화합니다
        /// </summary>
        public MyLinkedList()
        {

        }

        /// <summary>
        /// 지정한 LinkedList<T>에서 복사된 요소가 포함되어 있고 
        /// 복사된 요소의 수를 수용하는 충분한 용량을 가지는 IEnumerable 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="collection"></param>
        public MyLinkedList(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                AddLast(item);
        }

        /// <summary>
        /// LinkedList<T>의 시작 위치에 지정한 값이 포함된 새 노드를 추가합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MyLinkedListNode<T> AddFirst(T value)
        {
            // 1. 새로운 노드 생성
            MyLinkedListNode<T> node = new MyLinkedListNode<T>(this, value);

            // 2. 연결구조 바꾸기
            if(head != null)
            {
                node.next = head;
                head.prev = node;
            }
            else
            {
                head = node;
                tail = node;
            }

            // 3. 새로운 노드를 head 노드로 지정
            this.head = node;

            count++;

            return node;
        }

        //  Method  //

        /// <summary>
        /// LinkedList<T> 의 시작 위치에 지정한 새 노드를 추가합니다.
        /// </summary>
        /// <param name="node"></param>
        public void AddFirst(MyLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException();

            if (head != null)
            {
                node.next = head;
                head.prev = node;
            }
            else
            {
                head = node;
                tail = node;
            }

            this.head = node;

            count++;
        }

        /// <summary>
        /// LinkedList<T> 의 끝에 지정한 값이 포함된 새 노드를 추가합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MyLinkedListNode<T> AddLast(T value)
        {
            // 1. 새로운 노드 생성
            MyLinkedListNode<T> node = new MyLinkedListNode<T>(this, value);

            // 2. 연결구조 바꾸기
            if (tail != null)
            {
                node.prev = tail;
                tail.next = node;
            }
            else
            {
                head = node;
                tail = node;
            }

            // 3. 새로운 노드를 head 노드로 지정
            this.tail = node;

            count++;

            return node;
        }

        /// <summary>
        /// LinkedList<T>의 끝에 지정한 새 노드를 추가합니다.
        /// </summary>
        /// <param name="node"></param>
        public void AddLast(MyLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException();

            if (tail != null)
            {
                node.prev = tail;
                tail.next = node;
            }
            else
            {
                head = node;
                tail = node;
            }

            this.tail = node;

            count++;
        }

        /// <summary>
        /// LinkedList<T>의 지정한 기존 노드 다음에 지정한 값이 포함된 새 노드를 추가합니다
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public MyLinkedListNode<T> AddAfter(MyLinkedListNode<T> node, T value)
        {
            InteralFind(node);

            MyLinkedListNode<T> newNode = new MyLinkedListNode<T>(this, value);

            if (node == null || newNode == null)
                throw new ArgumentNullException();

            newNode.prev = node;
            if (node.next != null)
            {
                newNode.next = node.next;
                newNode.next.prev = newNode;
            }
            else tail = newNode;
            node.next = newNode;

            count++;

            return newNode;
        }

        /// <summary>
        /// LinkedList<T>의 지정한 기존 노드 다음에 지정한 새 노드를 추가합니다.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newNode"></param>
        /// <returns></returns>
        public MyLinkedListNode<T> AddAfter(MyLinkedListNode<T> node, MyLinkedListNode<T> newNode)
        {
            InteralFind(node);

            if (node == null || newNode == null)
                throw new ArgumentNullException();

            newNode.prev = node;
            if (node.next != null)
            {
                newNode.next = node.next;
                newNode.next.prev = newNode;
            }
            else tail = newNode;
            node.next = newNode;

            count++;

            return newNode;
        }

        /// <summary>
        /// LinkedList<T>의 지정한 기존 노드 앞에 지정한 값이 포함된 새 노드를 추가합니다.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public MyLinkedListNode<T> AddBefore(MyLinkedListNode<T> node, T value)
        {
            InteralFind(node);

            MyLinkedListNode<T> newNode = new MyLinkedListNode<T>(this, value);

            if (node == null || newNode == null)
                throw new ArgumentNullException();

            newNode.next = node;
            if (node.prev != null)
            {
                newNode.prev = node.prev;
                newNode.prev.next = newNode;
            }
            else head = newNode;
            node.prev = newNode;

            count++;

            return newNode;
        }

        /// <summary>
        /// LinkedList<T>의 지정한 기존 노드 앞에 지정한 새 노드를 추가합니다.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newNode"></param>
        /// <returns></returns>
        public MyLinkedListNode<T> AddBefore(MyLinkedListNode<T> node, MyLinkedListNode<T> newNode)
        {
            InteralFind(node);

            if (node == null || newNode == null)
                throw new ArgumentNullException();

            newNode.next = node;
            if (node.prev != null)
            {
                newNode.prev = node.prev;
                newNode.prev.next = newNode;
            }
            else head = newNode;
            node.prev = newNode;

            count++;

            return newNode;
        }

        /// <summary>
        /// LinkedList<T>의 시작 위치에서 노드를 제거합니다.
        /// </summary>
        public void RemoveFirst()
        {
            if (count == 0)
                throw new InvalidOperationException("LinkedList<T>가 비어 있습니다.");

            if (count == 1)
                Clear();
            else
            {
                head.next.prev = null;
                head = head.next;
                count--;
            }
        }

        /// <summary>
        /// LinkedList<T>의 끝에서 노드를 제거합니다.
        /// </summary>
        public void RemoveLast()
        {
            if (count == 0)
                throw new InvalidOperationException("LinkedList<T>가 비어 있습니다.");

            if (count == 1)
                Clear();
            else
            {
                tail.prev.next = null;
                tail = tail.prev;
                count--;
            }
        }

        /// <summary>
        /// LinkedList<T> 에서 맨 처음 발견되는 지정된 값을 제거합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(T value)
        {
            if(this == null || count == 0)
                throw new InvalidOperationException("LinkedList<T>가 비어 있습니다.");

            MyLinkedListNode<T> pointer = head;

            while (pointer != null)
            {
                if(pointer.Value.Equals(value))
                {
                    RemoveNode(pointer);
                    return true;
                }

                pointer = pointer.next;
            }

            return false;
        }

        /// <summary>
        /// LinkedList<T>에서 지정된 노드를 제거합니다.
        /// </summary>
        /// <param name="node"></param>
        public void Remove(MyLinkedListNode<T> node)
        {
            InteralFind(node);

            if (node.List == null || count == 0)
                throw new InvalidOperationException("LinkedList<T>가 비어 있습니다.");

            if (node == null)
                throw new ArgumentNullException("node가 null 입니다.");

            RemoveNode(node);
        }

        /// <summary>
        /// 내부적으로 특정 노드를 제거해주는 실제 메서드
        /// </summary>
        /// <param name="node"></param>
        private void RemoveNode(MyLinkedListNode<T> node)
        {
            if(node == head)
                RemoveFirst();
            else if (node == tail)
                RemoveLast();
            else
            {
                node.next.prev = node.prev;
                node.prev.next = node.next;
                node.prev = null;
                node.next = null;
                count--;
            }
        }

        /// <summary>
        /// LinkedList<T>에서 노드를 모두 제거합니다.
        /// </summary>
        public void Clear()
        {
            count = 0;
            
            head = null;
            tail = null;
        }

        /// <summary>
        /// 특정 조건에 맞는 요소를 순회하며 그 요소가 List에 있으면 True, 아니면 False를 반환하는 메서드
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            MyLinkedListNode<T> pointer = head;

            while (pointer != null)
            {
                if (pointer.Value.Equals(value))
                    return true;

                pointer = pointer.next;
            }

            return false;
        }

        /// <summary>
        /// 특정 조건에 맞는 요소를 순회하며 처음 나타나는 Node를 반환하는 메서드
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MyLinkedListNode<T>? Find(T value)
        {
            MyLinkedListNode<T> pointer = head;

            while (pointer != null)
            {
                if (pointer.Value.Equals(value))
                    return pointer;

                pointer = pointer.next;
            }

            return null;
        }

        /// <summary>
        /// 특정 조건에 맞는 요소를 역순회하며 처음 나타나는 Node를 반환하는 메서드
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MyLinkedListNode<T>? FindLast(T value)
        {
            MyLinkedListNode<T> pointer = tail;

            while (pointer != null)
            {
                if (pointer.Value.Equals(value))
                    return pointer;

                pointer = pointer.prev;
            }

            return null;
        }

        /// <summary>
        /// 매개변수로 받은 node가 현재 LinkedList에 있는지 확인해 예외처리를 해주는 메서드
        /// </summary>
        /// <param name="node"></param>
        private void InteralFind(MyLinkedListNode<T> node)
        {
            MyLinkedListNode<T> pointer = head;
            bool tf = false;

            while (pointer != null)
            {
                if (pointer == node)
                {
                    tf = true;
                    break;
                }

                pointer = pointer.next;
            }

            if (!tf)
                throw new InvalidOperationException("node가 LinkedList<T>에 없습니다.");
        }

        /// <summary>
        /// 대상 배열의 지정된 인덱스에서 시작하여 전체 LinkedList<T>을 호환되는 1차원 Array에 복사합니다.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(T[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException();

            if (count + index > array.Length || index < 0)
                throw new ArgumentOutOfRangeException();

            MyLinkedListNode<T> pointer = head;
            while (pointer != null)
            {
                array[index] = pointer.Value;
                index++; pointer = pointer.next;
            }
        }

        //  Interface Overriding  //

        /// <summary>
        /// LinkedList<T>를 반복하는 열거자를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// ISerializable 인터페이스를 구현하고 LinkedList<T> 인스턴스를 직렬화하는 데 필요한 데이터를 반환하는 메서드
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("LinkedList", this);
            info.AddValue("Count", this.count);
            info.AddValue("Head", this.head);
            info.AddValue("Tail", this.tail);
        }

        /// <summary>
        /// ISerializable 인터페이스를 구현하고, deserialization이 완료되면 deserialization 이벤트를 발생시킵니다.
        /// </summary>
        /// <param name="sender"></param>
        public void OnDeserialization(object sender)
        {
            this.siInfo = null;
        }

        /// <summary>
        /// ICollection 인터페이스를 구현(AddFirst 메서드와 동일)
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            AddFirst(item);
        }
    }

    /// <summary>
    /// LinkedList<T>의 노드를 나타냅니다. 이 클래스는 상속될 수 없습니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class MyLinkedListNode<T>
    {
        //  Property  //

        private T value;
        private MyLinkedList<T> list;
        internal MyLinkedListNode<T> prev;
        internal MyLinkedListNode<T> next;

        /// <summary>
        /// 노드에 포함된 값을 가져옵니다.
        /// </summary>
        public T Value { get { return value; } set { value = this.value; } }

        /// <summary>
        /// 노드에서 보유한 값에 대한 참조를 가져옵니다.
        /// </summary>
        public ref T ValueRef { get { return ref value; } }

        /// <summary>
        /// LinkedList<T>이 속하는 LinkedListNode<T>을 가져옵니다.
        /// </summary>
        public MyLinkedList<T> List { get { return list; } }

        /// <summary>
        /// LinkedList<T>의 이전 노드를 가져옵니다.
        /// </summary>
        public MyLinkedListNode<T> Previous { get { return prev; } }

        /// <summary>
        /// LinkedList<T>의 다음 노드를 가져옵니다.
        /// </summary>
        public MyLinkedListNode<T> Next { get { return next; } }

        //  Constructor  //

        /// <summary>
        /// LinkedListNode<T> 클래스의 새 인스턴스를 디폴트로 초기화합니다.
        /// </summary>
        internal MyLinkedListNode()
        {
            this.list = null;
            this.prev = null;
            this.next = null;
            this.value = default(T);
        }

        /// <summary>
        /// 지정한 값을 포함하는 list 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        internal MyLinkedListNode(MyLinkedList<T> list, T value)
        {
            this.list = list;
            this.prev = null;
            this.next = null;
            this.value = value;
        }

        /// <summary>
        /// 지정한 값을 포함하는 LinkedListNode<T> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="value"></param>
        public MyLinkedListNode(T value)
        {
            this.list = null;
            this.prev = null;
            this.next = null;
            this.value = value;
        }
    }
}
