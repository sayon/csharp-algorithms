using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    /*

    class AVLTree : BST, ITreeParent<AVLTree>
    {

        public AVLTree(int value, AVLTree left = null, AVLTree right = null, AVLTree parent = null)
            : base(value, left, right, parent)
        {
        }

        public int Height { get; private set; }

        int _diff = 0;


        AVLTree insertPosition(int key, out bool isLeft)
        {
            if (key <= Value)
            {
                if (Right == null) { isLeft = true; return this; }
                else return (Right as AVLTree).insertPosition(key, out isLeft);
            }
            else
            {
                if (Right == null) { isLeft = false; return this; }
                else return (Right as AVLTree).insertPosition(key, out isLeft);
            }

        }

        public AVLTree Add(int value)
        {
            bool isleft;
            AVLTree position = insertPosition(value, out isleft);
            AVLTree prev = null;
            if (isleft)
                position.Left = prev = new AVLTree(value, parent: position);
            else 
                position.Right = prev = new AVLTree(value, parent: position);

            while (position != null)
            {
                if (prev == position.Right)
                    position._diff++;
                else
                    position._diff--;
                if (position._diff == 0) break;
                position = position.PerformRotation();

                prev = position;
                position = position.Parent as AVLTree;
            }
            return this;
        }

        #region rotations
        AVLTree smallLeftRotation()
        {
            var ret = Right;
            var tmp = Right.Left;
            Right.Left = this;
            Right = tmp;
            return ret as AVLTree;
        }

        AVLTree smallRightRotation()
        {
            var ret = Left;
            var tmp = Left.Right;
            Left.Right = this;
            Left = tmp;
            return ret as AVLTree;
        }

        AVLTree bigLeftRotation()
        {
            var ret = Right.Left;
            Right.Left = ret.Right;
            ret.Right = Right;
            Right = ret.Left;
            ret.Left = this;
            return ret as AVLTree;
        }

        AVLTree bigRightRotation()
        {
            var ret = Left.Right;
            Left.Right = ret.Left;
            ret.Left = Left;
            Left = ret.Right;
            ret.Right = this;
            return ret as AVLTree;
        }

        enum RotationType { SmallLeft, SmallRight, BigLeft, BigRight, NoRotation };
        AVLTree PerformRotation()
        {
            RotationType r = RotationType.NoRotation;

            if (this._diff == -2 && (Right as AVLTree)._diff == -1)
            {
                (Right as AVLTree)._diff = 0;
                _diff = 0;
                r = RotationType.SmallLeft; 
            }
            else if (this._diff == -2 && (Right as AVLTree)._diff == 0)
            {
                _diff = -1;
                (Right as AVLTree)._diff = 1;
                r = RotationType.SmallLeft;
            }
            //symm for right
            else if (this._diff == 2 && (Left as AVLTree)._diff == -1)
            {
                (Left as AVLTree)._diff = 0;
                _diff = 0;
                r = RotationType.SmallRight;
            }
            else if (this._diff == 2 && (Left as AVLTree)._diff == 0)
            {
                _diff = 1;
                (Left as AVLTree)._diff = -1;
                r = RotationType.SmallRight;
            }
                //left again
            else if (_diff == -2 && (Right as AVLTree)._diff == 1 && ((Right as AVLTree).Left as AVLTree)._diff == 1)
            {
                _diff = 0;
                (Right as AVLTree)._diff = -1;
                ((Right as AVLTree).Left as AVLTree)._diff = 0;
                r = RotationType.BigLeft;
            }
            else if (_diff == -2 && (Right as AVLTree)._diff == 1 && ((Right as AVLTree).Left as AVLTree)._diff == -1)
            {
                _diff = 1;
                (Right as AVLTree)._diff = 0;
                ((Right as AVLTree).Left as AVLTree)._diff = 0;
                r = RotationType.BigLeft;
            }
            else if (_diff == -2 && (Right as AVLTree)._diff == 1 && ((Right as AVLTree).Left as AVLTree)._diff == 0)
            {
                _diff = 0;
                (Right as AVLTree)._diff = 0;
                ((Right as AVLTree).Left as AVLTree)._diff = 0;
                r = RotationType.BigLeft;
            }
            //symm for right
            else if (_diff == 2 && (Left as AVLTree)._diff == -1 && ((Left as AVLTree).Right as AVLTree)._diff == -1)
            {
                _diff = 0;
                (Left as AVLTree)._diff = 1;
                ((Left as AVLTree).Right as AVLTree)._diff = 0;
                r = RotationType.BigRight;
            }
            else if (_diff == 2 && (Left as AVLTree)._diff == -1 && ((Left as AVLTree).Right as AVLTree)._diff == 1)
            {
                _diff = -1;
                (Left as AVLTree)._diff = 0;
                ((Left as AVLTree).Right as AVLTree)._diff = 0;
                r = RotationType.BigRight;
            }
            else if (_diff == 2 && (Left as AVLTree)._diff == -1 && ((Right as AVLTree).Left as AVLTree)._diff == 0)
            {
                _diff = 0;
                (Left as AVLTree)._diff = 0;
                ((Left as AVLTree).Right as AVLTree)._diff = 0;
                r = RotationType.BigRight;
            }

            switch (r)
            {
                case RotationType.BigRight: Console.WriteLine("Big right on {0}", this); return bigRightRotation();
                case RotationType.BigLeft: Console.WriteLine("Big left e on {0}", this); return bigLeftRotation();
                case RotationType.SmallRight: Console.WriteLine("Small right on {0}", this); return smallRightRotation();
                case RotationType.SmallLeft: Console.WriteLine("Small left on {0}", this); return smallLeftRotation();
                default: return this;
            }
        }
        #endregion

        public override string ToString()
        {
            return string.Format("\"({0},diff: {1})\"", Value, _diff);
        }

        static Random _randnullcounter = new Random();
        public override string DottyRepr
        {
            get
            {
                const string POINTSHAPE = " [shape=point];";
                StringBuilder sb = new StringBuilder();
                if (Left == null && Right == null) sb.AppendLine(this.ToString());
                else
                {
                    if (Left != null) sb.AppendLine(this + " ->" + Left.ToString());
                    else
                    {
                        string nullname = "null" + _randnullcounter.Next();
                        sb.AppendLine(this + " ->" + nullname);
                        sb.AppendLine(nullname + POINTSHAPE);
                    }
                    if (Right != null) sb.AppendLine(this + " ->" + Right.ToString());
                    else
                    {
                        string nullname = "null" + _randnullcounter.Next();
                        sb.AppendLine(this + " ->" + nullname);
                        sb.AppendLine(nullname + POINTSHAPE);
                    }
                }
                return sb.ToString();
            }
        }
         

    }*/
}
