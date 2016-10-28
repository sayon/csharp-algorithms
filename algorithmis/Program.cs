using Algorithmis.DataStructures.HashMaps;
using System;
using Algorithmis.Util;
using Algorithmis.Numbers;
using Algorithmis.Demos;
using Algorithmis.DataStructures;

using Algorithmis.DataStructures.AbstractStructures;
using System.Numerics;
namespace Algorithmis
{

    class Program
    {
        public static void Main(String[] args)
        {
            //var a = new Matrix<int, IntegerRing>(new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            //var b = new Matrix<int, IntegerRing>(new int[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            //Console.WriteLine(a);
            //Console.WriteLine(b);
            //Console.WriteLine(a * b);
            //Polynomial<Complex, ComplexRing> p = new Polynomial<Complex, ComplexRing>(1, 2, 3, 4, 5);
            //Polynomial<Complex, ComplexRing> q = new Polynomial<Complex, ComplexRing>(1, 2, 3, 4, 5); 
            Polynomial<Complex, ComplexRing> p = new Polynomial<Complex, ComplexRing>(1, 2, 1);
            Console.WriteLine(p);


            var q = FFT.Transform(p, false);
            Console.WriteLine(q);
            var k = FFT.Transform(q, true);
            Console.WriteLine(k);

            Console.ReadKey();
        }
    }
}
