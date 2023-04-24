using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm
{
    public class MyPriorityQueue<TElement, TPriority> : IEnumerable<(TElement Element, TPriority Priority)>, IEnumerable, IReadOnlyCollection<(TElement Element, TPriority Priority)>
    {
        public struct Enumerator : IEnumerator<(TElement Element, TPriority Priority)>, IEnumerator
        {
            private MyPriorityQueue<TElement, TPriority> _queue;
            private int _index;
            private TElement _element;
            private TPriority _priority;

            public (TElement Element, TPriority Priority) Current { get { return Current; } }

            (TElement Element, TPriority Priority) IEnumerator<(TElement Element, TPriority Priority)>.Current
            {
                get { return (_element, _priority); }
            }

            object IEnumerator.Current { get { return Current; } }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (_index == (_queue.Count - 1))
                {
                    Reset();
                    return false;
                }

                return (_index++ <= _queue.Count - 1);
            }

            public void Reset()
            {
                _index = -1;
            }
        }

        List<KeyValuePair<TElement, TPriority>> nodes;
        private IComparer<TPriority> comparer;

        public MyPriorityQueue()
        {
            this.nodes = new List<KeyValuePair<TElement, TPriority>>();
            this.comparer = Comparer<TPriority>.Default;
        }

        public MyPriorityQueue(int initialCapacity)
        {
            this.nodes = new List<KeyValuePair<TElement, TPriority>>(initialCapacity);
            this.comparer = Comparer<TPriority>.Default;
        }

        public MyPriorityQueue(IComparer<TPriority>? comparer)
        {
            this.nodes = new List<KeyValuePair<TElement, TPriority>>();
            this.comparer = comparer;
        }

        public MyPriorityQueue(IEnumerable<(TElement Elemnet, TPriority Priority)> items)
        {
            EnqueueRange(items);
        }

        public MyPriorityQueue(int initialCapacity, IComparer<TPriority>? comparer)
        {
            this.nodes = new List<KeyValuePair<TElement, TPriority>>(initialCapacity);
            this.comparer = comparer;
        }

        public MyPriorityQueue(IEnumerable<(TElement Elemnet, TPriority Priority)> items, IComparer<TPriority>? comparer)
        {
            EnqueueRange(items);
            this.comparer = comparer;
        }

        public int Count { get { return nodes.Count; } }
        public IComparer<TPriority> Comparer { get { return comparer; } }

        public void Enqueue(TElement element, TPriority priority)
        {
            KeyValuePair<TElement, TPriority> newNode = new KeyValuePair<TElement, TPriority>(element, priority);
            
            // 1. 가장 뒤에 데이터 추가
            nodes.Add(newNode);
            int newNodeIndex = nodes.Count - 1;

            // 2. 새로운 노드를 힙상태가 유지되도록 승격 작업 반복
            while(newNodeIndex > 0)
            {
                // 2-1. 부모노드 확인
                int parentIndex = GetParentIndex(newNodeIndex);
                KeyValuePair<TElement, TPriority> parentNode = nodes[parentIndex];

                if (comparer.Compare(newNode.Value, parentNode.Value) < 0)
                {
                    nodes[newNodeIndex] = parentNode;
                    nodes[parentIndex] = newNode;
                    newNodeIndex = parentIndex;
                }
                else
                    break;
            }
        }

        public void EnqueueRange(IEnumerable<(TElement Elemnet, TPriority Priority)> items)
        {
            foreach (var item in items)
                Enqueue(item.Elemnet, item.Priority);
        }


        public TElement Dequeue()
        {
            TElement element = nodes[0].Key;
            KeyValuePair<TElement, TPriority> lastNode = nodes[nodes.Count - 1];
            nodes.RemoveAt(nodes.Count - 1);

            int searchIndex = 0;
            while (searchIndex < nodes.Count)
            {
                // case 3
                int leftChildIndex = GetLeftChildIndex(searchIndex);
                int rightChildIndex = GetRightChildIndex(searchIndex);

                // LeftChild & RightChild Exist
                if (rightChildIndex < nodes.Count)
                {
                    int lessChildIndex = comparer.Compare(nodes[leftChildIndex].Value, nodes[rightChildIndex].Value) < 0 ?
                    leftChildIndex : rightChildIndex;

                    if (comparer.Compare(lastNode.Value, nodes[lessChildIndex].Value) > 0)
                    {
                        nodes[searchIndex] = nodes[lessChildIndex];
                        searchIndex = lessChildIndex;
                    }
                    else
                    {
                        nodes[searchIndex] = lastNode;
                        break;
                    }
                }
                // LeftChild Exist
                else if (leftChildIndex < nodes.Count)
                {
                    if (comparer.Compare(lastNode.Value, nodes[leftChildIndex].Value) > 0)
                    {
                        nodes[searchIndex] = nodes[leftChildIndex];
                        searchIndex = leftChildIndex;
                    }
                    else
                    {
                        nodes[searchIndex] = lastNode;
                        break;
                    }
                }
                // Not Exist Child
                else
                {
                    nodes[searchIndex] = lastNode;
                    break;
                }
            }

            return element;
        }


        public bool TryDequeue(out TElement element, out TPriority priority)
        {
            if (nodes.Count == 0)
            {
                element = default(TElement);
                priority = default(TPriority);
                return false;
            }

            element = nodes[0].Key;
            priority = nodes[0].Value;
            Dequeue();
            return true;
        }

        public TElement Peek()
        {
            return nodes[0].Key;
        }

        public bool TryPeek(out TElement element, out TPriority priority)
        {
            if (nodes.Count == 0)
            {
                element = default(TElement);
                priority = default(TPriority);
                return false;
            }

            element = nodes[0].Key;
            priority = nodes[0].Value;
            return true;
        }

        public TElement EnqueueDequeue(TElement element, TPriority priority)
        {
            Enqueue(element, priority);
            return Dequeue();
        }

        public void Clear()
        {
            this.nodes = new List<KeyValuePair<TElement, TPriority>>();
            this.comparer = Comparer<TPriority>.Default;
        }

        public void EnsureCapacity(int capacity)
        {
            List<KeyValuePair<TElement, TPriority>> newNode = new List<KeyValuePair<TElement, TPriority>>(capacity);
            newNode.AddRange(this.nodes);
            this.nodes = newNode;
        }

        public void TrimExcess()
        {
            List<KeyValuePair<TElement, TPriority>> newNode = new List<KeyValuePair<TElement, TPriority>>(Count);
            newNode.AddRange(this.nodes);
            this.nodes = newNode;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private int GetLeftChildIndex(int parentIndex)
        {
            return (parentIndex * 2) + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return (parentIndex * 2) + 2;
        }

        public IEnumerator<(TElement Element, TPriority Priority)> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}
