using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class GraphNode<T> : IEnumerable<KeyValuePair<T, int>>
    {
        public T Vertex;
        public List<Edge> edgeList = new List<Edge>();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            for(int i = 0; i < edgeList.Count; i++)
            {
                yield return new KeyValuePair<T, int>(edgeList[i].Vertex, edgeList[i].Weight);
            }
        }

        public struct Edge
        {
            public T Vertex;
            public GraphNode<T> Node;
            public int Weight;
            public bool IsBoth; // checkflag라는 이름이 더 적합하다
        }

        public GraphNode() { }
        public GraphNode(T data) => Vertex = data;

        public void MakeNewEdge(T nextVertex, GraphNode<T> nextNode, int weight, bool isBoth = false)
        {
            Edge edge = new Edge() { Vertex = nextVertex, Node = nextNode, Weight = weight, IsBoth = isBoth };
            edgeList.Add(edge);
        }

        public bool TryGetValue(T vertex, out Edge edge)
        {
            var temp = new Edge();
            edge = temp;
            return false;
        }

        public void Release()
        {
            // 그냥 클리어를 하면 간단하게 할 수 있다!
            foreach (var edge in edgeList)
                edgeList.Remove(edge);

            edgeList.Clear();
            Vertex = default;
        }
    }
}