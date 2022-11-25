using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public sealed class Graph<T> : IEnumerable<GraphNode<T>>
    {
        // 딕셔너리 구현에 실패하여 기구현된 딕셔너리 사용
        readonly System.Collections.Generic.Dictionary<T, GraphNode<T>> nodes
            = new System.Collections.Generic.Dictionary<T, GraphNode<T>>();
        public int Count => nodes.Count;

        public GraphNode<T> Add(T vertex)
        {
            GraphNode<T> node = new GraphNode<T>();
            // 조건문 수행을 먼저 하고 new를 해주는게 좋다
            // 이미 해당 키값의 노드가 해당 딕셔너리에 있으면 리턴
            if (!nodes.TryAdd(vertex, node)) return null;
            return node;
        }

        public bool Contains(T vertex)
        {
            return nodes.ContainsKey(vertex);
        }
        public bool TryGetValue(T vertex, out GraphNode<T> result)
        {
            return nodes.TryGetValue(vertex, out result);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<GraphNode<T>> GetEnumerator()
        {
            foreach(var keyValue in nodes)
            {
                yield return keyValue.Value;
            }
        }

        public void SetEdge(T from, T to, int weight, bool isBoth)
        {
            // 두개가 같으면 throw를 해 주는것도 좋다.
            GraphNode<T> fromNode = GetNodeAndCheckInvalid(from);
            GraphNode<T> toNode = GetNodeAndCheckInvalid(to);

            SetNodeEdge(fromNode, toNode, to, weight, isBoth);
            if (isBoth)
                SetNodeEdge(toNode, fromNode, from, weight, isBoth);
        }
        public void SetEdge(T a, T b, int weigth_ab, int weigth_ba)
        {
            // 두개가 같으면 throw를 해 주는것도 좋다.
            GraphNode<T> ver1 = GetNodeAndCheckInvalid(a);
            GraphNode<T> ver2 = GetNodeAndCheckInvalid(b);

            SetNodeEdge(ver1, ver2, a, weigth_ab, true);
            SetNodeEdge(ver2, ver1, b, weigth_ba, true);
        }
        void SetNodeEdge(GraphNode<T> from, GraphNode<T> to, T toVertex, int weight, bool both = false)
        {
            from.MakeNewEdge(toVertex, to, weight, both);
        }
        GraphNode<T> GetNodeAndCheckInvalid(T vertex)
        {
            GraphNode<T> node = null;
            if (!nodes.TryGetValue(vertex, out node))
                throw new Exception($"{vertex} : 해당 노드는 존재하지 않습니다.");

            return node;
        }

        public GraphPath<T> CreatePath(T start, T end)
        {
            var startNode = GetNodeAndCheckInvalid(start);
            var endNode = GetNodeAndCheckInvalid(end);
            GraphPath<T> path = new GraphPath<T>(startNode, endNode);

            return path;
        }
        
        public List<GraphPath<T>> SearchAll(T start, T end, SearchPolicy policy)
        {
            var path = CreatePath(start, end);
            var paths = new List<GraphPath<T>>();

            Search(nodes[start], ref path, policy);
            // 경로마다 탐색하며 clone에 담으면 list에 담기게 된다
            return paths;
        }
        void Search(GraphNode<T> node, ref GraphPath<T> path, SearchPolicy policy)
        {
            Queue<GraphNode<T>> q = new Queue<GraphNode<T>>();
            q.Enqueue(node);
            path.Vertexs.Add(node.Vertex);

            if (node.edgeList != null)
            {
                foreach(var edge in node.edgeList)
                {
                    q.Enqueue(edge.Node);
                }
            }

            node = q.Dequeue();

            switch (policy)
            {
                case SearchPolicy.Visit:
                    if(path.IsVisited(node.Vertex))
                    {
                        do
                        {
                            node = q.Dequeue();
                        } while (path.IsPassed(node.Vertex) || q.Count > 0);
                    }
                    break;
                case SearchPolicy.Pass:
                    if (path.IsPassed(node.Vertex))
                    {
                        do
                        {
                            node = q.Dequeue();
                            if (q.Count == 0) return;
                        } while (path.IsPassed(node.Vertex) || q.Count > 0);
                    }
                    break;
                default:
                    break;
            }

            
            Search(node, ref path, policy);
        }

        public bool Remove(T vertex)
        {
            GraphNode<T> node = null;
            if (!nodes.TryGetValue(vertex, out node))
                return false;

            node.Release(); // 이 노드를 참조하고 있는 vertex의 간선을 없애주면 더 좋을 것.
            return true;
        }

        public void Clear()
        {

        }


        public enum SearchPolicy
        {
            Visit = 0,
            Pass,
        }
    }
}