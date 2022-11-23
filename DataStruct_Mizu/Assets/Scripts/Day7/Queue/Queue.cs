using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class Queue<T> : IEnumerable<T>
    {
        // 큐는 선입선출구조
        // 내부는 array, round queue 형식으로 구현하기
        // 사용할 util : Enqueue, Dequeue, Peek, Count
        T[] arr;
        const int _defaultCapacity = 2;
        private int _capacity = _defaultCapacity;

        internal int _front = 0;
        internal int _rear = -1;

        public int Count { get; private set; } = 0;

        public Queue()
        {
            arr = new T[_defaultCapacity];
        }
        public Queue(int capacity)
        {
            _capacity = capacity;
            arr = new T[_capacity];
        }

        public bool Contains(T value)
        {
            if (Count <= 0) // 0이면 False를 반환하는 것이 적합하다.
                return false;

            for (int i = 0; i < Count; i++)
                if (EqualityComparer<T>.Default.Equals(value, arr[i]))
                    return true;

            return false;
		}

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            //if (Count <= 0) // 0이면 반환할 필요가 없다.
            //    throw new Exception("Queue에 데이터가 없습니다.");

            for (int i=0;i<Count;i++)
                yield return arr[(i + _front) % _capacity];
            
		}

        public void Enqueue(T value)
        {
            // ref타입의 default는 막을 수 있으나 value타입의 0이 막혀버리는 문제가 발생.
            //if (EqualityComparer<T>.Default.Equals(value, default(T)))
            //    throw new Exception("null값은 추가할 수 없습니다.");
            // 고민의 흔적..
            //if(!value.GetType().IsValueType)
            //    if (EqualityComparer<T>.Default.Equals(value, default(T)))
            //        throw new Exception("null값은 추가할 수 없습니다.");
            // Null은 value이고, 비어있는 포인터. 변수는 값이다.
            // 확고한 이유가 있어서 null이 들어오면 안되는 자료구조가 아닌 한 Enqueue에서 null을 신경 쓸 필요는 없다.

            if (Count == _capacity)
                Expand();

            ++_rear;
            arr[_rear] = value;
            if (_rear >= _capacity)
                _rear %= _capacity;

            ++Count;
        }
        public T Dequeue()
        {
            if (Count == 0)
                throw new Exception("Queue에 데이터가 없습니다.");

            T value = arr[_front];
            arr[_front] = default(T);
            
            ++_front;
            if (_front >= _capacity)
                _front %= _capacity;
            --Count;
            return value;
        }
        public T Peek()
        {
            if (Count == 0)
                throw new Exception("Queue에 데이터가 없습니다.");

            return arr[_front];
        }
        public void Clear()
        {
            for (int i = 0; i < Count; ++i)
                arr[i] = default(T);

            _front = 0;
            _rear = -1;
            Count = 0;
        }
        internal void Expand()
        {
            T[] temp = new T[_capacity << 1];

            for(int i = 0; i < Count; ++i)
            {
                int index = (i + _front) % _capacity;
                temp[i] = arr[index];
                arr[index] = default(T);
            }

            _capacity <<= 1;
            _front = 0;
            _rear = Count - 1;
            arr = temp;
        }
    }
}