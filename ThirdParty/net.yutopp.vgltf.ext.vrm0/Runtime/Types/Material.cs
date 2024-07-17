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
        // trueの場合、ノーマルマップ用のテクスチャをgltfのフォーマット(rgba=((x+1)/2, (y+1)/2, (z+1)/2, 1), isLinear: trueで保存する。
        // falseの場合は未定義で、現状は環境依存 (GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.UNITY_NO_DXT5nm)による) かつ isLinear: false
        // ノーマルマップ以外は(OcculusionMap等でも)変換無し、isLinear: false
        [JsonField(Name = "normalMapAsGltfFormat")]
        public bool NormalMapAsGltfFormat = false;

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
