using Algorithmis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.Numbers
{

    public class RSA
    {
        /// <summary>
        /// GCD(e,(p-1)(q-1)) = 1
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        public RSA(BigInt e, int digitsCount)
        {
            var pq = InitPQ(digitsCount, e);
            var p = pq.item0; var q = pq.item1; 
             
            Modulo = p * q;
            E = e;
            D = Euclid.InvertModulo(e, (p-1)*(q-1));
        }

        private STuple<BigInt,BigInt> InitPQ(int digitsCount, BigInt e)
        {
            while (true)
            {
                var p = BigIntGenerator.GeneratePrime(digitsCount);
                var q = BigIntGenerator.GeneratePrime(digitsCount);
                if (Euclid.Gcd(e, (p - 1) * (q - 1)) == 1) return _.__(p, q);  
            }
            //            throw new ArgumentException("gcd(e, (p-1)(q-1)) == 1 is required!");
        }
        public BigInt Modulo;
        public BigInt E;
        public BigInt D;
        public BigInt P;
        public BigInt Q;

        public BigInt Encrypt(BigInt message)
        {
            return message.PowMod(E, Modulo);
        }
        public BigInt Decrypt(BigInt encodedMessage)
        {
            return encodedMessage.PowMod(D, Modulo);
        }
         
    }
}
