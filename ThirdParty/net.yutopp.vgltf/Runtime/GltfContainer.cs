//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;

namespace VGltf
{
    public sealed class GltfContainer
    {
        public Types.Gltf Gltf { get; }
        public Glb.StoredBuffer Buffer { get; }
        public VJson.Schema.JsonSchemaRegistry JsonSchemas { get; }

        public GltfContainer(Types.Gltf gltf, Glb.StoredBuffer buffer = null, VJson.Schema.JsonSchemaRegistry reg = null)
        {
            Gltf = gltf;
            Buffer = buffer;
            JsonSchemas = reg;
        }

        public static GltfContainer FromGltf(Stream s, Glb.StoredBuffer buffer = null)
        {
            var reg = new VJson.Schema.JsonSchemaRegistry();
            var gltf = GltfReader.Read(s, reg);

            return new GltfContainer(gltf, buffer, reg);
        }

        public static void FromGltf(Stream s, GltfContainer container)
        {
            // TODO: Raise an error if container.Buffer is not empty
            GltfWriter.Write(s, container.Gltf, container.JsonSchemas);
        }

        public static GltfContainer FromGlb(Stream s)
        {
            return Glb.Reader.ReadAsContainer(s);
        }

        public static void ToGlb(Stream s, GltfContainer container)
        {
            Glb.Writer.WriteFromContainer(s, container);
        }
    }
}
