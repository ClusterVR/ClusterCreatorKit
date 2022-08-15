//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using VGltf.Types;

namespace VGltf
{
    public interface ITypedView<T> where T : struct
    {
        //IEnumerable<T> GetEnumerable();
    }

    // T: primitive types (e.g. int, float...)
    // U: extracted types (e.g. int, Vector3...)
    public sealed class TypedArrayView<T, U> where T : struct where U : struct
    {
        public U[] TypedBuffer { get; }

        public TypedArrayView(
            ArraySegment<byte> buffer,
            int stride,
            int componentSize, // Size of primitives (e.g. int = 4)
            int componentNum,  // Number of primitives in compound values (e.g. VEC3 = 3)
            int count,         // Number of compound values (e.g. Number of VEC3)
            Func<T[], U> mapper)
        {
            // assert sizeof(T) == _componentSize;
            //
            // https://www.khronos.org/files/gltf20-reference-guide.pdf
            // e.g. stride = 12
            //             20 = 8 + (12 = stride)
            // 8   12  16  20  24  28
            // |---|---|   |---|---|
            // [ x | y ]   [ x | y ] : Vec2<float>
            //

            // Deep copy...
            TypedBuffer = new U[count];

            // TODO: improve performance
            var origin = new T[componentNum];
            var gch = GCHandle.Alloc(origin, GCHandleType.Pinned);
            try
            {
                for (var i = 0; i < count; ++i)
                {
                    var strideHeadOffset = i * stride;

                    // All buffer data are little endian.
                    // See: https://github.com/KhronosGroup/glTF/blob/8e3810c01a04930a8c98b2d76232b63f4dab944f/specification/2.0/Specification.adoc#36-binary-data-storage
                    //
                    // TODO: If you use this library on machines which have other endianness, need to implement supporting that.
                    //

                    Marshal.Copy(buffer.Array, buffer.Offset + strideHeadOffset, gch.AddrOfPinnedObject(), componentSize * componentNum);
                    TypedBuffer[i] = mapper(origin);
                }
            }
            finally
            {
                gch.Free();
            }
        }

        public IEnumerable<U> GetEnumerable()
        {
            return TypedBuffer;
        }
    }

    public sealed class TypedArrayStorageFromBufferView<T, U> where T : struct where U : struct
    {
        readonly TypedArrayView<T, U> _storage;

        public TypedArrayStorageFromBufferView(ResourcesStore store,
                                               int bufferViewIndex,
                                               int byteOffset,
                                               int componentSize, // Size of primitives (e.g. int = 4)
                                               int componentNum,  // Number of primitives in compound values (e.g. VEC3 = 3)
                                               int count,         // Number of compound values (e.g. Number of VEC3)
                                               Func<T[], U> mapper)
        {
            var bufferView = store.Gltf.BufferViews[bufferViewIndex];
            var r = store.GetOrLoadBufferViewResourceAt(bufferViewIndex);

            //
            // https://www.khronos.org/files/gltf20-reference-guide.pdf
            // e.g. stride = 12
            //             20 = 8 + (12 = stride)
            // 8   12  16  20  24  28
            // |---|---|   |---|---|
            // [ x | y ]   [ x | y ] : Vec2<float>
            //
            var compoundSize = componentSize * componentNum; // e.g. Size in bytes sizeof(int * VEC3) = 4 * 3
            var stride = bufferView.ByteStride != null ? bufferView.ByteStride.Value : compoundSize;

            var buffer = new ArraySegment<byte>(r.Data.Array, r.Data.Offset + byteOffset, count * stride);

            _storage = new TypedArrayView<T, U>(buffer, stride, componentSize, componentNum, count, mapper);
        }

        public IEnumerable<U> GetEnumerable()
        {
            return _storage.GetEnumerable();
        }
    }

    public sealed class TypedArrayEntity<T, U> where T : struct where U : struct
    {
        public TypedArrayStorageFromBufferView<T, U> DenseView { get; }

        public uint[] SparseIndices { get; }
        public TypedArrayStorageFromBufferView<T, U> SparseValues { get; }

        public int Length { get; }
        readonly int _componentNum; // Number of primitives in compound values (e.g. VEC3 = 3)

