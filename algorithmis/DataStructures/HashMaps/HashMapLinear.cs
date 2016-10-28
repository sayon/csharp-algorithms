using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithmis.Util;
namespace Algorithmis.DataStructures.HashMaps
{
    public class HashMapLinear<TKey, TValue> : IHashMap<TKey, TValue>
        where TKey : struct, IComparable<TKey>
        where TValue : struct
    {
        private List<KeyValuePair<TKey, TValue>?> _data;
        private Func<TKey, int, int> _hashFunction;
        private int _modulo; 

        public HashMapLinear(int count)
        {
            _modulo = count;
            _hashFunction = (key, attempt) => (key.GetHashCode() + attempt) % _modulo;
            _data = new List<KeyValuePair<TKey, TValue>?>()
                .Fill(null as KeyValuePair<TKey, TValue>?, _modulo);
        }

        public void Add(TKey key, TValue value)
        {
            for (int attempt = 0; attempt < _modulo; attempt++)
            {
                var hash = _hashFunction(key, attempt);
                if (!_data[hash].HasValue)
                {
                    _data[hash] = new KeyValuePair<TKey, TValue>(key, value);
                    return;
                }
            }
            throw new HashMapIsFullException();
        }
        public TValue? this[TKey key]
        {
            get
            {
                foreach (var position in positions(key))
                    if (ContainsRightKey(position, key))
                        return _data[position].Value.Value;
                return null;
            }
            set
            {
                Add(key, value.Value);
            }
        }

        public void Remove(TKey key)
        {
            var keyPositions = positions(key);
            int prev = keyPositions.First();
            bool found = false;
            foreach (var position in keyPositions)
            {
                if (found)
                    _data[prev] = _data[position];
                else
                    if (ContainsRightKey(position, key))
                        found = true;

                prev = position;
            }
            if (found) _data[prev] = null;
            else throw new KeyNotFoundException();
        }

        private IEnumerable<int> positions(TKey key)
        {
            for (int attempt = 0; attempt < _modulo; attempt++)
            {
                var hash = _hashFunction(key, attempt);
                if (_data[hash].HasValue) yield return hash;
                else yield break;
            }
        }

        private IEnumerable<KeyValuePair<TKey, TValue>> Enumerate()
        {
            return _data.Where((elem) => elem.HasValue).Select((elem) => elem.Value);
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        private bool ContainsRightKey(int position, TKey key)
        {
            return _data[position].Value.Key.IsEqual(key);
        }
    }
}
