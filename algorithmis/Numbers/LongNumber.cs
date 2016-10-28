using System;
using System.Collections.Generic;
using Algorithmis.Util;
using System.Linq;
namespace Algorithmis.Numbers
{
    public class BigInt : IComparable<BigInt>
    {
        public readonly bool IsNegative;
        public IReadOnlyList<bool> Digits { get { return _digits.AsReadOnly(); } }

        protected readonly List<bool> _digits = new List<bool>();
        public static readonly BigInt One = new BigInt(1);
        public static readonly BigInt Zero = new BigInt(0);
        #region Constructors
        public BigInt(int value)
        {
            _digits = intToDigits(Math.Abs(value)).ToList<bool>();
            IsNegative = value < 0;
        }
        public BigInt(IEnumerable<bool> digits, bool sign = false)
        {
            _digits = digits.ToList();
            IsNegative = sign;
        }
        public BigInt(BigInt copy)
            : this(copy.Digits)
        {
            IsNegative = copy.IsNegative;
        }
        #endregion

        #region Arithmetic operations
        public static BigInt operator +(BigInt fst, BigInt snd)
        {
            fst.Trim(); snd.Trim();
            if (fst.IsNegative && snd.IsNegative) return -(fst.Abs() + snd.Abs());
            if (fst.IsNegative && !snd.IsNegative) return snd.Abs() - fst.Abs();
            if (!fst.IsNegative && snd.IsNegative) return fst.Abs() - snd.Abs();

            int bound = CommonBound(fst, snd) + 1;
            var res = new BigInt(0);
            bool carry = false;
            foreach (var i in 0.to(bound))
            {
                int addition = NumberUtil.CountTrue(carry, fst[i], snd[i]);
                switch (addition)
                {
                    case 0:
                        res[i] = false; carry = false; break;
                    case 1:
                        res[i] = true; carry = false; break;
                    case 2:
                        res[i] = false; carry = true; break;
                    case 3:
                        res[i] = true; carry = true; break;
                    default: break;
                }
            }
            res.Trim();
            return res;
        }

        public static BigInt operator -(BigInt fst, BigInt snd)
        {
            switch (fst.CompareTo(snd))
            {
                case 1: return Subtract(fst, snd);
                case -1: return -Subtract(snd, fst);
                default: return 0;
            }
        }
        public BigInt Abs()
        {
            return new BigInt(this.Digits, false);
        }

        public static BigInt operator -(BigInt operand)
        {
            return new BigInt(operand.Digits, !operand.IsNegative);
        }

        public static BigInt operator *(BigInt x, BigInt y)
        {
            if (x.IsOne()) return y.Copy();
            if (x.IsZero()) return x.Copy();
            BigInt z = (x >> 1) * y;
            if (x.IsEven())
                return (z << 1);
            else
                return (z << 1) + y;
        }

        public static BigInt operator /(BigInt x, BigInt y)
        {
            return Divide(x, y).item0;
        }

        /// <summary>
        /// O(n^2)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>remainder</returns>
        public static BigInt operator %(BigInt x, BigInt y)
        {
            return Divide(x, y).item1;
        }

        #endregion

        #region  Helpers

        /// <summary>
        /// Returns a pair of a division result and remainder
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>(quotient, remainder)</returns>
        public static STuple<BigInt, BigInt> Divide(BigInt x, BigInt y)
        {
            //x = divided*y + remainder
            if (y.IsZero()) throw new DivideByZeroException();
            if (x.IsZero()) return new STuple<BigInt, BigInt>(0, 0);

            var q_r = Divide(x >> 1, y); var quotient = q_r.item0; var remainder = q_r.item1;

            quotient <<= 1;
            remainder <<= 1;

            if (x.IsOdd())
                remainder += 1;

            if (remainder >= y)
            {
                remainder -= y;
                quotient += 1;
            }
            return new STuple<BigInt, BigInt>(quotient, remainder);
        }


        private static BigInt Subtract(BigInt greater, BigInt lesser)
        {
            greater.Trim(); lesser.Trim();
            var size = CommonBound(greater, lesser) + 1;
            greater.Enlarge(size); lesser.Enlarge(size);

            var sndCode = (~lesser) + 1;
            var addition = new BigInt((greater + sndCode).Digits.Take(size));
            addition.Trim();
            return addition;
        }
        #endregion


