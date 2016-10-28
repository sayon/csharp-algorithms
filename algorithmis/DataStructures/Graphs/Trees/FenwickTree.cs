using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithmis;
using Algorithmis.Helpers;
namespace Algorithmis.DataStructures.Graphs.Trees
{
    class FenwickTree<T> : IDotSerializeable where T : IComparable<T>
    {
        private T[] _results;
        private Func<T, T, T> _operation;
        private Func<T, T, T> _inverseOperation;

        private T _neutral;

        public FenwickTree(IList<T> arr, Func<T, T, T> oper, Func<T, T, T> inv, T neutral)
        {
            _operation = oper;
            _results = new T[arr.Count];
            _neutral = neutral;
            _inverseOperation = inv;
            for (int i = 0; i < arr.Count; i++)
                modify(i, (s) => arr[i]);
        }

        public T aggregate(int to)
        {
            T res = _neutral;

            for (int idx = to; idx >= 0; idx = f(idx) - 1)
                res = _operation(res, _results[to]);
            return res;
        }
        public T aggregate(int from, int to)
        {
            return _inverseOperation(aggregate(to), aggregate(from));
        }

        FenwickTree<T> modify(int i, Func<T, T> changer)
        {
            for (int idx = i; idx < _results.Length; idx = h(idx))
                _results[i] = changer(_results[i]);
            return this;
        }


        public static int f(int x) { return x & (x + 1); }
        public static int h(int x) { return x | (x + 1); }

        private string edge(int i, int j)
        {
            return String.Format("\"({0}; {1})\" -> \"({2}; {3})\"", i, _results[i], j, _results[j]);
        }
        
        public string DotNodes
        {
            get { 
             var sb = new StringBuilder();
            var marks = new bool[_results.Length];
            for (int i = 0; i < _results.Length; i++)
                for (int c = i; c >= 0 && !marks[c]; c = f(c) - 1)
                {
                    if (f(c) == 0)
                        sb.AppendFormat("\"{0}\"\n", c);
                    else
                        sb.AppendFormat("\"{0}\" -> \"{1}\"\n", c, f(c) - 1);
                    marks[c] = true;
                }

            return sb.ToString();
            }
        }

        public string DotName
        {
            get { return String.Empty; }
        }


        /*
         * Get an easy-to-understand representation of a Fenwick tree of a given size
         * It is in fact a forest,where roots have indices like 2^{i}-1
         */
        public static String GetTreeView(int count)
        {
            return new FenwickTree<int>(
                new int[count], 
                Operations.BinPlusInt,
                Operations.BinMinusInt,
                0
                ).DotNodes;
        } 


        public override string ToString()
        {
            return DotNodes;
        }
        public bool IsDirectional { get { return true; } }


        public string DotFullRepr
        {
            get { return this.Envelope(); }
        }
    }
}
