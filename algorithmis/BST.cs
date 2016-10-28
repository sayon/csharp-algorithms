
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    /*
    class BST<T> : ITreeParent<T> where T: class, ITreeParent<T>
    {
        public int Value { get; set; }
        T _left; T _right;
        public T Left
        {
            get
            {
                return _left;
            }
            set
            {
                if (value != null) value.Parent = this as T;
                _left = value;
            }
        }

        public T Right
        {
            get
            {
                return _right;
            }
            set
            {
                if (value != null)
                    value.Parent = this as T;
                _right = value as T;
            }
        }
        public T Parent { get; set; }

        public BST(int value, T left = null, T right = null, T parent = null)
        {
            Value = value;
            Left = left;
            Right = right;
            Parent = parent;
        }
        public BST<T> Find(int key)
        {

            if (key == Value) return this;
            if (key < Value)
            {
                if (Left == null) return null;
                else return Left.Find(key);
            }
            else
            {
                if (Right == null) return null;
                else return Right.Find(key);
            }
        }
        public BST Maximum()
        {
            BST max = this;
            while (max.Right != null) max = max.Right as BST;
            return max;
        }

        public BST Minimum()
        {
            BST min = this;
            while (min.Left != null) min = min.Left as BST;
            return min;
        }
        public BST Succ()
        {
            if (Right != null)
                return (Right as BST).Minimum();
            else
            {
                BST ancestor = this.Parent as BST;
                BST res = this;
                while (ancestor != null && ancestor.Right == res)
                {
                    res = ancestor;
                    ancestor = ancestor.Parent as BST;
                }
                return res;
            }
        }
        public virtual string DottyRepr
        {
            get
            {
                return Value.ToString();
            }
        }
    }*/
}