        ///// <summary>
        ///// 
        ///// xy = (x1 B^m + x0)(y1 B^m + y0) = z2 B^2m + z1 B^m + z0
        ///// z0 = x0y0.
        ///// z2 = x1y1   
        ///// z1 = (x1 + x0)(y1 + y0) - z2 - z0
        ///// </summary>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <returns></returns>
        //public static LongNumber KaratsubaMultiply(LongNumber x, LongNumber y)
        //{
        //    //FIXME is not working yet
        //    if (x.IsOne()) return y.Copy();
        //    if (y.IsOne()) return x.Copy();
        //    if (x.IsZero() || y.IsZero()) return 0;
        //    x.Trim(); y.Trim();
        //    int bound = CommonBound(x, y);
        //    int limit = bound / 2;

        //    var x1 = x >> limit;
        //    var x0 = new LongNumber(x.Digits.Take(limit));
        //    var y1 = y >> limit;
        //    var y0 = new LongNumber(y.Digits.Take(limit));

        //    var z0 = KaratsubaMultiply(x0, y0);
        //    var z2 = KaratsubaMultiply(x1, y1);

        //    var z1 = KaratsubaMultiply(x1 + x0, y1 + y0) - z2 - z0;

        //    return z2 << (2 * limit) + z1 << limit + z0;
        //}

        #region Bitwise operations

        public static BigInt operator |(BigInt a, BigInt b)
        {
            return new BigInt(a.ZipWith(b, (x, y) => x || y));
        }
        public static BigInt operator &(BigInt a, BigInt b)
        {
            return new BigInt(a.ZipWith(b, (x, y) => x && y));
        }
        public static BigInt operator ~(BigInt n)
        {
            return new BigInt(n.Digits.Select((d) => !d));
        }
        public static BigInt operator >>(BigInt x, int shift)
        {
            return new BigInt(x.Digits.Skip(shift), x.IsNegative); 
        }

        public static BigInt operator <<(BigInt x, int shift)
        {
            return new BigInt(false.Repeat(shift).Append(x.Digits));
        }

        #endregion
        #region Conversions

        public static explicit operator int(BigInt n)
        {
            int res = 0;
            n.Trim();
            foreach (var digit in n.Digits.Reverse<bool>())
            {
                res <<= 1;
                if (digit)
                    res++;
            }
            return n.IsNegative ? -res : res;
        }



        public static implicit operator BigInt(int n)
        {
            return new BigInt(n);
        }

        #endregion


        public int? Log2()
        {
            if (IsZero())
                return null;
            return Digits.LastIndexOf(true);
        }

        public BigInt Pow(BigInt power)
        {
            BigInt mult = this.Copy();
            BigInt res = 1;
            while (!power.IsZero())
            {
                if (power.IsOdd())
                {
                    res = res * mult;
                    power -= 1;
                }
                else
                {
                    mult *= mult;
                    power >>= 1;
                }
            }
            return res;
        }
        public BigInt PowMod(BigInt power, BigInt modulo)
        {
            BigInt mult = this % modulo;
            BigInt res = 1;
            while (!power.IsZero())
            {
                if (power.IsOdd())
                {
                    res = (res * mult) % modulo;
                    power -= 1;
                }
                else
                {
                    mult = (mult * mult) % modulo;
                    power >>= 1;
                }
            }
            return res;
        }


        public bool IsZero()
        {
            return Digits.All((b) => !b);
        }
        public bool IsOne()
        {
            Trim();
            return Digits.Count == 1 && Digits[0];
        }
        public bool IsEven()
        {
            return !this[0];
        }
        public bool IsOdd()
        {
            return this[0];
        }

        #region Digits operations

        private IEnumerable<bool> ZipWith(BigInt other, Func<bool, bool, bool> act)
        {
            Trim(); other.Trim();
            int bound = CommonBound(this, other);
            foreach (var i in 0.to(CommonBound(this, other)))
                yield return act(this[i], other[i]);
        }

