using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mizu
{
	public sealed class NewDictionary<K, T> : IEnumerable<KeyValuePair<K, T>>
	{
		// buckets, entries 구현이 필요합니다.

		const int _defaultCapacity = 4;
		int _capacity = _defaultCapacity;
		public int Count { private set; get; } = 0;

		int[] buckets;
		List<Entry> entries = new List<Entry>();

		public NewDictionary()
        {
			buckets = new int[_defaultCapacity];
			entries = new List<Entry>(_defaultCapacity);
        }
		public NewDictionary(int capacity)
        {
			if (capacity < 1)
				throw new Exception("1 미만의 capacity는 설정할 수 없습니다.");

			_capacity = capacity;
			buckets = new int[_capacity];
			entries = new List<Entry>(_capacity);
        }

		public T this[K key]
		{
			set
			{
				int hash = key.GetHashCode();
				int index = GetIndex(hash);

				var entry = entries[buckets[index]];
				entry.value = value;
			}
			get
			{
				int hash = key.GetHashCode();
				int index = GetIndex(hash);

				return entries[buckets[index]].value;
			}
		}

		int GetIndex(int hash)
        {
			return ((hash * 127) + 31) % _capacity;
        }

		public bool ContainsKey(K key)
		{
			if (Count < 1) return false;

			foreach(var e in entries)
            {
				if (EqualityComparer<K>.Default.Equals(key, e.key))
					return true;
            }

			return false;
		}
		public bool ContainsValue(T value)
		{
			if (Count < 1) return false;

			foreach (var e in entries)
			{
				if (EqualityComparer<T>.Default.Equals(value, e.value))
					return true;
			}

			return false;
		}

		public bool TryGetValue(K key, out T result)
        {
			if (!ContainsKey(key))
			{
				result = default;
				return false;
			}

            int hash = key.GetHashCode();
            int index = GetIndex(hash);

			if(EqualityComparer<K>.Default.Equals(entries[buckets[index]].key, key))
            {
				result = entries[buckets[index]].value;
				return true;
			}
            else
            {
				index = entries[buckets[index]].next;
				while (!EqualityComparer<K>.Default.Equals(entries[buckets[index]].key, key))
                {
					index = entries[index].next;
                }

				result = entries[index].value;
				return true;
            }

			result = default;
			return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
		{
			foreach(var e in entries)
				if(EqualityComparer<T>.Default.Equals(e.value, default(T)))
					yield return new KeyValuePair<K, T>(e.key, e. value);
		}


        public bool Add(K key, T value)
        {
			if (ContainsKey(key)) return false;

			if (Count == _capacity)
				Expand();

            int hash = key.GetHashCode();
			int index = GetIndex(hash);
			if(EqualityComparer<T>.Default.Equals(entries[buckets[index]].value, default))
            {
				var entry = entries[buckets[index]];
				++Count;
            }

			return false;

        }
        //public bool Remove(K key)
        //{
        //	int hash = key.GetHashCode();

        //}

        public void Clear()
		{
			
		}

		void Expand()
        {

        }


		struct Entry
		{
			public int hashCode;
			public int next;
			public K key;
			public T value;
			public bool hasNext;
		}
	}
}