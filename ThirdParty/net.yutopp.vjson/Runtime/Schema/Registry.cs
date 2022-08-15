//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;

namespace VJson.Schema
{
    public sealed class JsonSchemaRegistry
    {
        readonly Dictionary<string, JsonSchema> _registory = new Dictionary<string, JsonSchema>();

        public JsonSchema Resolve(string id)
        {
            JsonSchema j = null;
            if (_registory.TryGetValue(id, out j))
            {
                return j;
            }

            return null;
        }

        public void Register(string id, JsonSchema j)
        {
            _registory.Add(id, j);
        }

        public IEnumerable<string> GetRegisteredIDs()
        {
            return _registory.Keys;
        }
    }
}