        private static int CommonBound(BigInt x, BigInt y)
        {
            return Math.Max(x.Digits.Count, y.Digits.Count);
        }

        public bool this[int idx]
        {
            get
            {
                return idx < Digits.Count && Digits[idx];
            }
            set
            {
                Enlarge(idx);
                _digits[idx] = value;
            }
        }
        private BigInt Copy()
        {
            return new BigInt(this);
        }

        /// <summary>
        /// Trims leading zeros. Modifies (not copies) the instance.
        /// </summary>
        /// <returns>the same instance but with trimmed leading zeros</returns>
        private BigInt Trim()
        {
            while (Digits.Count > 1 && Digits.Last() != true) _digits.RemoveLast();
            return this;
        }

        /// <summary>
        /// Adds leading zeros, modifies (not copies) the instance.
        /// </summary>
        /// <param name="count">Amount of zeros to add</param>
        /// <returns>this</returns>
        private BigInt Enlarge(int count)
        {
            while (count >= Digits.Count)
                _digits.Add(false);
            return this;

        }
        private IEnumerable<bool> intToDigits(int value)
        {
            if (value == 0)
                yield return false;
            while (value > 0)
            {
                yield return value % 2 == 1;
                value >>= 1;
            }
        }
        #endregion

        #region Comparisons
        public static bool operator >(BigInt fst, BigInt snd)
        {
            return fst.CompareTo(snd) == 1;
        }
        public static bool operator >=(BigInt fst, BigInt snd)
        {
            return fst.CompareTo(snd) >= 0;
        }
        public static bool operator <=(BigInt fst, BigInt snd)
        {
            return fst.CompareTo(snd) <= 0;
        }

        public static bool operator <(BigInt fst, BigInt snd)
        {
            return fst.CompareTo(snd) == -1;
        }
        public static bool operator ==(BigInt fst, BigInt snd)
        {
            return fst.CompareTo(snd) == 0;
        }
        public static bool operator !=(BigInt fst, BigInt snd)
        {
            return fst.CompareTo(snd) != 0;
        }

        public int CompareTo(BigInt other)
        {
            if (IsNegative && other.IsNegative) return -Abs().CompareTo(other.Abs());
            if (IsNegative && !other.IsNegative) return -1;
            if (!IsNegative && other.IsNegative) return 1;

            int bound = CommonBound(this, other);
            for (int i = bound; i >= 0; i--)
            {
                if (this[i] && !other[i]) return 1;
                if (!this[i] && other[i]) return -1;
            }

            return 0;
        }
        #endregion


        public override string ToString()
        {
            //return 
            return (IsNegative ? "-" : "") +
                Digits.Fold("", (acc, b) => (b ? "1" : "0") + acc);
        }
    }

    public static class BigIntGenerator
    {
        private static Random generator = new Random();
        private static bool NextBool() { return generator.Next() % 2 == 0; } 

        /// <summary>
        /// Generate a random prime number
        /// </summary>
        /// <param name="digits">Amount of digits</param>
        /// <returns></returns>
        public static BigInt GeneratePrime(int digits)
        {
            while (true)
            {
                var num = Generate(digits);
                if (num.IsPrime()) return num;
            }
        } 

        /// <summary>
        /// Generate a random prime number in range [0..modulo)
        /// </summary> 
        /// <returns>BigInt representing prime number</returns>
        public static BigInt GeneratePrimeModulo(BigInt modulo)
        {
            while (true)
            {
                var num = GenerateModulo(modulo);
                if (num.IsPrime()) return num;
            }
        }

        /// <summary>
        /// Generate a random prime number in range [0..2^(digits+1)-1]
        /// </summary>
        /// <param name="digits">Amount of bits in this number</param>
        /// <returns>BigInt representing prime</returns>
        public static BigInt Generate(int digitsCount)
        { 
            var digits = new List<bool>().Fill(()=>generator.Next() % 2 == 0, digitsCount);
            return new BigInt(digits);
        }

        /// <summary>
        /// Generate a random number in range [0..modulo)
        /// </summary> 
        /// <returns>BigInt representing generated number</returns>
        public static BigInt GenerateModulo(BigInt modulo)
        {
            return Generate(modulo.Digits.Count) % modulo;
        }
    }
}
