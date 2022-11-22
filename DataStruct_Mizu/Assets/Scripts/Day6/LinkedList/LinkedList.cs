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

            lList.Remove("Hi");
            lList.AddFirst("Hello");
            // Hello, My name is, AlphaGo
            lList.LogValues();

            lList.RemoveFirst();
            lList.AddLast("I'm glad to meet you");
            // My name is, AlphaGo, I'm glad to meet you
            lList.LogValues();
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

        // exception 처리 해주기
        public LinkedListNode<T> First { private set => head = value; get => head; }
        public LinkedListNode<T> Last { private set => tail = value; get => tail; }

        public bool Contains(T value)
        {
            LinkedListNode<T> node = First;
            while(node != Last)
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
            while (node != Last)
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
            // null에 대한 exception 처리 필요
            LinkedListNode<T> newNode = new LinkedListNode<T>(value);
            if (node == First)
                First = newNode;

            // 여기 구현 잘못됐음 수정하기!
            newNode.Prev = node.Prev;
            newNode.Next = node;
            node.Prev.Next = node;
            node.Prev = newNode;
            ++Count;

            return newNode;
        }
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            // null에 대한 exception 처리 필요
            LinkedListNode<T> newNode = new LinkedListNode<T>(value);
            if (node == Last)
                Last = newNode;

            // 여기도 구현 잘못됐음 수정하기!
            newNode.Next = node.Next;
            newNode.Prev = node;
            node.Next.Prev = node;
            node.Next = newNode;
            ++Count;

            return newNode;
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> node = First;
            while (node == Last)
            {
                if (EqualityComparer<T>.Default.Equals(node.data, value))
                {
                    node.Prev.Next = node.Next;
                    node.Next.Prev = node.Prev;
                    --Count;

                    return true;
                }
                node = node.Next;
            }

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
            if (1 == Count)
                First = default;
            else
            {
                First = First.Next;
                First.Prev = null;
            }

            --Count;

        }
        public void RemoveLast()
        {
            if (1 == Count)
                Last = default;
            else
            {
                Last = Last.Prev;
                Last.Next = null;
            }

            --Count;
        }

        public void Clear()
        {
            // 뭔가 잘못됐음 여기도 수정하기
            LinkedListNode<T> node = First;
            while (node == Last)
            {
                LinkedListNode<T> next = node.Next;
                node = default;
                node = next;
            }

            First = default;
            Last = default;
            Count = 0;
        }
    }
}