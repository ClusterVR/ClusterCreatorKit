//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;
using VJson;

namespace VGltf.Glb
{
    public sealed class StoredBuffer
    {
        public ArraySegment<byte> Payload;
    }

    public sealed class Reader : IDisposable
    {
        readonly BinaryReader _r;

        public Reader(Stream s)
        {
            _r = new BinaryReader(s);
        }

        public void Dispose()
        {
            if (_r != null)
            {
                ((IDisposable)_r).Dispose();
            }
        }

        public Header ReadHeader()
        {
            var h = new Header();

            h.Magic = _r.ReadUInt32();
            h.Version = _r.ReadUInt32();
            h.Length = _r.ReadUInt32();

            return h;
        }

        public Chunk ReadChunk()
        {
            try
            {
                var c = new Chunk();

                c.ChunkLength = _r.ReadUInt32();
                c.ChunkType = _r.ReadUInt32();
                c.ChunkData = _r.ReadBytes((int)c.ChunkLength); // TODO: support uint length

                return c;
            }
            catch (EndOfStreamException)
            {
                return null;
            }
        }

        public static GltfContainer ReadAsContainer(Stream s)
        {
            using (var r = new Reader(s))
            {
                var h = r.ReadHeader();
                if (h.Magic != 0x46546C67) // glTF Header
                {
                    throw new NotImplementedException(); // TODO: change types
                }

                if (h.Version != 2)
                {
                    throw new NotImplementedException(); // TODO: change types
                }

                Types.Gltf gltf = null;
                StoredBuffer buffer = null;
                var reg = new VJson.Schema.JsonSchemaRegistry();

                for (var i = 0; ; ++i)
                {
                    var c = r.ReadChunk();
                    if (c == null)
                    {
                        break;
                    }

                    switch (c.ChunkType)
                    {
                        case 0x4E4F534A: // JSON
                            if (i != 0)
                            {
                                // JSON chunk must be the first chunk
                                throw new NotImplementedException("Json"); // TODO: change type
                            }

                            if (gltf != null)
                            {
                                // Duplicated
                                throw new NotImplementedException("Json"); // TODO: change type
                            }

                            using (var cs = new MemoryStream(c.ChunkData))
                            {
                                gltf = GltfReader.Read(cs, reg);
                            }

                            break;

                        case 0x004E4942: // BIN
                            if (i != 1)
                            {
                                // Binary buffer chunk must be the second chunk
                                throw new NotImplementedException("BinaryBuffer"); // TODO: change type
                            }

                            if (buffer != null)
                            {
                                // Duplicated
                                throw new NotImplementedException("BinaryBuffer"); // TODO: change type
                            }

                            buffer = new StoredBuffer
                            {
                                Payload = new ArraySegment<byte>(c.ChunkData),
                            };

                            break;

                        default:
                            // Ignore
                            continue;
                    }
                }

                if (gltf == null)
                {
                    throw new NotImplementedException("Json is empty"); // TODO: change type
                }

                return new GltfContainer(gltf, buffer, reg);
            }
        }
    }
}
