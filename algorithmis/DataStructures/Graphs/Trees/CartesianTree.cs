using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures.Graphs.Trees
{
    public class CartesianTreeBase<TX, TY> : BinaryTreeBase<Tuple<TX, TY>, CartesianTreeBase<TX, TY>>
        where TX : IComparable<TX>
        where TY : IComparable<TY>
    {
        public static int GenerateY() { return _yGenerator.Next(); }
        private static Random _yGenerator = new Random();

        public CartesianTreeBase() : base(new Tuple<TX, TY>(default(TX), default(TY))) { }

        public CartesianTreeBase(TX x, TY y, CartesianTreeBase<TX, TY> left = null, CartesianTreeBase<TX, TY> right = null) : base(new Tuple<TX, TY>(x, y), left, right) { }


        public TY Y { get { return Value.Item2; } }
        public TX X { get { return Value.Item1; } }



        /// <summary>
        /// Merges two cartesian trees into one. Requires first to be left to second.
        /// </summary>
        /// <param name="left">Lesser tree (all keys are lesser than in the second tree)</param>
        /// <param name="right">Greater tree (all keys are greater than in the first tree)</param>
        /// <returns></returns>
        protected static CartesianTreeBase<TX, TY> Merge(CartesianTreeBase<TX, TY> left, CartesianTreeBase<TX, TY> right)
        {
            if (left == null) return right;
            if (right == null) return left;

            if (left.Y.CompareTo(right.Y) >= 0)
                return new CartesianTreeBase<TX, TY>(left.X, left.Y,
                    left.Left,
                    Merge(left.Right, right));
            else
                return new CartesianTreeBase<TX, TY>(right.X, right.Y,
                    Merge(left, right.Left),
                    right.Right);

        }

        protected void Split(TX key, out CartesianTreeBase<TX, TY> l, out CartesianTreeBase<TX, TY> r)
        {
            CartesianTreeBase<TX, TY> newt = null;
            if (X.CompareTo(key) <= 0)
            {
                if (Right == null)
                    r = null;
                else
                    Right.Split(key, out newt, out r);

                l = new CartesianTreeBase<TX, TY>(X, Y, Left, newt);
            }
            else
            {
                if (Left == null)
                    l = null;
                else
                    Left.Split(key, out l, out newt);

                r = new CartesianTreeBase<TX, TY>(X, Y, newt, Right);
            }
        }

        public CartesianTreeBase<TX, TY> Add(TX x, TY y)
        {
            CartesianTreeBase<TX, TY> l, r;
            Split(x, out l, out r);
            return Merge(Merge(l, new CartesianTreeBase<TX, TY>(x, y)), r);
        }
        public CartesianTreeBase<TX, TY> Remove(TX x, TX previous)
        {
            CartesianTreeBase<TX, TY> l, m, r;
            Split(previous, out l, out r);
            r.Split(x, out m, out r);
            return Merge(l, r);
        }

    }


    public class CartesianTree : CartesianTreeBase<int, int>
    {
        public CartesianTreeBase<int,int> Add(int x)
        {
            return base.Add(x, GenerateY()) ;
        }
        public void Remove(int x)
        {
            base.Remove(x, x - 1);
        }
    }



}
