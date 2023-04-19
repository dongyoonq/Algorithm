using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

namespace Algorithm
{
    internal class MyList<T> : IEnumerator<T>, IEnumerable<T>
    {
        private const int DefaultCapacity = 0;

        private T[] items;
        private int size;
        private int position = -1;

        /// <summary>
        /// LIST 요소의 개수
        /// </summary>
        public int Count
        {
            get { return size; }
        }

        /// <summary>
        /// LIST 의 최대 수용량
        /// </summary>
        public int Capacity
        {
            get { return items.Length; }
            set 
            {
                if (value < size)
                    throw new ArgumentOutOfRangeException();
                T[] newItems = new T[value];
                Array.Copy(items, newItems, items.Length);
                items = newItems;
            }
        }

        /// <summary>
        /// LIST 초기화하는 기본 생성자 및 오버로딩
        /// </summary>
        public MyList()
        {
            this.items = new T[DefaultCapacity];
            this.size = 0;
        }

        public MyList(int capacity)
        {
            this.items = new T[capacity];
            this.size = 0;
        }

        public MyList(IEnumerable<T> collections)
        {
            this.items = new T[DefaultCapacity];
            this.size = 0;
            AddRange(collections);
        }

        /// <summary>
        /// 인덱스로 LIST를 접근할 수 있게 하는 인덱스 프로퍼티
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();
                return items[index]; 
            }
            set {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();
                items[index] = value; 
            }
        }

        /// <summary>
        /// LIST의 마지막 요소 다음 인덱스에 데이터를 삽입하는 메서드
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (size < items.Length)
                items[size++] = item;
            else
            {
                Grow();
                items[size++] = item;
            }
        }

        /// <summary>
        /// LIST의 마지막 요소 다음 인덱스에 Enumerable한 데이터를 삽입하는 메서드
        /// </summary>
        /// <param name="collections"></param>
        public void AddRange(IEnumerable<T> collections)
        {
            int newCapacity = items.Length;
            if (collections == null)
                throw new ArgumentNullException();
            foreach (var it in collections)
            {
                Add(it);
                newCapacity++;
            }

            T[] newItems = new T[newCapacity];
            Array.Copy(items, newItems, newCapacity);
            items = newItems;
        }

        /// <summary>
        /// LIST의 특정 인덱스에 데이터를 삽입하는 메서드
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            if (size >= items.Length)
                Grow();

            CopyTo(index, items, index + 1, ++size - index - 1);
            //Array.Copy(items, index, items, index + 1, size - index - 1);
            items[index] = item;
        }

        /// <summary>
        /// LIST의 특정 인덱스에 Enumerable한 데이터를 삽입하는 메서드
        /// </summary>
        /// <param name="index"></param>
        /// <param name="collections"></param>
        public void InsertRange(int index, IEnumerable<T> collections)
        {
            if (collections == null)
                throw new ArgumentNullException();

            foreach (var it in collections)
                Insert(index++, it);
        }

        /// <summary>
        /// LIST의 크기를 늘리는 알고리즘
        /// </summary>
        private void Grow()
        {
            if (items.Length == 0)
            {
                items = new T[Marshal.SizeOf<T>()];
                return;
            }

            int newCapacity = items.Length * 2;
            T[] newItems = new T[newCapacity];
            Array.Copy(items, newItems, items.Length);
            items = newItems;
        }

        /// <summary>
        /// 지정한 데이터를 LIST에서 첫번째로 나타나는 요소를 삭제하는 메서드
        /// </summary>
        /// <param name="item">삭제할 요소</param>
        /// <returns></returns>
        public bool? Remove(T item)
        {
            for (int i = 0; i < size; i++)
            {
                if (items[i].Equals(item))
                {
                    RemoveAt(FindIndex(x => x.Equals(item)));
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 지정한 데이터를 LIST에서 전부 삭제하는 메서드
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int RemoveAll(Predicate<T> match)
        {
            int deleteCount = 0;
            for (int i = 0; i < items.Length; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (match(items[j]))
                    {
                        Remove(items[j]);
                        deleteCount++;
                        break;
                    }
                }
            }

            return deleteCount;
        }

        /// <summary>
        /// 리스트의 특정 위치의 요소를 삭제하는 메서드
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");

            for (int i = index; i < size - 1; i++)
                items[i] = items[i + 1];
            items[size-- - 1] = default(T);
        }

        /// <summary>
        /// 인덱스위치 부터 횟수만큼 요소를 삭제하는 메서드
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void RemoveRange(int index, int count)
        {
            if (index < 0 || count < 0 || index + count > size)
                throw new ArgumentOutOfRangeException();

            int tmp = index + count;
            for (; index < size - count; index++)
            {
                items[index] = items[index + count];
            }
            
            for(int i = 0; i < count; i++)
                items[size-- - 1] = default(T);
        }

        /// <summary>
        /// LIST 요소를 모두 제거하는 메서드
        /// </summary>
        public void Clear()
        {
            items = new T[Count];
            size = 0;
        }

        /// <summary>
        /// LIST에서 Predicate 델리게이트로 설정한 조건과 일치하는 첫번째 요소를 반환하는 메서드
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return items[i];
            }
            return default(T);
        }

        /// <summary>
        /// LIST에서 Predicate 델리게이트로 설정한 조건과 일치하는 마지막 요소를 반환하는 메서드
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public T FindLast(Predicate<T> match)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (match(items[i]))
                    return items[i];
            }
            return default(T);
        }

        /// <summary>
        /// LIST에서 Predicate 델리게이트로 설정한 조건과 일치하는 모든 요소를 반환하는 메서드
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public MyList<T> FindAll(Predicate<T> match)
        {
            MyList<T> newlist = new MyList<T>();
            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    newlist.Add(items[i]);
            }
            return newlist;
        }

        /// <summary>
        /// LIST에서 Predicate 델리게이트로 설정한 조건과 일치하는 첫번째 요소의 인덱스를 반환하는 메서드, 오버로딩
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return i;
            }

            return -1;
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            if (startIndex < 0 || startIndex > size)
                throw new ArgumentOutOfRangeException();

            for (; startIndex < size; startIndex++)
            {
                if (match(items[startIndex]))
                    return startIndex;
            }

            return -1;
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            if (startIndex < 0 || count < 0 || startIndex + count > size)
                throw new ArgumentOutOfRangeException();

            int tmp = startIndex + count;
            for(; startIndex < tmp; startIndex++)
            {
                if(match(items[startIndex]))
                    return startIndex;
            }

            return -1;
        }

        /// <summary>
        /// LIST에서 Predicate 델리게이트로 설정한 조건과 일치하는 마지막 요소의 인덱스를 반환하는 메서드, 오버로딩
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int FindLastIndex(Predicate<T> match)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (match(items[i]))
                    return i;
            }

            return -1;
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            if (startIndex < 0 || startIndex > size)
                throw new ArgumentOutOfRangeException();

            for (int i = startIndex; i >= 0; i--)
            {
                if (match(items[i]))
                    return i;
            }

            return -1;
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            if (startIndex < 0 || count < 0 || startIndex - count + 1 < 0)
                throw new ArgumentOutOfRangeException();

            int tmp = startIndex - count + 1;
            for (int i = startIndex; i >= tmp; i--)
            {
                if (match(items[i]))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// 지정한 데이터가 LIST에서 일치하는 첫번째 요소의 인덱스를 반환하는 메서드, 오버로딩
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(int item)
        {
            return FindIndex(x => x.Equals(item));
        }

        public int IndexOf(int item, int index)
        {
            return FindIndex(index, x => x.Equals(item));
        }

        public int IndexOf(int item, int index, int count)
        {
            return FindIndex(index, count, x => x.Equals(item));
        }

        /// <summary>
        /// 지정한 데이터가 LIST에서 일치하는 마지막 요소의 인덱스를 반환하는 메서드, 오버로딩
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LastIndexOf(int item)
        {
            return FindLastIndex(x => x.Equals(item));
        }

        public int LastIndexOf(int item, int index)
        {
            return FindLastIndex(index, x => x.Equals(item));
        }

        public int LastIndexOf(int item, int index, int count)
        {
            return FindLastIndex(index, count, x => x.Equals(item));
        }

        /// <summary>
        /// LIST에서 지정한 데이터가 있는지 반환하는 메서드
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < size; i++)
            {
                if (items[i].Equals(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Predicate 델리게이트로 지정한 조건으로 데이터가 있는지 반환하는 메서드
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool Exists(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// LIST의 요소들을 복사하는 메서드, 오버로딩
        /// </summary>
        /// <param name="array"></param>
        public void CopyTo(T[] array)
        {
            Array.Copy(items, array, size);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(items, 0, array, arrayIndex, size);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            Array.Copy(items, index, array, arrayIndex, count);
        }

        /// <summary>
        /// LIST를 다른 타입으로 변환시키고 반환하는 메서드
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="converter"></param>
        /// <returns></returns>
        public MyList<TOutput> ConvertAll<TOutput>(Converter<T,TOutput> converter)
        {
            MyList<TOutput> newlist = new MyList<TOutput>();
            newlist.items = new TOutput[this.Count];
            newlist.size = this.size;

            for (int i = 0; i < size; i++)
            {
                newlist.items[i] = converter(items[i]);
            }

            return newlist;
        }
        
        /// <summary>
        /// LIST에서 인덱스위치로 부터 특정 개수만큼 요소를 추출해 새로운 LIST를 만들어주는 메서드
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MyList<T> GetRange(int index, int count)
        {
            if (index < 0 || count < 0 || index + count > size)
                throw new ArgumentOutOfRangeException();

            MyList<T> newlist = new MyList<T>();
            for(int i = index; i < index + count; i++)
            {
                newlist.Add(items[i]);
            }
            newlist.TrimExcess();
            return newlist;
        }

        /// <summary>
        /// LIST 요소를 정렬시키는 메서드, 오버로딩
        /// </summary>
        public void Sort()
        {
            Array.Sort(items);
        }

        public void Sort(Comparison<T> comparison)
        {
            Array.Sort(items, comparison);
        }

        public void Sort(IComparer<T>? comparer)
        {
            Array.Sort(items, comparer);
        }

        public void Sort(int index, int count, IComparer<T>? comparer)
        {
            Array.Sort(items, index, count, comparer);
        }

        /// <summary>
        /// LIST 요소를 뒤집어주는 메서드, 오버로딩
        /// </summary>
        public void Reverse()
        {
            Array.Reverse(items);
        }

        public void Reverse(int index, int count)
        {
            Array.Reverse(items, index, count);
        }

        /// <summary>
        /// Action 델리게이트에 들어온 메서드를 각 요소마다 수행시켜주는 메서드
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(Action<T> action)
        {
            for(int i = 0; i < size; i++)
                action?.Invoke(items[i]);
        }

        /// <summary>
        /// LIST에 있는 용량이 요소보다 클때 용량을 요소에 맞춰주는 메서드
        /// </summary>
        public void TrimExcess()
        {
            T[] newItem = new T[size];
            CopyTo(0, newItem, 0, size);
            items = newItem;
        }

        /// <summary>
        /// Predicate 델리게이트를 매개변수로 지정자의 조건에 맞는 요소가 모두 일치하는지 여부 반환
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool TrueForAll(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException();

            foreach (T item in items)
                if (!match(item))
                    return false;

            return true;
        }

        public T[] ToArray() { return items; }

        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// Enumerator Interface 구현
        /// https://nomad-programmer.tistory.com/188
        /// /////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// IEnumerator 인터페이스를 상속받아 메서드 및 프로퍼티를 구현
        /// </summary>
        public T Current { get { return items[position]; } }

        object IEnumerator.Current { get { return items[position]; } }

        public bool MoveNext()
        {
            if(position == (size - 1))
            {
                Reset();
                return false;
            }

            position++;
            return (position < size);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// LIST를 foreach 구문을 사용하기 위해 GetEnumerator를 IEnumerable 인터페이스를 상속받아 구현
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < size; i++)
                yield return items[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < size; i++)
                yield return items[i];
        }
    }
}
