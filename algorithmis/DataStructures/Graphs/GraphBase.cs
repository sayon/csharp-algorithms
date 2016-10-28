using Algorithmis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithmis.DataStructures.Graphs
{
    /*
     * Graph implementation based on adjacent lists
     */
    public abstract class GraphAdjacentListsBase<V, E> : IGraph<V, E>, IDotSerializeable
    {
        List<Node<V>> nodes = new List<Node<V>>();
        List<LinkedList<Edge<V, E>>> edges = new List<LinkedList<Edge<V, E>>>();

        public GraphAdjacentListsBase(int count)
        {
            nodes.Capacity = count;
            edges.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                nodes.Add(new Node<V>(i, null));
                edges.Add(new LinkedList<Edge<V, E>>());
            }
        }

        public virtual IEnumerable<Node<V>> Vertices
        {
            get { return nodes as IEnumerable<Node<V>>; }
        }


        public virtual int NodesCount
        {
            get { return nodes.Count; }
        }

        public virtual int EdgesCount
        {
            get { return edges.Select((l) => l.Count).Sum(); }
        }

        public virtual IEnumerable<Node<V>> Adjacents(int idx)
        {
            if (idx >= NodesCount) throw new IndexOutOfRangeException("This graph does not contain a node with index " + idx);
            return edges[idx] as IEnumerable<Node<V>>;
        }

        public virtual IEnumerable<Node<V>> Adjacents(Node<V> node)
        {
            var idx = nodes.IndexOf(node);
            if (idx == -1) throw new InvalidOperationException("This graph does not contain the given node");
            return edges[idx].Select((edge) => nodes[edge.To.Index]);
        }

        public virtual IEnumerable<Edge<V, E>> AdjacentEdges(int idx)
        {
            return edges[idx] as IEnumerable<Edge<V, E>>;
        }

        public virtual IEnumerable<Edge<V, E>> AdjacentEdges(Node<V> node)
        {
            var idx = nodes.IndexOf(node);
            if (idx == -1) throw new IndexOutOfRangeException("This graph does not contain a node  " + idx);
            return edges[idx] as IEnumerable<Edge<V, E>>;
        }


        public virtual Node<V> NodeByIndex(int idx)
        {
            foreach (var node in this)
                if (node.Index == idx) return node;

            throw new IndexOutOfRangeException("This graph does not contain a node with index " + idx);
        }

        public virtual IEnumerator<Node<V>> GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return nodes.GetEnumerator();
        }


        public virtual bool ContainsIndex(int idx)
        {
            return idx < NodesCount;
        }

        public virtual bool ContainsNode(Node<V> node)
        {
            return this.Contains(node);
        }


        public virtual bool HasEdge(int from, int to)
        {
            if (!ContainsIndex(from))
                throw new IndexOutOfRangeException("'from' out of range: " + from);
            if (!ContainsIndex(to))
                throw new IndexOutOfRangeException("'to' out of range: " + to);

            return edges[from].Any((edge) => edge.To.Index == to);
        }

        public virtual IEnumerable<Edge<V, E>> Edges
        {
            get
            {
                return edges.Flatten();
            }
        }


        public virtual IEnumerable<Edge<V, E>> GetEdges(int from, int to)
        {
            return edges[from].Where((edge) => edge.To.Index == to);
        }


        public virtual Edge<V, E> GetEdge(int from, int to)
        {
            var es = GetEdges(from, to);
            if (es.Count() == 0) return new Edge<V, E>(null, null);
            else return es.First();
        }

        public virtual void AddEdge(int from, int to, E mark = default(E))
        {
            edges[from].AddLast(new Edge<V, E>(nodes[from], nodes[to], mark));
        }



        public virtual bool RemoveAllEdges(int from, int to)
        {
            if (!HasEdge(from, to)) return false;
            foreach (var edge in GetEdges(from, to).ToList())
                edges[from].Remove(edge);
            return true;
        }


        public virtual Node<V> AddNode(string name = null)
        {
            var node = new Node<V>(NodesCount, name);
            nodes.Add(node);
            edges.Add(new LinkedList<Edge<V, E>>());
            return node;
        }

        public virtual void Isolate(int idx)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                var filtered = edges[i].Where((edge) => edge.To.Index != idx && edge.From.Index != idx);
                edges[i].Clear();
                edges[i].AddAll(filtered);
            }
        }

        public virtual void Isolate(Node<V> node)
        {
            var idx = node.Index;
            Isolate(idx);
        }
        public virtual bool RemoveNode(Node<V> node)
        {
            if (!ContainsNode(node)) return false;
            Isolate(node);

            var idx = node.Index;
            foreach (var edge in Edges)
            {
                if (edge.To.Index > idx) edge.To = nodes[edge.To.Index - 1];
                if (edge.From.Index > idx) edge.From = nodes[edge.From.Index - 1];
            }
            nodes.RemoveAt(idx);
            return true;
        }

        public virtual bool RemoveNode(int idx)
        {
            return ContainsIndex(idx) && RemoveNode(nodes[idx]);
        }

        public virtual string DotNodes
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var node in this)
                    sb.AppendLine("\"" + node + "\"");
                foreach (var edge in Edges)
                    sb.AppendFormat("\"{0}\" -> \"{1}\" \n", edge.From, edge.To);
                return sb.ToString();
            }
        }

        public virtual string DotName
        {
            get { return String.Format("graph: {0} vertices, {1} edges", NodesCount, EdgesCount); }
        }
        public override string ToString()
        {
            return DotName;
        }
        public virtual bool IsDirectional { get { return true; } }


        public virtual string DotFullRepr
        {
            get { return this.Envelope(); }
        }


        public Node<V> this[int nodeIdx]
        {
            get
            {
                return nodes[nodeIdx];
            }
        }


        public void MarkNode(int idx, V mark)
        {
            nodes[idx].Mark = mark;
        }

        public void MarkNodes(IEnumerable<int> idxs, V mark)
        {
            foreach (var idx in idxs)
                MarkNode(idx, mark);
        }

        public V GetNodeMark(int idx)
        {
            return nodes[idx].Mark;
        }


        public void MarkAllNodes(V mark)
        {
            for (int i = 0; i < NodesCount; i++)
                MarkNode(i, mark);
        }
    }
}