        public TypedArrayEntity(ResourcesStore store, Accessor accessor, Func<T[], U> mapper)
        {
            Length = accessor.Count;
            _componentNum = accessor.Type.NumOfComponents();

            if (accessor.BufferView != null)
            {
                DenseView = new TypedArrayStorageFromBufferView<T, U>(
                    store,
                    accessor.BufferView.Value,
                    accessor.ByteOffset,
                    accessor.ComponentType.SizeInBytes(),
                    accessor.Type.NumOfComponents(),
                    accessor.Count,
                    mapper);
            }

            if (accessor.Sparse != null)
            {
                var sparse = accessor.Sparse;

                var indices = sparse.Indices;
                switch (indices.ComponentType)
                {
                    case Types.Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_BYTE:
                        SparseIndices = new TypedArrayStorageFromBufferView<byte, uint>(
                            store,
                            indices.BufferView,
                            indices.ByteOffset,
                            indices.ComponentType.SizeInBytes(),
                            1, // must be scalar
                            sparse.Count,
                            xs => (uint)xs[0]
                            )
                            .GetEnumerable()
                            .ToArray();
                        break;

                    case Types.Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_SHORT:
                        SparseIndices = new TypedArrayStorageFromBufferView<ushort, uint>(
                            store,
                            indices.BufferView,
                            indices.ByteOffset,
                            indices.ComponentType.SizeInBytes(),
                            1, // must be scalar
                            sparse.Count,
                            xs => (uint)xs[0]
                            )
                            .GetEnumerable()
                            .ToArray();
                        break;

                    case Types.Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_INT:
                        SparseIndices = new TypedArrayStorageFromBufferView<uint, uint>(
                            store,
                            indices.BufferView,
                            indices.ByteOffset,
                            indices.ComponentType.SizeInBytes(),
                            1, // must be scalar
                            sparse.Count,
                            xs => (uint)xs[0]
                            )
                            .GetEnumerable()
                            .ToArray();
                        break;
                }

                var values = sparse.Values;
                SparseValues = new TypedArrayStorageFromBufferView<T, U>(
                    store,
                    values.BufferView,
                    values.ByteOffset,
                    accessor.ComponentType.SizeInBytes(),
                    accessor.Type.NumOfComponents(),
                    sparse.Count,
                    mapper);
            }
        }

        public IEnumerable<U> GetEnumerable()
        {
            var denseArrayView = DenseView != null ? DenseView.GetEnumerable() : null;
            var sparseValuesArrayView = SparseValues != null ? SparseValues.GetEnumerable() : null;

            var sparseTargetIndex = uint.MaxValue;
            if (SparseIndices != null)
            {
                sparseTargetIndex = SparseIndices[0]; // assume that SparseIndices has at least 1 items
            }

            var defaultEmptyValue = new T[_componentNum];
            var sparseIndex = 0;
            // e.g. Vec3[0] ... Vec[i] ... Vec[Length-1]
            for (int i = 0; i < Length; ++i)
            {
                var v = default(U);

                if (denseArrayView != null)
                {
                    v = denseArrayView.ElementAt(i);
                }

                if (i == sparseTargetIndex)
                {
                    v = sparseValuesArrayView.ElementAt(sparseIndex);
                    ++sparseIndex;

                    // Set uint.MaxValue when the iterator reached to end to prevent matching for condition.
                    sparseTargetIndex = sparseIndex < SparseIndices.Length ? SparseIndices[sparseIndex] : uint.MaxValue;
                }

                yield return v;
            }
        }
    }

    public sealed class TypedBuffer
    {
        public ResourcesStore Store { get; }
        public Types.Accessor Accessor { get; }

        public TypedBuffer(ResourcesStore store, Types.Accessor accessor)
        {
            Store = store;
            Accessor = accessor;
        }

        public TypedArrayEntity<T, U> GetEntity<T, U>(Func<T[], U> mapper) where T : struct where U : struct
        {
            // TODO: Type check for safety
            return new TypedArrayEntity<T, U>(Store, Accessor, mapper);
        }

        public IEnumerable<U> GetPrimitivesAsCasted<U>() where U : struct
        {
            if (Accessor.Type != Types.Accessor.TypeEnum.Scalar)
            {
                throw new InvalidOperationException("Type must be Scalar: Actual = " + Accessor.Type);
            }

            switch (Accessor.ComponentType)
            {
                case Types.Accessor.ComponentTypeEnum.BYTE:
                    return GetEntity<sbyte, U>(xs => (U)Convert.ChangeType(xs[0], typeof(U))).GetEnumerable();

                case Types.Accessor.ComponentTypeEnum.UNSIGNED_BYTE:
                    return GetEntity<byte, U>(xs => (U)Convert.ChangeType(xs[0], typeof(U))).GetEnumerable();

                case Types.Accessor.ComponentTypeEnum.SHORT:
                    return GetEntity<short, U>(xs => (U)Convert.ChangeType(xs[0], typeof(U))).GetEnumerable();

                case Types.Accessor.ComponentTypeEnum.UNSIGNED_SHORT:
                    return GetEntity<ushort, U>(xs => (U)Convert.ChangeType(xs[0], typeof(U))).GetEnumerable();

                case Types.Accessor.ComponentTypeEnum.UNSIGNED_INT:
                    return GetEntity<uint, U>(xs => (U)Convert.ChangeType(xs[0], typeof(U))).GetEnumerable();

                case Types.Accessor.ComponentTypeEnum.FLOAT:
                    return GetEntity<float, U>(xs => (U)Convert.ChangeType(xs[0], typeof(U))).GetEnumerable();

                default:
                    throw new InvalidOperationException("Unexpected ComponentType: Actual = " + Accessor.ComponentType);
            }
        }
    }
}
