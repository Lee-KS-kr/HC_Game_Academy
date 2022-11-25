using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
	// 모든게 잘못된 구현입니다. 집에 가서 고쳐보겠습니다...TT
	public class Dictionary<K, T> : IEnumerable<KeyValuePair<K, T>>
	{
		// buckets, entries 구현이 필요합니다.

		const int _defaultCapacity = 4;
		int _capacity = _defaultCapacity;
		public int Count { private set; get; } = 0;

		Entry[] entries;
		public Dictionary()
		{
			entries = new Entry[_defaultCapacity];
		}
		public Dictionary(int capacity)
        {
			if (capacity < 1)
				throw new Exception("Capacity의 값이 1 이하입니다.");

			_capacity = capacity;
			entries = new Entry[_capacity];
        }
		// 리사이즈시 별도 처리...
		public T this[K key]
		{
			set
			{
				// 만약 있으면 추가처리를 할 수 없음.
				int hash = key.GetHashCode();
				int index = GetIndex(hash);
				entries[index].key = key;
				entries[index].value = value;
			}
			get
			{
				if (Count < 1) throw new Exception("Dictionary에 데이터가 없습니다.");

				int hash = key.GetHashCode();
				int index = GetIndex(hash);
				return entries[index].value;
			}
		}

        public bool ContainsKey(K key)
		{
			if (Count < 1) return false;

			foreach (var k in entries)
			{
				if (EqualityComparer<K>.Default.Equals(key, k.key))
					return true;
			}

			return false;
		}
		public bool ContainsValue(T value)
		{
			if (Count < 1) return false;

			foreach(var e in entries)
            {
				if (EqualityComparer<T>.Default.Equals(value, e.value))
					return true;
            }

			return false;
		}

		// 검색을 한번만 하기 위해서 하는 것.
		public bool TryGetValue(K key, out T result)
        {
            //if (!ContainsKey(key))
            //{
            //    result = default(T);
            //    return false;
            //}

            int hash = key.GetHashCode();
            int index = hash % _capacity;

            if (EqualityComparer<K>.Default.Equals(key, entries[index].key))
            {
                result = entries[index].value;
                return true;
            }

			result = default(T);
			return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
		{
			foreach(var e in entries)
            {
				if (null != e.value)
					yield return new KeyValuePair<K, T>(e.key, e.value);
            }
		}


		public bool Add(K key, T value)
		{
			if (Count == _capacity)
				Expand();

			if (ContainsKey(key)) return false;

			int hash = key.GetHashCode();
			int index = GetIndex(hash);
			if (!EqualityComparer<K>.Default.Equals(entries[index].key, default(K)))
			{
				int next = entries[index].next;
				if(entries[next].isNextChanged)
                {
                    while (!entries[next].isNextChanged)
                    {
						next = entries[next].next;
                    }
                }

				for(int i = index; i < _capacity + index; ++i)
                {
					if (EqualityComparer<K>.Default.Equals(entries[i%_capacity].key, default(K)))
                    {
						index = i % _capacity;
						entries[next].next = index;
						entries[next].isNextChanged = true;
                    }
				}
			}

			entries[index].hashCode = hash;
			entries[index].key = key;
			entries[index].value = value;
			Count++;
			return true;
		}


        public bool Remove(K key)
        {
			if (!ContainsKey(key)) return false;

            int hash = key.GetHashCode();
			int index = GetIndex(hash);
			if (EqualityComparer<K>.Default.Equals(entries[index].key, key))
			{ 
				if (entries[index].isNextChanged)
				{
					while (!entries[index].isNextChanged)
					{
						index = entries[index].next;
					}
				}

				entries[index].key = default;
				entries[index].value = default;
				entries[index].hashCode = default;
				// changed를 바꾸는걸 깜빡했음
				Count--;

				return true;
			}

			return false;
        }

        public void Clear()
		{
			Array.Resize(ref entries, 0);
			_capacity = _defaultCapacity;
			Count = 0;

			entries = new Entry[_defaultCapacity];
		}

		void SetEntriesDefault(Entry entry)
        {
			entry.hashCode = default(int);
			entry.next = default(int);
			entry.key = default(K);
			entry.value = default(T);
			entry.isNextChanged = false;
        }

		void Expand()
        {
			_capacity <<= 1;
			Entry[] temp = new Entry[_capacity];
			for (int i = 0; i < _capacity >> 1; ++i)
            {
				if (EqualityComparer<T>.Default.Equals(entries[i].value, default(T)))
					continue;

				int newIndex = GetIndex(entries[i].hashCode);
				temp[newIndex] = entries[i];
			}

			Array.Resize(ref entries, 0);
			entries = temp;
        }
		int GetIndex(int hash)
        {
			int result = (hash * 127) + 31;
			result = Math.Abs(result) % 19;
			return result % _capacity;
        }

		struct Entry
		{
			public int hashCode;
			public int next;
			public K key;
			public T value;
			public bool isNextChanged;
			// 함수 만들어서 하는거
		}
	}
}