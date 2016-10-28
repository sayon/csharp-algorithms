using Algorithmis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace Algorithmis.Numbers
{
    public static class Euclid
    {
        /// <summary>
        /// n as digits count
        /// O(k * n^2) , where k is recursion depth and k = O(n) 
        /// x mod y is less or equal to x/2  
        /// </summary>
        /// <param name="fst"></param>
        /// <param name="snd"></param>
        /// <returns></returns>
        public static BigInt Gcd(BigInt fst, BigInt snd)
        {
            if (fst == 0) return snd;
            if (snd == 0) return fst;
            if (fst > snd) return Gcd(fst % snd, snd);
            else return Gcd(fst, snd % fst);
        }

        /// <summary>
        /// Returns (gcd, a,b) where gcd(x,y) = ax + by
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>(gcd, a,b) </returns>
        public static STuple<BigInt,BigInt, BigInt> ExtendedEuclid(BigInt x, BigInt y)
        { 
            if (y == 0) 
                return _.__(x, BigInt.One, BigInt.Zero); 
            var rec = ExtendedEuclid(y, x % y); 
            
            var d = rec.item0; var a = rec.item1; var b = rec.item2;
            
            return _.__(d, b, a - b * (x / y));  // hey what?
            //d = ay + b(x mod y)  -- by definition
            //d = ay + b (x - [x/y]*y)
            //d = bx + (a-b[x/y])y
        }
         
        /// <summary>
        /// Extended Euclid tells us how to represent GCD(x,y) = a*x+b*y
        /// If y is a modulo and GCD is 1, then x is a^-1 modulo y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="modulo"></param>
        /// <returns></returns>
        public static BigInt  InvertModulo(BigInt x, BigInt modulo)
        {
            var extEuclid = ExtendedEuclid(x, modulo);
            var gcd = extEuclid.item0; var inverted = extEuclid.item1;
            if (gcd != 1) return null;
            else return inverted.CorrectModulo(modulo);
        }

        private static BigInt CorrectModulo(this BigInt num, BigInt modulo)
        {
            while (num.IsNegative) num += modulo;
            return num;
        }

       /* public static STuple<int, int, int> ExtendedEuclid(int x, int y)
        {
            if (y == 0)
                return _.__(x, 1, 0);
            var rec = ExtendedEuclid(y, x % y);

            var d = rec.item0; var a = rec.item1; var b = rec.item2;

            return _.__(d, b, a - b * (x / y));  // hey what?
            //d = ay + b(x mod y)  -- by definition
            //d = ay + b (x - [x/y]*y)
            //d = bx + (a-b[x/y])y
        }

        public static int? InvertModulo(int x, int modulo)
        {
            var extEuclid = ExtendedEuclid(x, modulo);
            var gcd = extEuclid.item0; var inverted = extEuclid.item1;
            if (gcd != 1) return null;
            else return inverted;
        }
        */
         
    }
}
