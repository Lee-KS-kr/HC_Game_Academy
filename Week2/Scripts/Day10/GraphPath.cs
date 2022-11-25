using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
	public class GraphPath<T> : IEnumerable<T>
	{
		public GraphNode<T> Start { private set; get; } = null;
		public GraphNode<T> End { private set; get; } = null;
		public GraphPath() { }
		public GraphPath(GraphNode<T> startNode, GraphNode<T> endNode)
		{
			Start = startNode;
			End = endNode;
		}

		public readonly List<T> Vertexs = new List<T>();
		public int Count => Vertexs.Count;
		public bool IsNoWay => !IsSame(End.Vertex, Vertexs[Count - 1]);

		public int GetTotalWeight()
		{
			// vertexs에서 순차적으로 더하면 된다
			return 0;
		}

		public bool IsVisited(T vertex)
		{
			// vertex의 방문 여부 확인
			return Vertexs.Contains(vertex);
		}

		public bool IsPassed(T vertex)
		{
			// 마지막 vertex에서 인자로 넘어온 vertex로 향한 edge의 통과 여부 확인
			return IsPassed(End.Vertex, vertex);
		}
		public bool IsPassed(T from, T to)
        {
			GraphNode<T> node = Start; // from을 어떻게 할 것인가?
			for (int i = 1; i < Vertexs.Count; i++)
			{
				if (IsSame(node.edgeList[i-1].Node.Vertex, from) &&
					(IsSame(node.edgeList[i].Node.Vertex,to)))
					return true;
			}

			return false;
		}
		

		public GraphPath<T> Clone()
		{
			GraphPath<T> clone = new GraphPath<T>(Start, End);
			for(int i = 0; i < Count; i++)
            {
				clone.Vertexs.Add(Vertexs[i]);
            }

			return clone;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator()
		{
			foreach (var v in Vertexs)
				yield return v;
		}

		bool IsSame(T value1, T value2)
        {
			return EqualityComparer<T>.Default.Equals(value1, value2);
        }
	}
}