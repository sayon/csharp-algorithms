using System;
using System.Collections.Generic;
using System.Linq;
using Algorithmis.Util;

namespace Algorithmis.DataStructures.HashMaps
{
    public class HashMapChaining<TKey, TValue> : IHashMap<TKey, TValue>
        where TKey : IComparable<TKey>
        where TValue : struct
    {

        protected readonly int _modulo;
        protected List<LinkedList<KeyValuePair<TKey, TValue>>> array;
        protected Func<TKey, int> _hashFunction;


        public HashMapChaining(int hashModulo)
        {
            _modulo = hashModulo;
            _hashFunction = (key) => key.GetHashCode() % _modulo;
            //fill with empty linked list instances
            array = new List<LinkedList<KeyValuePair<TKey, TValue>>>()
                .Fill(() => new LinkedList<KeyValuePair<TKey, TValue>>(), _modulo);
        }
        public void Add(TKey key, TValue value)
        {
            array[_hashFunction(key)]
                .AddLast(new KeyValuePair<TKey, TValue>(key, value));
        }
        public void Remove(TKey key)
        {
            var list = array[_hashFunction(key)];
            for (int i = 0; i < list.Count; i++)
                if (list.ElementAt(i).Key.CompareTo(key) == 0)
                    list.Remove(list.ElementAt(i));
        }

        public TValue? this[TKey key]
        {
            get
            {
                return array[_hashFunction(key)]
                    .First(
                    (kvp) => kvp.Key.CompareTo(key) == 0
                    ).Value;
            }
            set
            {
                Remove(key);
                Add(key, value.Value);
            }
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> Enumerate()
        {
            foreach (var list in array)
                foreach (var item in list)
                    yield return item;
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }
    }
}
