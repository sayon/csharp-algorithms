//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Algorithmis.DataStructures.Graphs.Trees
//{
//    class TreeGraph<T, U> : IGraph<T, NIL>, ITreeParent<T, U> where U : ITreeParent<T, U>
//    {
//        #region Tree
//        private U _treeRoot;

//        public TreeGraph(U root) { _treeRoot = root; }

//        public U Parent
//        {
//            get
//            {
//                return _treeRoot.Parent;
//            }
//            set
//            {
//                _treeRoot.Parent = value;
//            }
//        }

//        public IEnumerable<U> Children
//        {
//            get { return _treeRoot.Children; }
//        }

//        public T Value
//        {
//            get
//            {
//                return _treeRoot.Value;
//            }
//            set
//            {
//                _treeRoot.Value = value;
//            }
//        }

//        IEnumerable<ITree> ITree.Children
//        {
//            get { return _treeRoot.Children as IEnumerable<ITree>; }
//            set { }
//        }
//        #endregion

//        #region IGraph

//        public IEnumerable<Edge<T, NIL>> Edges
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public IEnumerable<Node<T>> Vertices
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public bool HasEdge(int from, int to)
//        {
//            throw new NotImplementedException();
//        }

//        public void AddEdge(int from, int to, NIL mark = default(NIL))
//        {
//            throw new NotImplementedException();
//        }

//        public Node<T> AddNode(string name = null)
//        {
//            throw new NotImplementedException();
//        }

//        public bool RemoveNode(Node<T> node)
//        {
//            throw new NotImplementedException();
//        }

//        public bool RemoveNode(int idx)
//        {
//            throw new NotImplementedException();
//        }

//        public Edge<T, NIL> GetEdge(int from, int to)
//        {
//            throw new NotImplementedException();
//        }

//        public bool RemoveAllEdges(int from, int to)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Edge<T, NIL>> GetEdges(int from, int to)
//        {
//            throw new NotImplementedException();
//        }

//        public int NodesCount
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public int EdgesCount
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public IEnumerable<Node<T>> Adjacents(int idx)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Node<T>> Adjacents(Node<T> node)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Edge<T, NIL>> AdjacentEdges(int idx)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Edge<T, NIL>> AdjacentEdges(Node<T> node)
//        {
//            throw new NotImplementedException();
//        }

//        public Node<T> NodeByIndex(int idx)
//        {
//            throw new NotImplementedException();
//        }

//        public bool ContainsIndex(int idx)
//        {
//            throw new NotImplementedException();
//        }

//        public bool ContainsNode(Node<T> node)
//        {
//            throw new NotImplementedException();
//        }

//        public void MarkNode(int idx, T mark)
//        {
//            throw new NotImplementedException();
//        }

//        public void MarkNodes(IEnumerable<int> idxs, T mark)
//        {
//            throw new NotImplementedException();
//        }

//        public void MarkAllNodes(T mark)
//        {
//            throw new NotImplementedException();
//        }

//        public T GetNodeMark(int idx)
//        {
//            throw new NotImplementedException();
//        }

//        public void Isolate(int idx)
//        {
//            throw new NotImplementedException();
//        }

//        public void Isolate(Node<T> node)
//        {
//            throw new NotImplementedException();
//        }

//        public Node<T> this[int nodeIdx]
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public IEnumerator<Node<T>> GetEnumerator()
//        {
//            throw new NotImplementedException();
//        }

//        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//        {
//            throw new NotImplementedException();
//        }

//        public string DotNodes
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public string DotName
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public string DotFullRepr
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public bool IsDirectional
//        {
//            get { throw new NotImplementedException(); }
//        }
//        #endregion
//    }
//}
