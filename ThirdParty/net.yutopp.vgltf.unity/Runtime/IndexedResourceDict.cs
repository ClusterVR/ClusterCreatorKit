//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VGltf.Unity
{
    public sealed class IndexedResourceDict<K, V> : IDisposable where V : UnityEngine.Object
    {
        readonly Dictionary<K, IndexedResource<V>> _dict;
        readonly MultiMap<string, IndexedResource<V>> _nameDict = new MultiMap<string, IndexedResource<V>>();

        public IndexedResourceDict(IEqualityComparer<K> equalityComparer = default)
        {
            _dict = new Dictionary<K, IndexedResource<V>>(equalityComparer);
        }

        public IndexedResource<V> Add(K k, int index, string name, V v)
        {
            var resource = new IndexedResource<V>
            {
                Index = index,
                Value = v,
            };
            _dict.Add(k, resource);

            if (!string.IsNullOrEmpty(name))
            {
                _nameDict.Add(name, resource);
            }

            return resource;
        }

        public IndexedResource<V> this[K k]
        {
            get => _dict[k];
        }

        public IndexedResource<V> GetOrCall(K k, Func<IndexedResource<V>> generator)
        {
            // Cached by reference
            if (TryGetValue(k, out var res))
            {
                return res;
            }

            return generator();
        }

        public async Task<IndexedResource<V>> GetOrCallAsync(K k, Func<Task<IndexedResource<V>>> generator)
        {
            // Cached by reference
            if (TryGetValue(k, out var res))
            {
                return res;
            }

            return await generator();
        }

        public IEnumerable<T> Map<T>(Func<IndexedResource<V>, T> f)
        {
            return _dict.Select(kv => f(kv.Value));
        }

        public bool Contains(K k)
        {
            return _dict.ContainsKey(k);
        }

        public bool TryGetValue(K k, out IndexedResource<V> res)
        {
            return _dict.TryGetValue(k, out res);
        }

        public bool TryGetValueByName(string k, out IndexedResource<V> res)
        {
            if (!_nameDict.TryGetValues(k, out var resList))
            {
                res = default;
                return false;
            }

            // Can not distinguish that there is no element or more elements... (bad interface)
            if (resList.Count != 1)
            {
                res = default;
                return false;
            }

            res = resList.First();
            return true;
        }

        public void Dispose()
        {
            foreach (var v in _dict.Values)
            {
                Utils.Destroy(v.Value);
            }
            _dict.Clear();
            _nameDict.Clear();
        }
    }

    sealed class MultiMap<K, V>
    {
        readonly Dictionary<K, List<V>> _dict = new Dictionary<K, List<V>>();

        public void Add(K key, V value)
        {
            if (_dict.TryGetValue(key, out var values))
            {
                values.Add(value);

                return;
            }

            _dict.Add(key, new List<V>{ value });
        }

        public bool TryGetValues(K key, out List<V> values)
        {
            return _dict.TryGetValue(key, out values);
        }

        public void Clear()
        {
            _dict.Clear();
        }
    }
}
