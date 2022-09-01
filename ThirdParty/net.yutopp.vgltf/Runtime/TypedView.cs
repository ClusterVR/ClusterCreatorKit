//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using VGltf.Types;

namespace VGltf
{
    public interface ITypedView<T> where T : struct
    {
        //IEnumerable<T> GetEnumerable();
    }

    public delegate Out Mapper<Elem, Out>(Elem[] xs, int i) where Elem : struct where Out : struct;

    // TODO: Rename because this class is not view already.
    public sealed class TypedArrayView<T> where T : struct
    {
        public readonly T[] TypedBuffer;

        TypedArrayView(T[] typedBuffer)
        {
            TypedBuffer = typedBuffer;
        }

        // For memory safety, this class DOES NOT convert to T directly.
        // Prim: primitive types (e.g. int, float...)
        // T: extracted types (e.g. int, Vector3...)
        public static TypedArrayView<T> CreateFromPrimitive<Prim>(
            ArraySegment<byte> buffer,
            int stride,
            int componentSize, // Size of primitives (e.g. int = 4)
            int componentNum,  // Number of primitives in compound values (e.g. VEC3 = 3)
            int count,         // Number of compound values (e.g. Number of VEC3)
            Mapper<Prim, T> mapper) where Prim : struct
        {
            // assert sizeof(T) == componentSize;
            //
            // https://www.khronos.org/files/gltf20-reference-guide.pdf
            // e.g. stride = 12
            //             20 = 8 + (12 = stride)
            // 8   12  16  20  24  28
            // |---|---|   |---|---|
            // [ x | y ]   [ x | y ] : Vec2<float>
            //

            // Deep copy...
            var typedBuffer = new T[count];

            var compoundSize = componentSize * componentNum; // e.g. Size in bytes sizeof(int * VEC3) = 4 * 3
            if (compoundSize == stride)
            {
                var origin = new Prim[componentNum * count];
                var baseArray = buffer.Array;
                var baseOffset = buffer.Offset;

                System.Buffer.BlockCopy(baseArray, baseOffset, origin, 0, componentSize * componentNum * count);

                // TODO: optimize...
                for (var i = 0; i < origin.Length / componentNum; ++i)
                {
                    typedBuffer[i] = mapper(origin, i * componentNum);
                }
            }
            else
            {
                // Target buffer is sparse, thus copy values by per components.
                // TODO: improve performance
                var origin = new Prim[componentNum];
                var baseArray = buffer.Array;
                var baseOffset = buffer.Offset;

                for (var i = 0; i < count; ++i)
                {
                    var strideHeadOffset = i * stride;

                    // All buffer data are little endian.
                    // See: https://github.com/KhronosGroup/glTF/blob/8e3810c01a04930a8c98b2d76232b63f4dab944f/specification/2.0/Specification.adoc#36-binary-data-storage
                    //
                    // TODO: If you use this library on machines which have other endianness, need to implement supporting that.

                    System.Buffer.BlockCopy(baseArray, baseOffset + strideHeadOffset, origin, 0, componentSize * componentNum);
                    typedBuffer[i] = mapper(origin, 0);
                }
            }

            return new TypedArrayView<T>(typedBuffer);
        }
    }

    public static class TypedArrayStorageFromBufferView
    {
        public static TypedArrayView<T> CreateFrom<Prim, T>(
            ResourcesStore store,
            int bufferViewIndex,
            int byteOffset,
            int componentSize, // Size of primitives (e.g. int = 4)
            int componentNum,  // Number of primitives in compound values (e.g. VEC3 = 3)
            int count,         // Number of compound values (e.g. Number of VEC3)
            Mapper<Prim, T> mapper) where Prim : struct where T : struct
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
            // TODO: assert that bufferView.ByteStride.Value >= compoundSize
            var stride = bufferView.ByteStride != null ? bufferView.ByteStride.Value : compoundSize;

            var buffer = new ArraySegment<byte>(r.Data.Array, r.Data.Offset + byteOffset, count * stride);

