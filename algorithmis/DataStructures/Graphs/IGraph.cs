using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures.Graphs
{
    public interface IGraph<V,E> : IEnumerable<Node<V>>, IDotSerializeable
    {
        IEnumerable<Edge<V,E>> Edges { get;}
        IEnumerable<Node<V>> Vertices { get; }
        bool HasEdge(int from, int to);
        void AddEdge(int from, int to, E mark = default(E)); 
        Node<V> AddNode(string name = null);
        bool RemoveNode(Node<V> node);
        bool RemoveNode(int idx);

        Edge<V, E> GetEdge(int from, int to);
        bool RemoveAllEdges(int from, int to);
        IEnumerable<Edge<V, E>> GetEdges(int from, int to);
        int NodesCount { get; }
        int EdgesCount { get; }
        IEnumerable<Node<V>> Adjacents(int idx);
        IEnumerable<Node<V>> Adjacents(Node<V> node); 
        IEnumerable<Edge<V,E>> AdjacentEdges(int idx);
        IEnumerable<Edge<V,E>> AdjacentEdges(Node<V> node);

        Node<V> NodeByIndex( int idx );
        bool ContainsIndex(int idx);
        bool ContainsNode(Node<V> node);

        void MarkNode(int idx, V mark);
        void MarkNodes(IEnumerable<int> idxs, V mark);
        void MarkAllNodes(V mark);
        V GetNodeMark(int idx);
        //E GetEdgeMark(int from, int to);
        //E GetEdgeMark(Node<V> from, Node<V> to);
        void Isolate(int idx);
        void Isolate(Node<V> node);
        Node<V> this[int nodeIdx] { get;}

    }

    public class Node<V>
    {
        public readonly string Name;
        public int Index;
        public V Mark;
        public Node(int idx, string name )
        {
            Index = idx; Name = name ?? idx.ToString(); 
        }
        public override string ToString()
        {
            return String.Format("{0} ({1})", Name, Mark);
        }
    } 

    public class Edge<V, E>
    {
        public Node<V> From;
        public Node<V> To;
        public E Mark;
        public Edge(Node<V> from, Node<V> to, E mark = default(E))
        {
            From = from; To = to; Mark = mark;
        } 
    }
}
