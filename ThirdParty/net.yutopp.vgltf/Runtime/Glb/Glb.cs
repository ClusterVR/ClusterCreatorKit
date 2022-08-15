//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

// Reference: https://github.com/KhronosGroup/glTF/tree/master/specification/2.0#glb-file-format-specification
namespace VGltf.Glb
{
    public sealed class Header
    {
        public uint Magic;
        public uint Version;
        public uint Length;
    }

    public sealed class Chunk
    {
        public uint ChunkLength;
        public uint ChunkType;
        public byte[] ChunkData; // TODO: treat it as Stream to reduce memory usage
    }
}
