using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class MyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 딕셔너리 테이블에 저장할 정보를 구조체로 만든다.
        /// 정보는 현재 Entry의 상태정보인 비어있는 상태 None, 사용중인 상태 Using, 제거된 상태 Delected가 있다.
        /// 해싱함수로 얻은 인덱스정보를 가지게한다.
        /// Entry를 찾을 Key정보와 값 Value를 가지게한다.
        /// </summary>
        private struct Entry
        {
            public enum State { None, Using, Delected }

            public State state;
            public int hashCode;
            public TKey key;
            public TValue value;
        }

        /// <summary>
        /// Entry를 1000개 가지는 테이블을 만들어준다.
        /// </summary>
        private const int DefaultCapacity = 1000;
        private Entry[] table;

        public MyDictionary()
        {
            table = new Entry[DefaultCapacity];
        }

        /// <summary>
        /// Value를 Key값으로 접근하기 위한 인덱스를 만들어준다.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public TValue this[TKey key]
        {
            get
            {
                // 1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                // 2. key가 일치하는 데이터가 나올때까지 다음으로 이동
                while (table[index].state == Entry.State.Using)
                {
                    // 3. 동일한 키값을 찾았을때 Value 반환
                    if (key.Equals(table[index].key))
                        return table[index].value;

                    // table에 없으면
                    if (table[index].state != Entry.State.Using)
                        break;

                    index = index < table.Length - 1 ? index + 1 : 0;
                }

                throw new InvalidOperationException();
            }

            set
            {
                // 1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                // 2. key가 일치하는 데이터가 나올때까지 다음으로 이동
                while (table[index].state == Entry.State.Using)
                {
                    // 3. 동일한 키값을 찾았을때 덮어쓰기
                    if (key.Equals(table[index].key))
                    {
                        table[index].value = value;
                        return;
                    }

                    // table에 없으면
                    if (table[index].state != Entry.State.Using)
                        break;

                    index = index < table.Length - 1 ? index + 1 : 0;
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            // 1. key를 index로 해싱
            int index = Math.Abs(key.GetHashCode() % table.Length);

            // 2. 사용중이 아닌 index 까지 다음으로 이동
            while(table[index].state == Entry.State.Using)
            {
                if(key.Equals(table[index].key))
                    throw new InvalidOperationException();
                else
                    index = index < table.Length - 1 ? index + 1 : 0;
            }

            // 3. 사용중이 아닌 index를 발견한 경우 그 위치에 저장
            table[index].hashCode = key.GetHashCode();
            table[index].key = key;
            table[index].value = value;
            table[index].state = Entry.State.Using;
        }

        public void Remove(TKey key)
        {
            // 1. key를 index로 해싱
            int index = Math.Abs(key.GetHashCode() % table.Length);

            // 2. Key값과 동일한 데이터를 찾았을때까지 index 증가
            while (table[index].state == Entry.State.Using)
            {
                if (key.Equals(table[index].key))
                {
                    table[index].state = Entry.State.Delected;
                }

                if (table[index].state == Entry.State.None)
                    break;

                index = index < table.Length - 1 ? index + 1 : 0;
            }

            throw new InvalidOperationException();
        }
    }
}
