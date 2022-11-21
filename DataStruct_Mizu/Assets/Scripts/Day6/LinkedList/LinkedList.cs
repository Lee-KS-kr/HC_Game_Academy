using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class LinkedList : StudyBase
    {
        protected override void OnLog()
        {
            var lList = new LinkedList<string>();

            lList.AddFirst("My name is");
            lList.AddLast("AlphaGo");
            lList.AddLast("Hi");
            // My name is, AlphaGo, Hi
            lList.LogValues();

            //lList.Remove("Hi");
            lList.AddFirst("Hello");
            // Hello, My name is, AlphaGo
            lList.LogValues();

            //lList.RemoveFirst();
            //lList.AddLast("I'm glad to meet you");
            // My name is, AlphaGo, I'm glad to meet you
            //lList.LogValues();
        }
    }

    public class LinkedListNode<T>
    {
        public T data { get; }
        public LinkedListNode(T data)
        {
            this.data = data;
        }

        public LinkedListNode<T> Next { get; set; }
        public LinkedListNode<T> Prev { get; set; }
    }

    public sealed class LinkedList<T> : IEnumerable<T>
    {
        // 양방향 링크로 구현하세요
        public int Count { private set; get; } = 0;
        LinkedListNode<T> head = null;
        LinkedListNode<T> tail = null;

        public LinkedListNode<T> First { private set => head = value; get => head; }
        public LinkedListNode<T> Last { private set => tail = value; get => tail; }

        public bool Contains(T value)
        {
            LinkedListNode<T> node = First;
            while(node == Last)
            {
                if (EqualityComparer<T>.Default.Equals(node.data, value)) return true;
                node = node.Next;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> node = First;
            while (node == Last)
            {
                yield return node.data;
                node = node.Next;
            }
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);
            if(0 != Count)
            {
                head.Prev = node;
                node.Next = head;
            }

            First = node;
            ++Count;

            return node;
        }
        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);
            if (0 != Count)
            {
                Last.Next = node;
                node.Prev = tail;
            }

            Last = node;
            ++Count;

            return node;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(value);
            if (node == First)
                First = newNode;

            newNode.Prev = node.Prev;
            newNode.Next = node;
            node.Prev.Next = node;
            node.Prev = newNode;
            ++Count;

            return node;
        }
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(value);
            if (node == Last)
                Last = newNode;

            newNode.Next = node.Next;
            newNode.Prev = node;
            node.Next.Prev = node;
            node.Next = newNode;
            ++Count;

            return node;
        }

        public bool Remove(T value)
        {
            return false;
        }
        public void Remove(LinkedListNode<T> node)
        {
            if (node == null)
                throw new Exception($"{nameof(node)}가 null 입니다.");

            --Count;
        }

        public void RemoveFirst()
        {
            --Count;

        }
        public void RemoveLast()
        {
            --Count;
        }

        public void Clear()
        {
            Count = 0;
        }

    }
}