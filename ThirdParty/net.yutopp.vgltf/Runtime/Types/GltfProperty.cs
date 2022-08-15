//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using VJson;
using VJson.Schema;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [Json]
    public abstract class GltfProperty
    {
        [JsonField(Name = "extensions"), JsonFieldIgnorable]
        public Dictionary<string, INode> Extensions;

        [JsonField(Name = "extras"), JsonFieldIgnorable]
        public Dictionary<string, INode> Extras;

        //

        public void AddExtension<T>(string name, T value)
        {
            if (Extensions == null)
            {
                Extensions = new Dictionary<string, INode>();
            }

            var s = new JsonSerializer(typeof(T));
            var node = s.SerializeToNode(value);
            Extensions.Add(name, node);
        }

        public bool TryGetExtension<T>(string name, JsonSchemaRegistry reg, out T value)
        {
            if (Extensions == null)
            {
                value = default(T);
                return false;
            }

            INode node;
            if (!Extensions.TryGetValue(name, out node))
            {
                value = default(T);
                return false;
            }

            var v = JsonSchema.CreateFromType<T>(reg);
            var ex = v.Validate(node, reg);
            if (ex != null)
            {
                // TODO: 
                throw ex;
            }

            var d = new JsonDeserializer(typeof(T));
            var dv = d.DeserializeFromNode(node);
            value = (T)dv;

            return true;
        }

        //

        public void AddExtra<T>(string name, T value)
        {
            if (Extras == null)
            {
                Extras = new Dictionary<string, INode>();
            }

            var s = new JsonSerializer(typeof(T));
            var node = s.SerializeToNode(value);
            Extras.Add(name, node);
        }

        public bool TryGetExtra<T>(string name, JsonSchemaRegistry reg, out T value)
        {
            if (Extras == null)
            {
                value = default(T);
                return false;
            }

            INode node;
            if (!Extras.TryGetValue(name, out node))
            {
                value = default(T);
                return false;
            }

            var v = JsonSchema.CreateFromType<T>(reg);
            var ex = v.Validate(node, reg);
            if (ex != null)
            {
                // TODO: 
                throw ex;
            }

            var d = new JsonDeserializer(typeof(T));
            var dv = d.DeserializeFromNode(node);
            value = (T)dv;

            return true;
        }
    }
}
