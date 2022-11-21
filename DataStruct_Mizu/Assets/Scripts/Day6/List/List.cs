using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mizu
{
    public class List : StudyBase
    {
        protected override void OnLog()
        {
            var aList = new List<int>();

            aList.Add(2);
            // 2
            aList.LogValues();

            aList.Insert(0, 1);
            // 1, 2
            aList.LogValues();

            aList.Add(4);
            aList.Insert(aList.Count - 1, 3);
            // 1, 2, 3, 4
            aList.LogValues();

            aList.Remove(2);
            aList.RemoveAt(0);
            // 4
            Log(aList[aList.Count - 1]);
        }
    }
    public sealed class List<T> : IEnumerable<T>
    {
        private const int _defaultCapacity = 2;
        private int _capacity = _defaultCapacity;
        private int _size = 0;
        public int Count { private set => _size = value; get => _size; }

        T[] arr = new T[_defaultCapacity];
        public T this[int index]
        {
            set => arr[index] = value;
            get => arr[index];
        }

        public bool Contains(T value)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (EqualityComparer<T>.Default.Equals(this[i], value))
                    return true;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; ++i)
                yield return arr[i];
        }

        public void Add(T value)
        {
            if (Count == _capacity)
                Expand();

            arr[Count] = value;
            ++Count;
        }
        public void Insert(int index, T value)
        {
            if (Count == _capacity)
                Expand();

            for (int i = Count; index < i; i--)
                arr[i] = arr[i - 1];
            arr[index] = value;

            ++Count;
        }

        public bool Remove(T value)
        {
            for (int i = 0; i < _size; ++i)
            {
                if (EqualityComparer<T>.Default.Equals(this[i], value))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; ++i)
                arr[i] = arr[i + 1];
            arr[Count - 1] = default;

            --Count;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
                arr[i] = default;

            Count = 0;
        }
        private void Expand()
        {
            _capacity <<= 1;
            T[] temp = new T[_capacity];

            for(int i = 0; i < Count; i++)
            {
                temp[i] = arr[i];
            }

            arr = temp;
        }
    }
}