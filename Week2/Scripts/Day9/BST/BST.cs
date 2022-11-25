using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class BSTNode<T>
    {
        public BST<T> tree;
        public T data { get; set; }
        public BSTNode<T> parent;
        public BSTNode<T> left;
        public BSTNode<T> right;
        public bool isLeaf;
        public bool hasVisited = false;

        public BSTNode() => ResetNode();
        public BSTNode(BST<T> _tree, T _data,
            BSTNode<T> _left = default, BSTNode<T> _right = default, bool _isLeaf = false, bool _hasVisited = false)
        {
            tree = _tree;
            data = _data;
            left = _left;
            right = _right;
            isLeaf = _isLeaf;
            hasVisited = _hasVisited;
        }
        public void ResetNode()
        {
            data = default;
            parent = null;
            left = null;
            right = null;
            isLeaf = false;
            hasVisited = false;
        }
    }

    public sealed class BST<T> : IEnumerable<T>
    {
        public int Count { private set; get; } = 0;
        public BSTNode<T> Root { private set; get; } = null;
        public IComparer<T> Comparer { private set; get; } = Comparer<T>.Default;

        public bool Contains(T value)
        {
            if (Count < 1)
                return false;

            // 만약 노드가 default이면 false 리턴
            BSTNode<T> node = Find(value);
            if (IsSame(node, default))
                return false;

            return true;
        }

        public BSTNode<T> Find(T value)
        {
            if (Count < 1)
                throw new Exception("Tree에 데이터가 존재하지 않습니다.");

            BSTNode<T> node = Root;
            while (true)
            {
                if (IsSame(node, default))
                    return default;

                if (Comparer.Compare(node.data, value) == 0)
                    return node;
                else
                {
                    if (node.isLeaf && Comparer.Compare(node.data, value) != 0)
                        return default;

                    if (Comparer.Compare(node.data, value) == -1)
                    {
                        node = node.right;
                        continue;
                    }
                    else
                    {
                        node = node.left;
                        continue;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            if(Root!=null)
            {
                var enumer = _Inorder(Root);
                while (enumer.MoveNext())
                    yield return enumer.Current;
            }

            IEnumerator<T> _Inorder(BSTNode<T> node)
            {
                if (node.left != null)
                {
                    var value = _Inorder(node.left);
                    while (value.MoveNext())
                        yield return value.Current;
                }

                yield return node.data;

                if (node.right != null)
                {
                    var value = _Inorder(node.right);
                    while (value.MoveNext())
                        yield return value.Current;
                }
            }
        }


        public IEnumerator<T> GetOverlaps(T min, T max)
        {
            if(Root!=null)
            {
                var enumer = _Postorder(Root);
                while (enumer.MoveNext())
                    yield return enumer.Current;
            }

            IEnumerator<T> _Postorder(BSTNode<T> node)
            {
                var compMax = Comparer.Compare(node.data, max);
                if (0 < compMax)
                {
                    if (node.left != null)
                    {
                        var value = _Postorder(node.left);
                        while (value.MoveNext())
                            yield return value.Current;
                    }

                    yield break;
                }

                var compMin = Comparer.Compare(node.data, min);
                if (0 > compMin)
                {
                    if(node.right != null)
                    {
                        var value = _Postorder(node.right);
                        while (value.MoveNext())
                            yield return value.Current;
                    }
                }
            }
        }


        public BSTNode<T> Insert(T value)
        {
            BSTNode<T> newNode = new BSTNode<T>(this, value, _isLeaf: true);
            if (Count == 0)
            {
                Root = newNode;
                Count++;
                return newNode;
            }

            if (Contains(value))
                throw new Exception("Tree에 해당 데이터가 존재합니다.");

            BSTNode<T> searchNode = Root;
            int comp = Comparer.Compare(searchNode.data, value);
            while (!searchNode.isLeaf)
            {
                comp = Comparer.Compare(searchNode.data, value);

                if (comp == -1)
                {
                    if (IsSame(searchNode.right, default))
                        break;

                    searchNode = searchNode.right;
                }
                else
                {
                    if (IsSame(searchNode.left, default))
                        break;

                    searchNode = searchNode.left;
                }
            }

            newNode.parent = searchNode;
            searchNode.isLeaf = false;
            Count++;

            comp = Comparer.Compare(searchNode.data, value);
            if (comp == -1)
                searchNode.right = newNode;
            else
                searchNode.left = newNode;

            return newNode;
        }

        public bool Remove(T value)
        {
            BSTNode<T> node = Find(value);
            if (IsSame(node, default)) return false;

            if (node.isLeaf)
            {
                if (IsSame(node.parent.left, node))
                    node.parent.left = default;
                else if (IsSame(node.parent.right, node))
                    node.parent.right = default;

                node.ResetNode();
                Count--;
                return true;
            }
            else
            {
                // 자식 노드가 두개인 경우
                if (node.left != null && node.right != null)
                {

                }
                else // 자식 노드가 하나인 경우
                {
                    if (node.left != null)
                    {
                        // 내 노드의 위치 찾기
                        node.left.parent = node.parent;

                        if (IsSame(node.parent.left, node))
                            node.parent.left = node.left;
                        else if (IsSame(node.parent.right, node))
                            node.parent.right = node.left;

                        Count--;
                        return true;
                    }
                    else
                    {
                        node.right.parent = node.parent;
                        if (IsSame(node.parent.left, node))
                            node.parent.left = node.right;
                        else if (IsSame(node.parent.right, node))
                            node.parent.right = node.right;

                        Count--;
                        return true;
                    }
                }
            }

            return false;
        }
        public void Remove(BSTNode<T> node)
        {
            if (IsSame(node, default) || node.tree != this)
                throw new Exception("Tree에 해당 노드가 존재하지 않습니다.");
        }

        public void Clear()
        {
            BSTNode<T> node = Root;
            BSTNode<T> temp = Root;
            bool isLeft = true;
            while (true)
            {
                if (node.left != null)
                {
                    node = node.left;
                    isLeft = true;
                    continue;
                }
                else if (node.right != null)
                {
                    node = node.right;
                    isLeft = false;
                    continue;
                }

                if (node.isLeaf)
                {
                    temp = node.parent;

                    if (isLeft)
                        node.parent.left = default;
                    else
                        node.parent.right = default;

                    node.ResetNode();
                    node = temp;
                }

                if (IsSame(node, Root)) break;
            }

            node.ResetNode();
        }

        bool IsSame<K>(K value1, K value2)
        {
            return EqualityComparer<K>.Default.Equals(value1, value2);
        }
    }
}