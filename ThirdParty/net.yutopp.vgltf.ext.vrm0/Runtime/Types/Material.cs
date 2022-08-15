//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using VJson;
using VJson.Schema;

namespace VGltf.Ext.Vrm0.Types
{
    // TODO: Obsolete this class.
    [JsonSchema(Title = "vrm.material",
                Id = "vrm.material.schema.json"/* TODO: Fix usage of Id */)]
    public class Material
    {
        [JsonField(Name = "name")]
        public string Name;

        [JsonField(Name = "shader")]
        public string Shader;

        [JsonField(Name = "renderQueue")]
        public int RenderQueue = -1;

        [JsonField(Name = "floatProperties")]
        public Dictionary<string, float> FloatProperties = new Dictionary<string, float>();

        [JsonField(Name = "vectorProperties")]
        public Dictionary<string, float[]> VectorProperties = new Dictionary<string, float[]>();

        [JsonField(Name = "textureProperties")]
        public Dictionary<string, int> TextureProperties = new Dictionary<string, int>();

        [JsonField(Name = "keywordMap")]
        public Dictionary<string, bool> KeywordMap = new Dictionary<string, bool>();

        [JsonField(Name = "tagMap")]
        public Dictionary<string, string> TagMap = new Dictionary<string, string>();

        public static readonly string VRM_USE_GLTFSHADER = "VRM_USE_GLTFSHADER";
    }
}
