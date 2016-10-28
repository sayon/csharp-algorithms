using Algorithmis.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Algorithmis.Helpers;
using Algorithmis.DataStructures.AbstractStructures;
namespace Algorithmis.DataStructures
{
    /// <summary>
    /// Represents a two-dimensional matrix of elements.
    /// </summary>
    /// <typeparam name="T">Type of elements inside this matrix</typeparam>
    public sealed class Matrix<T, U> : IEnumerable<IEnumerable<T>>
        where U : IRing<T, IGroup<T>, IMonoid<T>>, new()
        where T : IEquatable<T>
    {
        public readonly U Ring = new U();

        private T[,] _data;
        public int Cols { get { return _data.GetLength(1); } }
        public int Rows { get { return _data.GetLength(0); } }
        public STuple<int, int> Size { get { return _.__(Rows, Cols); } }
        public T[,] ToArray { get { return _data.MakeCopy(); } }

        public Matrix(int rows, int cols, Func<int, int, T> filler = null)
        {
            _data = new T[rows, cols];
            if (filler != null)
                forEach((i, j, x) => filler(i, j));
        }

        public Matrix(T[,] data)
        {
            _data = data;
        }


        public T this[int row, int col]
        {
            get
            {
                if (row >= Rows || row < 0 || col >= Cols || col < 0) throw new IndexOutOfRangeException();
                return _data[row, col];
            }
            set
            {
                if (row >= Rows || row < 0 || col >= Cols || col < 0) throw new IndexOutOfRangeException();
                _data[row, col] = value;
            }
        }

        #region Operations
        public static Matrix<T, U> operator *(Matrix<T, U> fst, Matrix<T, U> snd)
        {
            if (fst.Rows != snd.Cols || fst.Cols != snd.Rows)
                throw new ArgumentException(
                    String.Format("Matrix dimensions do not match for multiplication! {0} {1}", fst.Size, snd.Size));

            return new Matrix<T, U>(fst.Rows, snd.Cols, (r, c) =>
                 snd.GetCol(c)
                 .Zip(fst.GetRow(r), fst.Ring.Multiplication.Operation)
                 .Fold(fst.Ring.Addition.Operation)
                 );
        }
        #endregion


        #region Transformations
        public Matrix<T, U> Project(Func<int, int, T, T> transform)
        {
            return new Matrix<T, U>(Rows, Cols, (i, j) => transform(i, j, this[i, j]));
        }
        public Matrix<T, U> forEach(Func<int, int, T, T> trans)
        {
            foreach (var r in 0.to(Rows))
                foreach (var c in 0.to(Cols))
                    this[r, c] = trans(r, c, this[r, c]);
            return this;
        }

        public Matrix<T, U> Transpose()
        {
            return new Matrix<T, U>(Cols, Rows, (r, c) => this[c, r]);
        }
        #endregion


        #region Serialization
        private StringBuilder _sb = new StringBuilder();
        public override string ToString()
        {
            _sb.Clear();
            for (int row = 0; row < Rows; row++)
            {
                _sb.Append(this[row, 0]);
                for (int col = 1; col < Cols; col++)
                {
                    _sb.Append('\t');
                    _sb.Append(this[row, col]);
                }
                _sb.AppendLine(";");
            }
            return _sb.ToString();
        }
        #endregion

        public IEnumerator<T> GetFlatEnumerator()
        {
            return (IEnumerator<T>)_data.GetEnumerator();
        }
        public IEnumerable<T> GetRow(int row)
        {
            foreach (var c in 0.to(Cols))
                yield return this[row, c];
        }

        public IEnumerable<T> GetCol(int col)
        {
            foreach (var r in 0.to(Rows))
                yield return this[r, col];
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<IEnumerable<T>> IEnumerable<IEnumerable<T>>.GetEnumerator()
        {
            for (int i = 0; i < _data.GetLength(0); i++)
                yield return _data.Slice(1, i);
        }



    }
    public static class Matrices{
        public static Matrix<T, U> Vandermond<T, U>(params T[] values)
            where U : IRing<T, IGroup<T>, IMonoid<T>>, new()
            where T : IEquatable<T>
        {
            return Vandermond<T, U>(values as IEnumerable<T>);
        }


        public static Matrix<T, U> Vandermond<T, U>(IEnumerable<T> values)
            where U : IRing<T, IGroup<T>, IMonoid<T>>, new()
            where T : IEquatable<T>
        {
            int dim = values.Count();
            var res = new Matrix<T, U>(dim, dim);
            int r = 0;
            foreach (var item in values)
            {
                res[r, 0] = res.Ring.Multiplication.Neutral;
                var current = item;
                foreach (var c in 1.to(dim))
                {
                    res[r, c] = current;
                    current = res.Ring.Multiplication.Operation(current, item);
                }
                r++;
            }
            return res;
        }
    }
}
