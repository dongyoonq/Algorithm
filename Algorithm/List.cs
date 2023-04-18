using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Algorithm
{
    internal class MyList<T> : IEnumerable<T>, IEnumerator
    {
        private const int DefaultCapacity = 0;

        private T[] items;
        private int size;
        private int position = -1;

        public int Count
        {
            get { return size; }
        }

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

        //public T Current {  get { return items[position]; } }

        public object Current { get { return items[position]; } }

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

        public T this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

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

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");

            for (int i = index; i < size - 1; i++)
                items[i] = items[i + 1];
            items[size-- - 1] = default(T);
        }

        public void Clear()
        {
            items = new T[Count];
            size = 0;
        }


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

        public int IndexOf(int item)
        {
            return FindIndex(x => x.Equals(item));
        }

        public T[] ToArray() { return items; }

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

        public void AddRange(IEnumerable<T> collections)
        {
            int newCapacity = items.Length;
            if (collections == null)
                throw new ArgumentNullException();
            foreach(var it in collections)
            {
                Add(it);
                newCapacity++;
            }

            T[] newItems = new T[newCapacity];
            Array.Copy(items, newItems, newCapacity);
            items = newItems;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public bool MoveNext()
        {
            position++;
            return (position < items.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
