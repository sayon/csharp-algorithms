using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures.Graphs.Trees
{
    public abstract class BinaryTreeBase<T, U> : TreeBase<T, U>
        where U : BinaryTreeBase<T, U>
    {
        private U _left;
        private U _right;
        public U Left
        {
            get
            {
                return _left;
            }
            set
            {
                if (value != null)
                    value.Parent = this as U;
                _left = value;

            }
        }
        public U Right
        {
            get
            {
                return _right;
            }
            set
            {
                if (value != null)
                    value.Parent = this as U;
                _right = value;
            }
        }
        public bool IsRightChild { get { return Parent != null && Parent.Right == this; } }
        public bool IsLeftChild { get { return Parent != null && Parent.Left == this; } }

        public BinaryTreeBase(T value, U l = null, U r = null, U par = null)
            : base(par)
        {
            Value = value; Left = l; Right = r;
        }

        public override IEnumerable<U> Children
        {
            get
            {
                if (Left != null)
                    yield return Left;
                if (Right != null)
                    yield return Right;
            }
            set
            {
                switch (value.Count())
                {
                    case 1:
                        Left = value.ElementAt(0).Actual();
                        break;
                    case 2:
                        Left = value.ElementAt(0).Actual();
                        Right = value.ElementAt(1).Actual();
                        break;
                    default:
                        break;
                }
            }
        }

        public void RightIndex()
        {
            if (IsRightChild) Index = Parent.Index * 2 + 1;
            else
                if (IsLeftChild) Index = Parent.Index * 2;
                else Index = 1;
        }
    }

    public class BinaryTree<T> : BinaryTreeBase<T, BinaryTree<T>>
    {
        public BinaryTree(): base(default(T), null,null) { }
        public BinaryTree(T value, BinaryTree<T> l = null, BinaryTree<T> r = null)
            : base(value, l, r) { }
    }
}
