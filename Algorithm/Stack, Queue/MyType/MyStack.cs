using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class MyStack<T> : IEnumerable<T>, IEnumerable
    {
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private MyStack<T> _stack;
            public T Current => throw new NotImplementedException();

            object IEnumerator.Current => throw new NotImplementedException();

            public Enumerator(MyStack<T> stack)
            {
                _stack = stack;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        private T[] array;
        private int size;

        public int Count { get { return size; } }

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

        public T Pop()
        {
            if(array == null)
                throw new InvalidOperationException();

            return array[size-- - 1];
        }

        public T Peek()
        {
            if (array == null)
                throw new InvalidOperationException();

            return array[size - 1];
        }

        private void Grow()
        {

            T[] newArray = new T[size * 2];
            Array.Copy(array, newArray, size);
            array = newArray;
        }

        public void TrimExcess()
        {
            T[] newArray = new T[size];
            Array.Copy(array, newArray, size);
            array = newArray;
        }

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
