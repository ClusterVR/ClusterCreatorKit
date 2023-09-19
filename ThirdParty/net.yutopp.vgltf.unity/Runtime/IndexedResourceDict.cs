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
        readonly IndexedDisposableResourceDict<K, Utils.DestroyOnDispose<V>> _internal;

        public IndexedResourceDict(IEqualityComparer<K> equalityComparer = default)
        {
            _internal = new IndexedDisposableResourceDict<K, Utils.DestroyOnDispose<V>>(equalityComparer);
        }

        public IndexedResource<V> Add(K k, int index, string name, V v)
        {
            var res = _internal.Add(k, index, name, new Utils.DestroyOnDispose<V>(v));
            return Unwrap(res);
        }

        public IndexedResource<V> this[K k]
        {
            get => Unwrap(_internal[k]);
        }

        public IndexedResource<V> GetOrCall(K k, Func<IndexedResource<V>> generator)
        {
            return Unwrap(_internal.GetOrCall(k, () => Wrap(generator())));
        }

        public async Task<IndexedResource<V>> GetOrCallAsync(K k, Func<Task<IndexedResource<V>>> generator)
        {
            return Unwrap(await _internal.GetOrCallAsync(k, async () => Wrap(await generator())));
        }

        public IEnumerable<T> Map<T>(Func<IndexedResource<V>, T> f)
        {
            return _internal.Map(wres => f(Unwrap(wres)));
        }

        public bool Contains(K k)
        {
            return _internal.Contains(k);
        }

        public bool TryGetValue(K k, out IndexedResource<V> res)
        {
            var found = _internal.TryGetValue(k, out var wres);
            if (found)
            {
                res = Unwrap(wres);
                return true;
            }

            res = default;
            return false;
        }

        public bool TryGetValueByName(string k, out IndexedResource<V> res)
        {
            var found = _internal.TryGetValueByName(k, out var wres);
            if (found)
            {
                res = Unwrap(wres);
                return true;
            }

            res = default;
            return false;
        }

        public void Dispose()
        {
            _internal.Dispose();
        }

        static IndexedResource<Utils.DestroyOnDispose<V>> Wrap(IndexedResource<V> wres)
        {
            return new IndexedResource<Utils.DestroyOnDispose<V>>(
                index: wres.Index,
                value: new Utils.DestroyOnDispose<V>(wres.Value)
            );
        }

        static IndexedResource<V> Unwrap(IndexedResource<Utils.DestroyOnDispose<V>> wres)
        {
            return new IndexedResource<V>(
                index: wres.Index,
                value: wres.Value.Value
            );
        }
    }

    public sealed class IndexedDisposableResourceDict<K, V> : IDisposable where V : IDisposable
    {
        readonly Dictionary<K, IndexedResource<V>> _dict;
        readonly MultiMap<string, IndexedResource<V>> _nameDict = new MultiMap<string, IndexedResource<V>>();

        public IndexedDisposableResourceDict(IEqualityComparer<K> equalityComparer = default)
        {
            _dict = new Dictionary<K, IndexedResource<V>>(equalityComparer);
        }

        public IndexedResource<V> Add(K k, int index, string name, V v)
        {
            var resource = new IndexedResource<V>(index, v);
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
                v.Value.Dispose();
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
