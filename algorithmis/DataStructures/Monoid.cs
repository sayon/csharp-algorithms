using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures
{
    class Monoid<T>
    {
        public readonly Func<T, T, T> Operation;
        public readonly T Id;
        public Monoid(Func<T, T, T> operation, T id) { Id = id; Operation = operation; }
    }
}
