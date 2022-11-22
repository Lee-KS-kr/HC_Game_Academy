using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class Stack<T> : IEnumerable<T>
    {
		const int _defaultCapacity = 2;
		int _capacity = _defaultCapacity;
		int _index = -1;
		T[] arr;

		public int Count { private set; get; } = 0;

		public Stack()
        {
			arr = new T[_defaultCapacity];
        }
		public Stack(int capacity)
        {
			_capacity = capacity;
			arr = new T[_capacity];
        }

		public bool Contains(T value)
		{
			if (Count < 0)
				throw new Exception("Stack이 비어 있습니다.");

			for (int i = 0; i < Count; i++)
				if (EqualityComparer<T>.Default.Equals(arr[i], value))
					return true;

			return false;
		}

		public T Peek()
		{
			if (Count < 0)
				throw new Exception("Stack이 비어 있습니다.");

			return arr[_index];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator()
		{
			if (Count < 0)
				throw new Exception("Stack이 비어 있습니다.");

			for (int i = 0; i < Count; i++)
				yield return arr[i];
		}

		public void Push(T value)
		{
			//if (EqualityComparer<T>.Default.Equals(value, default(T)))
			//	throw new Exception("null값은 추가할 수 없습니다.");

			if (Count == _capacity)
				Expand();

			++_index;
			++Count;
			arr[_index] = value;
		}
		public T Pop()
		{
			if (Count < 0)
				throw new Exception("Stack이 비어 있습니다.");

			T value = arr[_index];
			arr[_index] = default(T);

			--_index;
			--Count;

			return value;
		}

		public void Clear()
		{
			if (Count < 0)
				throw new Exception("Stack이 비어 있습니다.");

			for (int i = 0; i < Count; i++)
            {
				arr[i] = default(T);
            }

			_index = -1;
			Count = 0;
		}

		internal void Expand()
        {
			_capacity <<= 1;
			T[] temp = new T[_capacity];

			for(int i = 0; i < Count; i++)
            {
				temp[i] = arr[i];
				arr[i] = default(T);
            }

			arr = temp;
        }
	}
}