//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;

namespace VGltf.Glb
{
    public sealed class Writer : IDisposable
    {
        readonly BinaryWriter _w;

        public Writer(Stream s)
        {
            _w = new BinaryWriter(s);
        }

        public void Dispose()
        {
            if (_w != null)
            {
                ((IDisposable)_w).Dispose();
            }
        }

        public void WriteHeader(Header h)
        {
            _w.Write(h.Magic);
            _w.Write(h.Version);
            _w.Write(h.Length);
        }

        public void WriteChunk(Chunk c)
        {
            _w.Write(c.ChunkLength);
            _w.Write(c.ChunkType);
            _w.Write(c.ChunkData);
        }

        public static void WriteFromContainer(Stream s, GltfContainer container)
        {
            const uint HeaderSize = 12;
            const uint ChunkHeaderSize = 8;

            using (var w = new Writer(s))
            {
                uint chunksTotalSize = 0;

                // TODO: make this processes as streamable
                byte[] gltf = null;
                if (container.Gltf == null)
                {
                    throw new NotImplementedException("Json is empty"); // TODO: change type
                }
                using (var cs = new MemoryStream())
                {
                    GltfWriter.Write(cs, container.Gltf, container.JsonSchemas);
                    gltf = cs.ToArray();
                }
                chunksTotalSize += ChunkHeaderSize + (uint)gltf.Length;

                var padding = Align.CalcPadding(chunksTotalSize, 4); // Must be 4Bytes aligned
                chunksTotalSize += padding;

                byte[] buffer = null;
                if (container.Buffer != null)
                {
                    using (var cs = new MemoryStream())
                    {
                        var payload = container.Buffer.Payload;
                        cs.Write(payload.Array, payload.Offset, payload.Count);
                        buffer = cs.ToArray();
                    }
                    chunksTotalSize += ChunkHeaderSize + (uint)buffer.Length;
                }

                // Start writing
                w.WriteHeader(new Header
                {
                    Magic = 0x46546C67,
                    Version = 2,
                    Length = HeaderSize + chunksTotalSize,
                });

                w.WriteChunk(new Chunk
                {
                    ChunkLength = (uint)gltf.Length + padding,
                    ChunkType = 0x4E4F534A, // JSON
                    ChunkData = gltf,
                });
                w._w.Write(new byte[]{ 0x20, 0x20, 0x20, 0x20 }, 0, (int)padding); // Add space padding if needed. 0x20 == ' '.

                if (buffer != null)
                {
                    w.WriteChunk(new Chunk
                    {
                        ChunkLength = (uint)buffer.Length,
                        ChunkType = 0x004E4942, // BIN
                        ChunkData = buffer,
                    });
                }
            }
        }
    }
}
