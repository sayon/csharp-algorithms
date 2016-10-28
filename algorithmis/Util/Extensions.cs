using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.Util
{
   
   
    public static partial class Extensions
    {

        public static IEnumerable<T> Slice<T>(this T[,] array, int varIdx, int dim)
        {
            for (int i = 0; i < array.GetLength(varIdx); i++)
                yield return array[dim, i];
        }
        public static string Strings<T>(this IEnumerable<T> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
                sb.AppendLine(item.ToString());
            return sb.ToString();
        }

        public static T[] MakeCopy<T>(this T[] arr)
        {
            T[] result = new T[arr.Length];
            Array.Copy(arr, result, arr.Length);
            return result;
        }
        public static T[,] MakeCopy<T>(this T[,] arr)
        {
            T[,] result = new T[arr.GetLength(0), arr.GetLength(1)];
            Array.Copy(arr, result, arr.Length);
            return result;
        }
    }
     
}
