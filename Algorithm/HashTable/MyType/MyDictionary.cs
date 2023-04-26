using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class MyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        // Struct //

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
        // Key, Value를 가진 entry배열 테이블을 만들어준다.
        private Entry[] table;

        public MyDictionary()
        {
            table = new Entry[DefaultCapacity];
        }

        /// <summary>
        /// Value를 Key값으로 접근하고 수정하기 위한 인덱스를 만들어준다.
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

                // 2. 탐색
                while (true)
                {
                    // 2-1. 빈곳이 나타나면 탐색 종료
                    if (table[index].state == Entry.State.None)
                    {
                        break;
                    }
                    // 2-2. 사용중일때 동일한 키값을 찾았을때 Value 반환
                    else if (table[index].state == Entry.State.Using)
                    {
                        if (key.Equals(table[index].key))
                        {
                            return table[index].value;
                        }
                    }    
                    // 2-3. 지워진곳이나, 동일한 키값을 찾지 못했으면 계속 탐색
                    else
                        index = (index + 1) % table.Length;
                }

                throw new InvalidOperationException();
            }

            set
            {
                // 1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                // 2. key가 일치하는 데이터가 나올때까지 다음으로 이동
                while (true)
                {
                    // 2-1. 아무것도 없었다면
                    if (table[index].state == Entry.State.None)
                    {
                        break;
                    }
                    // 2-2. 지워진 곳이라면 다음 인덱스로 이동한다.
                    else if (table[index].state == Entry.State.Delected)
                    {
                        index = (index + 1) % table.Length;
                    }
                    // 2-3. 사용중인 곳이라면 같은키인지 확인해 값 설정
                    // 같은키가 아니라면 다음으로 이동
                    else if (table[index].state == Entry.State.Using)
                    {
                        if (key.Equals(table[index].key))
                        {
                            table[index].value = value;
                            table[index].key = key;
                            return;
                        }
                    }
                    // 2-3. 다음인덱스로 넘긴다.
                    else
                        index = (index + 1) % table.Length;
                }

                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// 해시테이블에 key, value로 들어온 요소를 추가한다.
        /// key는 해시함수를 통해 인덱스로 만들고 그 인덱스에 value를 저장한다
        /// 만약 동일한 인덱스의 다른키가 들어오게된다면 개방주소법을 이용해
        /// 비어있는 또는 삭제된 인덱스를 찾아 그 인덱스에 값을 삽입해준다.
        /// 만약 동일한 인덱스에 동일한 키값이 들어오게된다면 예외처리를 해준다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Add(TKey key, TValue value)
        {
            // 1. key를 index로 해싱
            int index = Math.Abs(key.GetHashCode() % table.Length);

            // 2. 빈공간or지워진곳을 찾는다.
            while (true)
            {
                // 2-1. 빈공간or지워진곳을 찾으면 탈출
                if (table[index].state == Entry.State.None || table[index].state == Entry.State.Delected)
                {
                    break;
                }
                // 2-2. 그 공간이 사용중인 공간일때, 같은키면 오류
                else if (table[index].state == Entry.State.Using)
                {
                    if (key.Equals(table[index].key))
                    {
                        throw new InvalidOperationException();
                    }
                }
                // 2-3. 다음인덱스로 넘긴다.
                else
                    index = (index + 1) % table.Length;
            }

            // 3. 사용중이 아닌 index를 발견한 경우 그 위치에 저장
            table[index].hashCode = key.GetHashCode();
            table[index].key = key;
            table[index].value = value;
            table[index].state = Entry.State.Using;
        }

        /// <summary>
        /// 해시테이블에 key, value로 들어온 요소를 추가한다.
        /// key는 해시함수를 통해 인덱스로 만들고 그 인덱스에 value를 저장한다
        /// 만약 동일한 인덱스에 다른 키값이 들어오게된다면 개방주소법을 이용해
        /// 비어있는 또는 삭제된 인덱스를 찾아 그 인덱스에 값을 성공적으로 삽입하고 True를 반환한다.
        /// 만약 동일한 인덱스에 동일한 키값이 들어오게된다면 false를 반환하고 Add하지 않는다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public bool TryAdd(TKey key, TValue value)
        {
            // 1. key를 index로 해싱해 인덱스를 찾는다.
            int index = Math.Abs(key.GetHashCode() % table.Length);

            // 2. 빈공간or지워진곳을 찾는다.
            while (true)
            {
                // 2-1. 빈공간or지워진곳을 찾으면 탈출
                if (table[index].state == Entry.State.None || table[index].state == Entry.State.Delected)
                {
                    break;
                }
                // 2-2. 그 공간이 사용중인 공간일때, 같은키면 오류
                else if (table[index].state == Entry.State.Using)
                {
                    if (key.Equals(table[index].key))
                    {
                        return false;
                    }
                }
                // 2-3. 다음인덱스로 넘긴다.
                else
                    index = (index + 1) % table.Length;
            }

            // 3. 사용중이 아닌 index를 발견한 경우 그 위치에 저장
            table[index].hashCode = key.GetHashCode();
            table[index].key = key;
            table[index].value = value;
            table[index].state = Entry.State.Using;
            return true;
        }

        /// <summary>
        /// 해시테이블에 key를 지워준다. 그 인덱스를 지운다.(Deleted 상태로 만든다)
        /// key는 해시함수를 통해 인덱스로 만들고 그 인덱스를 탐색한다.
        /// 만약 동일한 인덱스가 들어오게된다면 그 인덱스의 상태를 Deleted로 만들어준다.
        /// 찾지못하면 예외를 처리해준다.
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public bool Remove(TKey key)
        {
            int index = FindIndex(key);

            if (index < 0)
            {
                throw new InvalidOperationException();
            }
            else
            {
                table[index].state = Entry.State.Delected;
                return true;
            }
        }

        /// <summary>
        /// Key의 Index를 Table에서 있는지 없는지 찾아 그 True/False와 
        /// 찾았으면 true와 false를 반환한다. 반환값은 True아니면 False뿐이다.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out TValue value);
        }

        /// <summary>
        /// Key의 Index를 Table에서 있는지 없는지 찾아 그 True/False와 
        /// 찾았으면 true와 그 value에 Index의 Value를 반환하는 메서드
        /// 못찾았을때는 value에 TValue의 default값과 false를 반환한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = FindIndex(key);

            if (index < 0)
            {
                value = default(TValue);
                return false;
            }
            else
            {
                value = table[index].value;
                return true;
            }
        }

        /// <summary>
        /// 테이블을 비운다.
        /// </summary>
        public void Clear()
        {
            table = new Entry[DefaultCapacity];
        }

        /// <summary>
        /// Key의 Index를 Table에서 있는지 없는지 찾아주는 메서드
        /// 못찾았을때는 -1을 반환한다. 찾았을때는 Key에대한 Index를 반환한다.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int FindIndex(TKey key)
        {
            int index = Math.Abs(key.GetHashCode() % table.Length);

            while (true)
            {
                // 2-1. 빈곳이 나타나면 탐색 종료
                if (table[index].state == Entry.State.None)
                    break;
                // 2-2. 사용중일때 동일한 키값을 찾았을때 index를 반환
                else if (table[index].state == Entry.State.Using)
                {
                    if (key.Equals(table[index].key))
                    {
                        return index;
                    }
                }
                // 2-3. 지워진곳이나, 동일한 키값을 찾지 못했으면 계속 탐색
                index = (index + 1) % table.Length;
            }

            return -1;
        }
    }
}
