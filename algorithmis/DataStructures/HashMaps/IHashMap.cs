using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures.HashMaps
{
    public interface IHashMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey> where TValue: struct
    {
        void Add(TKey key, TValue value);
        TValue? this[TKey key] { get; set; }
        void Remove(TKey key);
    }


  
}
