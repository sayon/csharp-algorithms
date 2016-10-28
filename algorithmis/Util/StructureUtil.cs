using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.Util
{
    public enum NIL
    {
    }

    public static partial class _
    {
        public static STuple<T1, T2, T3> __<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new STuple<T1, T2, T3>(item1, item2, item3);
        }
        public static STuple<T1, T2> __<T1, T2>(T1 item1, T2 item2)
        {
            return new STuple<T1, T2>(item1, item2);
        }

        public static T Max<T>(params T[] objects) where T : IComparable<T>
        {
            return objects.Max();
        }
        public static T Min<T>(params T[] objects) where T : IComparable<T>
        {
            return objects.Min();
        }
    }
    public static partial class EnumerableUtils 
    {
        public static IEnumerable<T> EmptyEnumerable<T>() { yield break; }
    }

    public struct STuple<T1, T2>
    {
        public T1 item0;
        public T2 item1;

        public STuple(T1 item1, T2 item2)
        {
            this.item0 = item1;
            this.item1 = item2;
        }
        public override string ToString()
        {
            return String.Format("[{0};{1}]", item0, item1);
        }
    }
    public struct STuple<T1, T2, T3>
    {
        public T1 item0;
        public T2 item1;
        public T3 item2;

        public STuple(T1 item1, T2 item2, T3 item3)
        {
            this.item0 = item1;
            this.item1 = item2;
            this.item2 = item3;
        }
        
        public override string ToString()
        {
            return String.Format("[{0};{1};{2}]", item0, item1,item2);
        }
    }
}
