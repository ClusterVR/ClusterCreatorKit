//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using VJson;
using VJson.Schema;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [JsonSchema(Id = "glTF.schema.json")]
    public sealed class Gltf : GltfProperty
    {
        [JsonField(Name = "extensionsUsed"), JsonFieldIgnorable]
        [JsonSchema(UniqueItems = true, MinItems = 1)]
        public List<string> ExtensionsUsed;

        [JsonField(Name = "extensionsRequired"), JsonFieldIgnorable]
        [JsonSchema(UniqueItems = true, MinItems = 1)]
        public List<string> ExtensionsRequired;

        [JsonField(Name = "accessors"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Accessor> Accessors;

        [JsonField(Name = "animations"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Animation> Animations;

        [JsonField(Name = "asset")]
        [JsonSchemaRequired]
        public Asset Asset;

        [JsonField(Name = "buffers"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Buffer> Buffers;

        [JsonField(Name = "bufferViews"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<BufferView> BufferViews;

        [JsonField(Name = "cameras"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Camera> Cameras;

        [JsonField(Name = "images"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Image> Images;

        [JsonField(Name = "materials"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Material> Materials;

        [JsonField(Name = "meshes"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Mesh> Meshes;

        [JsonField(Name = "nodes"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Node> Nodes;

        [JsonField(Name = "samplers"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Sampler> Samplers;

        [JsonField(Name = "scene"), JsonFieldIgnorable]
        [JsonSchemaDependencies("scenes"), JsonSchemaRef(typeof(GltfID))]
        public int? Scene;

        [JsonField(Name = "scenes"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Scene> Scenes;

        [JsonField(Name = "skins"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Skin> Skins;

        [JsonField(Name = "textures"), JsonFieldIgnorable]
        [JsonSchema(MinItems = 1)]
        public List<Texture> Textures;

        //

        /// <summary>
        ///   Returns indices of root nodes if the Scene is defined.
        ///   Returns empty IE<int> if the Scene is undefined or nodes are not defined in the scene.
        /// </summary>
        public IEnumerable<int> RootNodesIndices {
            get {
                if (Scene == null)
                {
                    return Enumerable.Empty<int>();
                }

                var node = Scenes[Scene.Value];
                if (node.Nodes == null) {
                    return Enumerable.Empty<int>();
                }

                return node.Nodes;
            }
        }

        /// <summary>
        ///   Returns root nodes if the Scene is defined.
        ///   Returns empty IE<Node> if the Scene is undefined or nodes are not defined in the scene.
        ///   Raise exceptions if any elements can not be found.
        /// </summary>
        public IEnumerable<Node> RootNodes {
            get {
                return RootNodesIndices.Select(i => Nodes[i]);
            }
        }
    }
}

namespace VGltf.Types.Extensions
{
    public static class GltfExtensions
    {
        public static Scene GetSceneObject(this Gltf gltf)
        {
            if (gltf.Scene == null)
            {
                throw new Exception("Scene is null");
            }

            return gltf.Scenes[gltf.Scene.Value];
        }

        /// <summary>
        ///   NOTE: Throw exceptions if elements are not found.
        /// </summary>
        public static Image GetImageByTextureIndex(this Gltf gltf, int index, out int? imageIndex)
        {
            var tex = gltf.Textures[index];
            imageIndex = tex.Source;

            return gltf.Images[imageIndex.Value];
        }

        public static Image GetImageByTextureIndex(this Gltf gltf, int index)
        {
            int? dummy;
            return GetImageByTextureIndex(gltf, index, out dummy);
        }

        public static Sampler GetSamplerByTextureIndex(this Gltf gltf, int index, out int? samplerIndex)
        {
            var tex = gltf.Textures[index];
            samplerIndex = tex.Sampler;

            return gltf.Samplers[samplerIndex.Value];
        }

        public static Sampler GetSamplerByTextureIndex(this Gltf gltf, int index)
        {
            int? dummy;
            return GetSamplerByTextureIndex(gltf, index, out dummy);
        }

        public static int AddImage(this Gltf gltf, Image item) {
            if (gltf.Images == null) {
                gltf.Images = new List<Image>();
            }

            var n = gltf.Images.Count;
            gltf.Images.Add(item);
            return n;
        }

        public static int AddAccessor(this Gltf gltf, Accessor item)
        {
            if (gltf.Accessors == null)
            {
                gltf.Accessors = new List<Accessor>();
            }

            var n = gltf.Accessors.Count;
            gltf.Accessors.Add(item);
            return n;
        }

        public static int AddMesh(this Gltf gltf, Mesh item)
        {
            if (gltf.Meshes == null)
            {
                gltf.Meshes = new List<Mesh>();
            }

            var n = gltf.Meshes.Count;
            gltf.Meshes.Add(item);
            return n;
        }

        public static int AddNode(this Gltf gltf, Node item)
        {
            if (gltf.Nodes == null)
            {
                gltf.Nodes = new List<Node>();
            }

            var n = gltf.Nodes.Count;
            gltf.Nodes.Add(item);
            return n;
        }

        public static int AddScene(this Gltf gltf, Scene item)
        {
            if (gltf.Scenes == null)
            {
                gltf.Scenes = new List<Scene>();
            }

            var n = gltf.Scenes.Count;
            gltf.Scenes.Add(item);
            return n;
        }

        public static int AddMaterial(this Gltf gltf, Material item)
        {
            if (gltf.Materials == null)
            {
                gltf.Materials = new List<Material>();
            }

            var n = gltf.Materials.Count;
            gltf.Materials.Add(item);
            return n;
        }

        public static int AddSampler(this Gltf gltf, Sampler item)
        {
            if (gltf.Samplers == null)
            {
                gltf.Samplers = new List<Sampler>();
            }

            var n = gltf.Samplers.Count;
            gltf.Samplers.Add(item);
            return n;
        }

        public static int AddTexture(this Gltf gltf, Texture item)
        {
            if (gltf.Textures == null)
            {
                gltf.Textures = new List<Texture>();
            }

            var n = gltf.Textures.Count;
            gltf.Textures.Add(item);
            return n;
        }

        public static int AddSkin(this Gltf gltf, Skin item)
        {
            if (gltf.Skins == null)
            {
                gltf.Skins = new List<Skin>();
            }

            var n = gltf.Skins.Count;
            gltf.Skins.Add(item);
            return n;
        }

        public static void AddExtensionUsed(this Gltf gltf, string name)
        {
            if (gltf.ExtensionsUsed == null)
            {
                gltf.ExtensionsUsed = new List<string>();
            }

            if (gltf.ContainsExtensionUsed(name))
            {
                return;
            }
            gltf.ExtensionsUsed.Add(name);
        }

        public static bool ContainsExtensionUsed(this Gltf gltf, string name)
        {
            if (gltf.ExtensionsUsed == null)
            {
                return false;
            }

            return gltf.ExtensionsUsed.Contains(name);
        }

        public static void AddExtensionRequired(this Gltf gltf, string name)
        {
            if (gltf.ExtensionsRequired == null)
            {
                gltf.ExtensionsRequired = new List<string>();
            }

            if (gltf.ContainsExtensionRequired(name))
            {
                return;
            }
            gltf.ExtensionsRequired.Add(name);
        }

        public static bool ContainsExtensionRequired(this Gltf gltf, string name)
        {
            if (gltf.ExtensionsRequired == null)
            {
                return false;
            }

            return gltf.ExtensionsRequired.Contains(name);
        }
    }
}
