using System;
using Algorithmis.Util;

namespace Algorithmis.DataStructures.HashMaps
{
    public static class HashFunctionsFamily
    { 
        private static Random _paramsSelector = new Random();

        public static Func<int, int> GetFunction(int a, int b, int modulo) {
            return (int key) => ((a * key + b) % modulo.NextPrime()) % modulo;
        }

        public static Func<int, int> GetRandomFunction(int modulo)
        {
            return GetFunction(_paramsSelector.Next(), _paramsSelector.Next(), modulo);
        }
    }
}