            return TypedArrayView<T>.CreateFromPrimitive<Prim>(buffer, stride, componentSize, componentNum, count, mapper);
        }
    }

    public sealed class TypedArrayEntity<T, U> where T : struct where U : struct
    {
        public readonly TypedArrayView<U> DenseView;

        public readonly TypedArrayView<uint> SparseIndices;
        public readonly TypedArrayView<U> SparseValues;

        public readonly int Length;
        readonly int _componentNum; // Number of primitives in compound values (e.g. VEC3 = 3)

        public TypedArrayEntity(ResourcesStore store, Accessor accessor, Mapper<T, U> mapper)
        {
            Length = accessor.Count;
            _componentNum = accessor.Type.NumOfComponents();

            if (accessor.BufferView != null)
            {
                DenseView = TypedArrayStorageFromBufferView.CreateFrom<T, U>(
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
                        SparseIndices = TypedArrayStorageFromBufferView.CreateFrom<byte, uint>(
                            store,
                            indices.BufferView,
                            indices.ByteOffset,
                            indices.ComponentType.SizeInBytes(),
                            1, // must be scalar
                            sparse.Count,
                            (xs, i) => (uint)xs[i]
                            );
                        break;

                    case Types.Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_SHORT:
                        SparseIndices = TypedArrayStorageFromBufferView.CreateFrom<ushort, uint>(
                            store,
                            indices.BufferView,
                            indices.ByteOffset,
                            indices.ComponentType.SizeInBytes(),
                            1, // must be scalar
                            sparse.Count,
                            (xs, i) => (uint)xs[i]
                            );
                        break;

                    case Types.Accessor.SparseType.IndicesType.ComponentTypeEnum.UNSIGNED_INT:
                        SparseIndices = TypedArrayStorageFromBufferView.CreateFrom<uint, uint>(
                            store,
                            indices.BufferView,
                            indices.ByteOffset,
                            indices.ComponentType.SizeInBytes(),
                            1, // must be scalar
                            sparse.Count,
                            (xs, i) => (uint)xs[i]
                            );
                        break;

                    default:
                        throw new NotSupportedException();
                }

                var values = sparse.Values;
                SparseValues = TypedArrayStorageFromBufferView.CreateFrom<T, U>(
                    store,
                    values.BufferView,
                    values.ByteOffset,
                    accessor.ComponentType.SizeInBytes(),
                    accessor.Type.NumOfComponents(),
                    sparse.Count,
                    mapper);
            }
        }

        public U[] AsArray()
        {
            var denseArrayView = DenseView != null ? DenseView.TypedBuffer : null;
            if (SparseValues == null)
            {
                return denseArrayView;
            }

            var sparseValuesArrayView = SparseValues.TypedBuffer;
            var sparseTargetIndex = SparseIndices.TypedBuffer[0]; // assume that SparseIndices has at least 1 items

            var buf = new U[Length];

            var sparseIndex = 0;
            // e.g. Vec3[0] ... Vec[i] ... Vec[Length-1]
            for (int i = 0; i < buf.Length; ++i)
            {
                var v = default(U);

                if (denseArrayView != null)
                {
                    v = denseArrayView[i];
                }

                if (i == sparseTargetIndex)
                {
                    v = sparseValuesArrayView[sparseIndex];
                    ++sparseIndex;

                    // Set uint.MaxValue when the iterator reached to end to prevent matching for condition.
                    sparseTargetIndex = sparseIndex < SparseIndices.TypedBuffer.Length ? SparseIndices.TypedBuffer[sparseIndex] : uint.MaxValue;
                }

                buf[i] = v;
            }

            return buf;
        }
    }

    public sealed class TypedBuffer
    {
        public readonly ResourcesStore Store;
        public readonly Types.Accessor Accessor;

        public TypedBuffer(ResourcesStore store, Types.Accessor accessor)
        {
            Store = store;
            Accessor = accessor;
        }

        public TypedArrayEntity<T, U> GetEntity<T, U>(Mapper<T, U> mapper) where T : struct where U : struct
        {
            // TODO: Type check for safety
            return new TypedArrayEntity<T, U>(Store, Accessor, mapper);
        }

        public int[] GetPrimitivesAsInt()
        {
            if (Accessor.Type != Types.Accessor.TypeEnum.Scalar)
            {
                throw new InvalidOperationException("Type must be Scalar: Actual = " + Accessor.Type);
            }

            switch (Accessor.ComponentType)
            {
                case Types.Accessor.ComponentTypeEnum.BYTE:
                    return GetEntity<sbyte, int>((xs, i) => (int)xs[i]).AsArray();

                case Types.Accessor.ComponentTypeEnum.UNSIGNED_BYTE:
                    return GetEntity<byte, int>((xs, i) => (int)xs[i]).AsArray();

                case Types.Accessor.ComponentTypeEnum.SHORT:
                    return GetEntity<short, int>((xs, i) => (int)xs[i]).AsArray();

                case Types.Accessor.ComponentTypeEnum.UNSIGNED_SHORT:
                    return GetEntity<ushort, int>((xs, i) => (int)xs[i]).AsArray();

                case Types.Accessor.ComponentTypeEnum.UNSIGNED_INT:
                    // May cause overflow...
                    return GetEntity<uint, int>((xs, i) => (int)xs[i]).AsArray();

                case Types.Accessor.ComponentTypeEnum.FLOAT:
                    // return GetEntity<float, U>(xs => (U)Convert.ChangeType(xs[0], typeof(U))).AsArray();
                    throw new InvalidOperationException("Cannot convert from float to int");

                default:
                    throw new InvalidOperationException("Unexpected ComponentType: Actual = " + Accessor.ComponentType);
            }
        }
    }
}
