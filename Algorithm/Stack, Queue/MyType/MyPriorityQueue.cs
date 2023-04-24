using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm
{
    public class MyPriorityQueue<TElement, TPriority> : IEnumerable<(TElement Element, TPriority Priority)>, IEnumerable, IReadOnlyCollection<(TElement Element, TPriority Priority)>
    {
        // Enumerator Structure //

        /// <summary>
        /// IEnumerator 인터페이스를 상속받아 PriorityQueue를 반복시킬 수 있는 반복기 메서드 및 프로퍼티를 구현
        /// 우선순위큐에 GetEnumerator, KeyPairValue에 대한 Enumerator 아직 미구현
        /// </summary>
        public struct Enumerator : IEnumerator<(TElement Element, TPriority Priority)>, IEnumerator
        {
            private MyPriorityQueue<TElement, TPriority> _queue;
            private int _index;
            private TElement _element;
            private TPriority _priority;

            public Enumerator(MyPriorityQueue<TElement, TPriority> queue)
            {
                _queue = queue;
                this._index = -1;
                this._priority = default(TPriority);
                this._element = default(TElement);
            }

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

        // Property //

        // Key값(Element), Value값(TPriority)를 가진 List를 가지게한다.
        List<KeyValuePair<TElement, TPriority>> nodes;
        // comparer를 만들어 Comparer에 따라, 힙을 최소로 만들건지, 최대로 만들건지 정한다.
        private IComparer<TPriority> comparer;

        public int Count { get { return nodes.Count; } }
        public IComparer<TPriority> Comparer { get { return comparer; } }

        // Constructor //

        /// <summary>
        /// 생성자를 총 여섯개로 오버로딩해서 구현했다.
        /// 디폴트, 큐의 크기, 비교자, 반복자를 매개변수로 가지게 하여 여러가지 생성자를 만듬.
        /// </summary>
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

        public MyPriorityQueue(IComparer<TPriority> comparer)
        {
            this.nodes = new List<KeyValuePair<TElement, TPriority>>();
            this.comparer = comparer;
        }

        public MyPriorityQueue(IEnumerable<(TElement Elemnet, TPriority Priority)> items)
        {
            EnqueueRange(items);
        }

        public MyPriorityQueue(int initialCapacity, IComparer<TPriority> comparer)
        {
            this.nodes = new List<KeyValuePair<TElement, TPriority>>(initialCapacity);
            this.comparer = comparer;
        }

        public MyPriorityQueue(IEnumerable<(TElement Elemnet, TPriority Priority)> items, IComparer<TPriority> comparer)
        {
            EnqueueRange(items);
            this.comparer = comparer;
        }

        // Method //

        /// <summary>
        /// 요소와 우선순위를 입력받아 Queue에 삽입해주는 메서드다.
        /// 들어온 값으로 KeyValue를 가진 새로운 노드를 만들어주고
        /// 현재 큐에 가장뒤에 추가해준다. 이때 새로운 노드의 우선순위가 부모 노드우선순위 보다 더 높으면
        /// 바꿔주는 승격작업을 해야한다. 높지 않으면 그 위치에 놓고 작업 종료한다.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
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

        /// <summary>
        /// Enumerable한 items를 받아 요소들을 전부 List에 추가해주는 메서드이다.
        /// </summary>
        /// <param name="items"></param>
        public void EnqueueRange(IEnumerable<(TElement Elemnet, TPriority Priority)> items)
        {
            foreach (var item in items)
                Enqueue(item.Elemnet, item.Priority);
        }

        /// <summary>
        /// Queue에 맨위 즉, 우선순위가 제일 높거나 낮은 최상단의 노드를 제거해주고, 그 노드의 요소를 반환하는 메서드다.
        /// Queue의 맨위를 제거해주고 난후 후처리는 다음과 같다.
        /// 1. 맨위의 노드를 맨 마지막 노드로 변경후, 맨 마지막 노드를 제거한다.
        /// 2. 맨위의 노드의 우선순위가 자식 노드우선순위 보다 낮으면 바꿔주는 강등작업을 해야한다. 강등 작업은 이렇다
        /// 3. 자식 노드가 세가지 형태로 존재한다
        /// 3-1. 왼쪽과 오른쪽 자식이 있는경우에, 자신보다 더 우선순위가 높으면 왼쪽 오른쪽 자식 중 더 낮은 자식과 스왑작업을 한다.
        /// 3-2. 왼쪽 자식만 있는경우(하나만 있는경우, 이경우는 완전 이진트리이므로 왼쪽자식임) 자신보다 우선순위가 더 높으면 왼쪽자식과 스왑작업을 한다.
        /// 3-3. 자식이 없는 경우, 강등작업을 종료한다..
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public TElement Dequeue()
        {
            // 노드가 업으면 뺄것이 없으므로 예외처리
            if (nodes.Count == 0)
                throw new InvalidOperationException();

            // 우선순위가 제일 높거나 낮은 반환하기 위한 최상단의 노드의 요소
            TElement element = nodes[0].Key;

            // 0 번째 노드를 마지막 노드로 변경, 마지막 노드 제거
            nodes[0] = nodes[nodes.Count - 1];
            nodes.RemoveAt(nodes.Count - 1);

            // 0 번위치부터 강등(탐색)작업 시작
            int searchIndex = 0;

            // 마지막 인덱스까지 도착하면 탐색 종료
            while (searchIndex < nodes.Count - 1)
            {
                // case 3
                int leftChildIndex = GetLeftChildIndex(searchIndex);
                int rightChildIndex = GetRightChildIndex(searchIndex);

                // LeftChild & RightChild Exist
                if (rightChildIndex < nodes.Count)
                {
                    // 왼쪽 오른쪽 자식 중 더 높은 우선순위를 가진 인덱스를 구한다.
                    int lessChildIndex = (comparer.Compare(nodes[leftChildIndex].Value, nodes[rightChildIndex].Value) < 0)
                        ? leftChildIndex : rightChildIndex;

                    // 자신(현재위치의 노드)의 우선순위보다 위에서 구한 자식의 우선순위가 높으면 스왑후 인덱스 변경.
                    if (comparer.Compare(nodes[searchIndex].Value, nodes[lessChildIndex].Value) > 0)
                    {
                        KeyValuePair<TElement, TPriority> temp = nodes[searchIndex];
                        nodes[searchIndex] = nodes[lessChildIndex]; nodes[lessChildIndex] = temp;
                        searchIndex = lessChildIndex;
                    }
                    else
                        // 자신의 우선순위가 두 자식 우선순위보다 더 높으면 그 위치에 놓고 작업 종료
                        break;
                }
                // LeftChild Exist
                else if (leftChildIndex < nodes.Count)
                {
                    // 자신(현재위치의 노드)의 우선순위보다 왼쪽 자식의 우선순위가 높으면 스왑후 인덱스 변경.
                    if (comparer.Compare(nodes[searchIndex].Value, nodes[leftChildIndex].Value) > 0)
                    {
                        KeyValuePair<TElement, TPriority> temp = nodes[searchIndex];
                        nodes[searchIndex] = nodes[leftChildIndex]; nodes[leftChildIndex] = temp;
                        searchIndex = leftChildIndex;
                    }
                    else
                        // 자신의 우선순위가 왼쪽 자식보다 더 높으면 그 위치에 놓고 작업 종료
                        break;
                }
                // Not Exist Child
                // 자식이 없을 때 그 위치에 놓고 작업 종료
                else
                    break;
            }

            return element;
        }

        /// <summary>
        /// Dequeue를 시도하기 전에 요소가 0(비어있다면) false를 반환하고, 요소값과 우선순위 값을 default로 출력(반환)시킨다.
        /// 만약 비어있지 않으면 Queue에 맨위 즉, 우선순위가 제일 높거나 낮은 최상단의 노드의 요소와 우선순위를 출력(반환)시키고
        /// 정상적으로 Dequeue를 실행하고 True를 반환한다.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Queue에 맨위 즉, 우선순위가 제일 높거나 낮은 최상단의 노드의 요소를 반환한다.
        /// </summary>
        /// <returns></returns>
        public TElement Peek()
        {
            return nodes[0].Key;
        }

        /// <summary>
        /// Peek을 시도하기 전에 요소가 0(비어있다면) false를 반환하고, 요소값과 우선순위 값을 default로 출력(반환)시킨다.
        /// 만약 비어있지 않으면 Queue에 맨위 즉, 우선순위가 제일 높거나 낮은 최상단의 노드의 요소와 우선순위를 출력(반환)시키고
        /// True를 반환한다.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 요소와 우선순위를 입력받아 Queue에 삽입하고 바로
        /// 제일 우선순위가 높거나 낮은 노드의 요소를 출력하는 메서드이다.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public TElement EnqueueDequeue(TElement element, TPriority priority)
        {
            Enqueue(element, priority);
            return Dequeue();
        }

        /// <summary>
        /// Queue를 초기화 시켜준다. 원래 상태로 복구해준다.
        /// </summary>
        public void Clear()
        {
            this.nodes = new List<KeyValuePair<TElement, TPriority>>();
            this.comparer = Comparer<TPriority>.Default;
        }

        /// <summary>
        /// 배열(List)의 크기를 매개변수로 받은 인수로 확장하기 위한 메서드
        /// 예를 들어, EnsureCapacity(100) 메서드를 호출하면, 내부 배열의 크기를 100 이상으로 늘린다.
        /// </summary>
        /// <param name="capacity"></param>
        public void EnsureCapacity(int capacity)
        {
            if (capacity < nodes.Count)
                throw new InvalidOperationException();

            List<KeyValuePair<TElement, TPriority>> newNode = new List<KeyValuePair<TElement, TPriority>>(capacity);
            newNode.AddRange(this.nodes);
            this.nodes = newNode;
        }

        /// <summary>
        /// 요소보다 배열(List)의 크기가 클 때 배열의 크기를 요소만큼으로 정리해준다.
        /// </summary>
        public void TrimExcess()
        {
            List<KeyValuePair<TElement, TPriority>> newNode = new List<KeyValuePair<TElement, TPriority>>(Count);
            newNode.AddRange(this.nodes);
            this.nodes = newNode;
        }

        /// <summary>
        /// 부모노드의 인덱스를 구하는 메서드이다 
        /// 자식노드 인덱스에서 1을 빼고 2로나누면 부모노드인덱스이다
        /// </summary>
        /// <param name="childIndex"></param>
        /// <returns></returns>
        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        /// <summary>
        /// 자식노드의 왼쪽 인덱스를 구하는 메서드이다 
        /// 부모노드의 인덱스에서 2를 곱하고 1을 더하면 자식의 왼쪽 노드의 인덱스이다.
        /// </summary>
        /// <param name="parentIndex"></param>
        /// <returns></returns>
        private int GetLeftChildIndex(int parentIndex)
        {
            return (parentIndex * 2) + 1;
        }

        /// <summary>
        /// 자식노드의 오른쪽 인덱스를 구하는 메서드이다 
        /// 부모노드의 인덱스에서 2를 곱하고 2을 더하면 자식의 오른쪽 노드의 인덱스이다.
        /// </summary>
        /// <param name="parentIndex"></param>
        private int GetRightChildIndex(int parentIndex)
        {
            return (parentIndex * 2) + 2;
        }

        // Interface Method Overriding //

        public IEnumerator<(TElement Element, TPriority Priority)> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
