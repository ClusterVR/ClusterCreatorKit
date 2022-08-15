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
    public static class Align
    {
        public static uint CalcPadding(uint offset, uint alignment)
        {
            return (alignment - offset % alignment) % alignment;
        }

        public static uint WritePadding(Stream s, uint offset, uint alignment, byte pad = 0)
        {
            var padding = CalcPadding(offset, alignment);
            for(int i=0; i< padding; ++i)
            {
                s.WriteByte(pad);
            }

            return padding;
        }
    }
}
