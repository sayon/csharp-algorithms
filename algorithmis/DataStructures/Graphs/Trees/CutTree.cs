using Algorithmis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures.Graphs.Trees
{
    class CutTree<T> : BinaryTreeBase<T,CutTree<T>>
    {
        public readonly int From;
        public readonly int To; 
        public CutTree(int from, int to, T value,  
            CutTree<T> left = null, 
            CutTree<T> right = null)
            : base(value, left, right )
        {
            From = from; To = to;  
        }


        public static CutTree<U> Build<U>(IList<U> source, int from, int to, Func<CutTree<U>, CutTree<U>, U> calc)
        {
            if (to < from) return null;
            int length = to - from + 1;
            if (length == 1)
                return new CutTree<U>(from, to, source[from]);
            int leftSize = NumberUtil.IsPowerOfTwo(length) ?
                length / 2 :
                NumberUtil.NextPowerOfTwo(length) / 2;

            var leftTree = Build(source, from, from + leftSize - 1, calc);
            var rightTree = Build(source, from + leftSize, to, calc);

            return new CutTree<U>(from, to, calc(leftTree, rightTree), leftTree, rightTree);
        }

        public override string DotName
        {
            get
            {
                return String.Format("\"({0},{1}):{2}\"", From, To, Value);
            }
        }
    }
}
