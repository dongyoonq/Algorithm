using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class MyLinkedList<T> : IEnumerable<T>
    {
        private int count = 0;
        private MyLinkedListNode<T>? head = null;
        private MyLinkedListNode<T>? tail = null;

        public MyLinkedList()
        {

        }
        
        public MyLinkedList(IEnumerable<T> collection)
        {
            foreach(T item in collection)
                AddLast(item);
        }

        public MyLinkedListNode<T>? First { get { return head; } }
        public MyLinkedListNode<T>? Last { get { return tail; } }
        public int Count { get { return count; } }

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

        public void AddFirst(MyLinkedListNode<T> node)
        {
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

        public void AddLast(MyLinkedListNode<T> node)
        {
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

        public MyLinkedListNode<T> AddAfter(MyLinkedListNode<T> node, T value)
        {
            MyLinkedListNode<T> newNode = new MyLinkedListNode<T>(this, value);

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

        public MyLinkedListNode<T> AddAfter(MyLinkedListNode<T> node, MyLinkedListNode<T> newNode)
        {
            if (newNode == null)
                throw new NullReferenceException();

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

        public MyLinkedListNode<T> AddBefore(MyLinkedListNode<T> node, T value)
        {
            MyLinkedListNode<T> newNode = new MyLinkedListNode<T>(this, value);

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

        public MyLinkedListNode<T> AddBefore(MyLinkedListNode<T> node, MyLinkedListNode<T> newNode)
        {
            if (newNode == null)
                throw new NullReferenceException();

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

        public bool Remove(T value)
        {
            if (count == 0)
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

        public void Remove(MyLinkedListNode<T> node)
        {
            if (node.List == null || count == 0)
                throw new InvalidOperationException("LinkedList<T>가 비어 있습니다.");

            if (node == null)
                throw new ArgumentNullException("node가 null 입니다.");

            MyLinkedListNode<T> pointer = head;

            while (pointer != null)
            {
                if (pointer == node)
                {
                    RemoveNode(pointer);
                    return;
                }

                pointer = pointer.next;
            }

            throw new InvalidOperationException("node가 현재 LinkedList<T>에 없습니다.");
        }

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

        public void Clear()
        {
            count = 0;
            
            head = null;
            tail = null;
        }

        public MyLinkedListNode<T> Find(T value)
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

        public MyLinkedListNode<T> FindLast(T value)
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
        /// 배열에 특정 인덱스부터 LinkedList의 요소들을 삽입해주는 메서드
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(T[] array, int index)
        {
            if (count + index > array.Length)
                throw new ArgumentOutOfRangeException();

            MyLinkedListNode<T> pointer = head;
            while (pointer != null)
            {
                array[index] = pointer.Value;
                index++; pointer = pointer.next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            MyLinkedListNode<T> pointer = head;
            while(pointer != null)
            { yield return pointer.Value; pointer = pointer.next; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            MyLinkedListNode<T> pointer = head;
            while (pointer != null)
            { yield return pointer.Value; pointer = pointer.next; }
        }
    }

    class MyLinkedListNode<T>
    {
        private T value;
        private MyLinkedList<T> list;
        internal MyLinkedListNode<T> prev;
        internal MyLinkedListNode<T> next;

        public T Value { get { return value; } set { value = this.value; } }
        public MyLinkedList<T> List { get { return list; } }
        public MyLinkedListNode<T> Previous { get { return prev; } }
        public MyLinkedListNode<T> Next { get { return next; } }

        public MyLinkedListNode()
        {
            this.list = null;
            this.prev = null;
            this.next = null;
            this.value = default(T);
        }
        public MyLinkedListNode(T value)
        {
            this.list = null;
            this.prev = null;
            this.next = null;
            this.value = value;
        }
        public MyLinkedListNode(MyLinkedList<T> list, T value)
        {
            this.list = list;
            this.prev = null;
            this.next = null;
            this.value = value;
        }
    }
}
