using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithmis.Util
{
    public static class IEnumerableUtils
    {
        

        public static IEnumerable<STuple<T, T>> Cartesian<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            foreach (var item1 in source)
                foreach (var item2 in other)
                    yield return new STuple<T, T>(item1, item2);
        }
        public static IEnumerable<STuple<T,T,T>> Cartesian<T>(this IEnumerable<T> source, IEnumerable<T> other0, IEnumerable<T> other1)
        {
            foreach (var item0 in source)
                foreach (var item1 in other0)
                    foreach (var item2 in other1)
                        yield return _.__(item0, item1, item2);
        }
        public static IEnumerable<STuple<T, T>> Cartesian2<T>(this IEnumerable<T> source)
        {
            return source.Cartesian(source);
        }
        public static IEnumerable<STuple<T, T, T>> Cartesian3<T>(this IEnumerable<T> source)
        {
            return source.Cartesian(source, source);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            foreach (var item in source) yield return item;
            foreach (var item in other) yield return item;
        }


        public static IEnumerable<T> Tail<T>(this IEnumerable<T> source)
        {
            bool skippedFirst = false;
            foreach (var item in source)
                if (skippedFirst)
                    yield return item;
                else
                    skippedFirst = true;
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        {
            foreach (var list in source)
                foreach (var item in list)
                    yield return item;
        }

        public static A Fold<T, A>(this IEnumerable<T> list, A acc, Func<A, T, A> transform)
        {
            var res = acc;
            foreach (var elem in list)
                res = transform(res, elem);
            return res;
        }
        public static T Fold<T>(this IEnumerable<T> list, Func<T, T, T> transform)
        {
            var res = list.First();

            foreach (var elem in list.Tail())
                res = transform(res, elem);
            return res;
        }

        public static IEnumerable<T> Repeat<T>(this T arg) { while (true) yield return arg; }

        public static IEnumerable<T> Repeat<T>(this T arg, int count) { return arg.Repeat().Take(count); }
        public static IEnumerable<T> Repeat<T>(Func<T> arg) { while (true) yield return arg(); }
        public static int LastIndexOf<T>(this IEnumerable<T> list, T item) where T : IComparable<T>
        {
            int i = 0;
            int returnIndex = 0;
            foreach (var listItem in list)
            {
                if (item.CompareTo(listItem) == 0)
                    returnIndex = i;
                i++;
            }
            return returnIndex;
        }

        public static IEnumerable<STuple<T, U>> Zip2<T, U>(this IEnumerable<T> fst, IEnumerable<U> snd)
        {
            return fst.Zip(snd, (tf, ts) => _.__(tf, ts));
        }
        //public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
        //{
        //    int counter = 0;
        //    foreach (var item in source)
        //    {
        //        if (counter < count)
        //            yield return item;
        //        else yield break;
        //        counter++;
        //    }
        //}






    }


    public static class ListExtensions
    {
        public static T Last<T>(IList<T> list)
        {
            return list[list.Count - 1];
        }

        public static T First<T>(IList<T> list)
        {
            return list[0];
        }

        public static U Fill<T, U>(this U list, T value, int count) where U : IList<T>
        {
            list.Clear();
            for (int i = 0; i < count; i++)
                list.Add(value);
            return list;
        }
        public static U Fill<T, U>(this U list, Func<T> valueGenerator, int count) where U : IList<T>
        {
            list.Clear();
            for (int i = 0; i < count; i++)
                list.Add(valueGenerator());
            return list;
        }
        public static void RemoveLast<T>(this IList<T> list)
        {
            list.RemoveAt(list.Count - 1);
        }
        public static void RemoveFirst<T>(this IList<T> list)
        {
            list.RemoveAt(0);
        }



        public static void AddAll<T>(this LinkedList<T> list, IEnumerable<T> what)
        {
            foreach (var elem in what) list.AddLast(elem);
        }

        public static void ForEach<T>(this List<T> list, Action<T, int> action)
        {
            int i = 0;
            foreach (var item in list)
            {
                action(item, i);
                i++;
            }
        }
        //public static List<T> ReversedCopy<T>(this List<T> list)
        //{ 
        //}
    }

}
