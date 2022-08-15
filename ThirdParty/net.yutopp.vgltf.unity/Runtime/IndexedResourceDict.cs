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
        readonly Dictionary<K, IndexedResource<V>> _dict = new Dictionary<K, IndexedResource<V>>();
        readonly Dictionary<string, IndexedResource<V>> _nameDict = new Dictionary<string, IndexedResource<V>>();

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

        public bool ContainsByName(string k)
        {
            return _nameDict.ContainsKey(k);
        }

        public bool TryGetValueByName(string k, out IndexedResource<V> res)
        {
            return _nameDict.TryGetValue(k, out res);
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
}
