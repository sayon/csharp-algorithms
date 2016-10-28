using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.Helpers
{
    public static class Operations
    {
        public static Func<int, int, int> BinPlusInt = (a, b) => a + b;
        public static Func<int, int, int> BinMinusInt = (a, b) => a - b;
        public static Func<int, int, int> BinMulInt = (a, b) => a * b;

        public static Func<double, double, double> BinPlusDouble = (a, b) => a + b;
        public static Func<double, double, double> BinMinusDouble = (a, b) => a - b;
        public static Func<double, double, double> BinMulDouble = (a, b) => a * b; 

    }
}
