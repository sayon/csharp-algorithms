using Algorithmis.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.Util
{

    public static class NumberUtil
    {
        public static int NextPowerOfTwo(this int arg)
        {
            int res = 1;
            while (res < arg && res > 0) res *= 2;
            return res;
        }
        public static bool IsPowerOfTwo(this int x)
        {
            return ((x != 0) && (x & (x - 1)) == 0);
        }
        public static IEnumerable<int> Primes(int start)
        {
            if (start <= 2) yield return 2;
            for (var num = Math.Max(3, start); num < int.MaxValue - 1; num += 2)
                if (num.IsPrime()) yield return num;
        }

        private static Random primesCheckingRandom = new Random();

        /// <summary>
        /// Failure probability is less or equal to 1/(2^attempts), 
        /// cause if one number A in [1..p-1] is failing the test AND 
        /// gcd(A,p) = 1, then 
        /// at least 1/2 of all numbers in [1..p-1] do as well -- except for Carmichael numbers.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="attempts"></param>
        /// <returns></returns>
        public static bool IsPrime(this int num, int attempts = -1)
        {
            if (attempts == -1) attempts = Math.Min(num, 100);
            for (int i = 0; i < attempts; i++)
            {
                var a = 1;
                do
                    a = 1 + primesCheckingRandom.Next(num - 1);
                while (num % a == 0);
                if (a.PowMod(num - 1, num) == 1) continue;
                else return false;
            }
            return true;
        }

        static BigInt[] firstPrimes = { 3, 5 };

        public static bool IsPrime(this BigInt num, int attempts = 6)
        {
            if (num.IsEven() || firstPrimes.Any((p) => num % p == 0 && num != p )) return false;
            foreach (var i in 0.to(attempts))
            {
                var a = new BigInt(1);
                do
                    a = BigIntGenerator.GenerateModulo(num);
                while (a.IsZero() || num % a == 0);
                if (a.PowMod(num - 1, num) == 1) continue;
                else return false;
            }
            return true;
        }



        public static int NextPrime(this int num)
        {
            return Primes(num).First();
        }
        /// <summary>
        /// Range 'from'...'to-1'
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Enumerates integers in range</returns>
        public static IEnumerable<int> to(this int from, int to)
        {
            if (from <= to)
                for (int i = from; i < to; i++)
                    yield return i;
            else
                for (int i = from; i > to; i--)
                    yield return i; 
        }

        public static IEnumerable<int> to(this int from, int step, int to)
        {
            for (int i = from; i < to; i += step)
                yield return i;
        }

        public static int PowMod(this int number, int power, int modulo)
        {
            int result = 1;
            for (int i = 0; i < power; i++)
                result = (result * number) % modulo;
            return result;
        }

        public static int AsInt(this bool b)
        {
            return b ? 1 : 0;
        }
        public static int AsSign(this bool b)
        {
            return b ? -1 : 1;
        }



        public static T MaxOfTwo<T>(this T fst, T snd) where T : IComparable<T>
        {
            switch (fst.CompareTo(snd))
            {
                case -1: return snd;
                case 1: return fst;
                default: return fst;
            }
        }

        public static T MinOfTwo<T>(this T fst, T snd) where T : IComparable<T>
        {
            switch (fst.CompareTo(snd))
            {
                case 1: return snd;
                case -1: return fst;
                default: return fst;
            }
        }
        public static bool IsEqual<T>(this T fst, T snd) where T : IComparable<T>
        {
            return fst.CompareTo(snd) == 0;

        }


    }

    public class NumberComparer<T> : Comparer<T>
    {
        private Func<T, int> extractor;
        public NumberComparer(Func<T, int> ext) { extractor = ext; }

        public override int Compare(T x, T y)
        {
            return extractor(x).CompareTo(extractor(y));
        }
    }



}
