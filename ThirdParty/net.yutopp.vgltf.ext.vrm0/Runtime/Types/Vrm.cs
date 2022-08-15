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
    [JsonSchema(Title = "vrm",
                Description = "A VRM extension is for 3d humanoid avatars (and models) in VR applications.",
                Id = "vrm.schema.json"/* TODO: Fix usage of Id */)]
    public class Vrm
    {
        public static readonly string ExtensionName = "VRM";

        [JsonField(Name = "exporterVersion", Order = 0), JsonFieldIgnorable]
        [JsonSchema(Description = @"Version of exporter that vrm created.")]
        public string ExporterVersion;

        [JsonField(Name = "specVersion", Order = 10)]
        [JsonSchema(Description = @"The VRM specification version that this extension uses.")]
        public string SpecVersion = "0.0"; // If not specified, it will be treated as `0.0`.

        [JsonField(Name = "meta", Order = 20)]
        public Meta Meta = new Meta();

        [JsonField(Name = "humanoid", Order = 30)]
        public Humanoid Humanoid = new Humanoid();

        [JsonField(Name = "firstPerson", Order = 40)]
        public FirstPerson FirstPerson = new FirstPerson();

        [JsonField(Name = "blendShapeMaster", Order = 50)]
        public BlendShape BlendShapeMaster = new BlendShape();

        [JsonField(Name = "secondaryAnimation", Order = 60)]
        public SecondaryAnimation SecondaryAnimation = new SecondaryAnimation();

        [JsonField(Name = "materialProperties", Order = 70)]
        public List<Material> MaterialProperties = new List<Material>();
    }
}
