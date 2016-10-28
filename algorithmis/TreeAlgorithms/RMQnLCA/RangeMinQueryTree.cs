using Algorithmis.DataStructures.Graphs;
using Algorithmis.DataStructures.Graphs.Trees;
using Algorithmis.Util;
using System;
using System.Collections.Generic;
namespace Algorithmis.TreeAlgorithms.RMQnLCA
{
    class RangeMinQueryTree<T> : IDotSerializeable, IRMQTaskSolver<T> where T : IComparable<T>
    {
        CutTree<T> _tree;
        private Func<CutTree<T>, CutTree<T>, T> minfunction =  (l, r) =>  l.Value.MinOfTwo(r.Value);
               

        public RangeMinQueryTree(IList<T> source)
        {
            _tree = CutTree<T>.Build<T>(
                source,
                0, source.Count - 1,
                minfunction
            );
        }


        public T Ask(int from, int to)
        {
            return _question(from, to, _tree);
        }



        private T _question(int from, int to, CutTree<T> node)
        {
            if (node.To == to && node.From == from)
                return node.Value;

            bool goesLeft = node.Left != null && from <= node.Left.To;
            bool goesRight = node.Right != null && to >= node.Right.From;

            T leftBranch = default(T);
            T rightBranch = default(T);

            if (goesLeft) 
                leftBranch = _question(from, Math.Min(node.Left.To, to), node.Left);
            if (goesRight) 
                rightBranch = _question(Math.Max(node.Right.From, from), to, node.Right);

            if (goesLeft && !goesRight) return leftBranch;
            if (!goesLeft && goesRight) return rightBranch;

         
            return leftBranch.MinOfTwo(rightBranch);

        }


        public string DotNodes
        {
            get { return _tree.DotNodes; }
        }

        public string DotName
        {
            get { return _tree.DotName; }
        }
        public bool IsDirectional { get { return true; } }
        public string DotFullRepr
        {
            get { return this.Envelope(); }
        }
    }
}
