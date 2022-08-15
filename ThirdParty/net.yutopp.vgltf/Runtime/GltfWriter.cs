//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.IO;
using VJson;
using VJson.Schema;

namespace VGltf
{
    public class GltfWriter
    {
        public static void Write(Stream s, Types.Gltf gltf, JsonSchemaRegistry reg)
        {
            var schema = JsonSchema.CreateFromType<Types.Gltf>(reg);
            var ex = schema.Validate(gltf, reg);
            if (ex != null)
            {
                throw ex;
            }

            WriteWithoutValidation(s, gltf);
        }

        public static void WriteWithoutValidation(Stream s, Types.Gltf gltf)
        {
            var js = new JsonSerializer(typeof(Types.Gltf));
            js.Serialize(s, gltf);
        }
    }
}
