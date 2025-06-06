using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;
using VGltf;
using VGltf.Types;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public static class GltfValidator
    {
        const int MaxScenesCount = 1;
        const int MaxNodesCount = 128;
        const int MaxMeshesCount = 8;
        const int MaxMeshRenderersCount = 32;
        const int MaxSubMeshesCount = 2;

        const int MaxTextureSize = 8192;

        internal static IEnumerable<ValidationMessage> ValidateName(GameObject gameObject)
        {
            var nodeNameCountDict = new Dictionary<string, int>();
            var validationMessages = new List<ValidationMessage>();

            void CheckNodeNameFunc(GameObject go)
            {
                if (nodeNameCountDict.TryGetValue(go.name, out var count))
                {
                    if (count == 1)
                    {
                        validationMessages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_duplicate_gameobject_names, go.name),
                            ValidationMessage.MessageType.Error));
                    }
                    nodeNameCountDict[go.name]++;
                }
                else
                {
                    nodeNameCountDict.Add(go.name, 1);
                }
                foreach (Transform childTransform in go.transform)
                {
                    CheckNodeNameFunc(childTransform.gameObject);
                }
            }

            CheckNodeNameFunc(gameObject);
            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateScene(GltfContainer gltfContainer)
        {
            var validationMessages = new List<ValidationMessage>();
            var gltf = gltfContainer.Gltf;
            if (gltf.Scenes == null || gltf.Scenes.Count == 0)
            {
                validationMessages.Add(
                    new ValidationMessage(TranslationTable.cck_scene_required, ValidationMessage.MessageType.Error));
            }
            else if (gltf.Scenes.Count > MaxScenesCount)
            {
                validationMessages.Add(CreateCountValidationMessage("Scene", gltf.Scenes.Count, MaxScenesCount));
            }
            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateNode(GltfContainer gltfContainer)
        {
            var validationMessages = new List<ValidationMessage>();
            var gltf = gltfContainer.Gltf;
            if (gltf.Nodes.Count > MaxNodesCount)
            {
                validationMessages.Add(CreateCountValidationMessage(TranslationTable.cck_prefab_gameobjects, gltf.Nodes.Count,
                    MaxNodesCount));
            }
            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateMesh(GltfContainer gltfContainer,
            int maxTrianglesCount)
        {
            var validationMessages = new List<ValidationMessage>();

            var gltf = gltfContainer.Gltf;
            var resourcesStore = new ResourcesStore(gltfContainer, new ResourceLoaderFromEmbedOnly());

            if (gltf.Meshes == null || gltf.Meshes.Count == 0)
            {
                validationMessages.Add(new ValidationMessage(TranslationTable.cck_mesh_required_for_item,
                    ValidationMessage.MessageType.Error));
                return validationMessages;
            }

            if (gltf.Meshes.Count > MaxMeshesCount)
            {
                validationMessages.Add(CreateCountValidationMessage("Mesh", gltf.Meshes.Count, MaxMeshesCount));
            }

            int meshRenderersCount = gltf.Nodes.Count(node => node.Mesh != null);
            if (meshRenderersCount > MaxMeshRenderersCount)
            {
                validationMessages.Add(CreateCountValidationMessage("MeshRenderer", meshRenderersCount, MaxMeshRenderersCount));
            }

            var triangleCount = 0;
            foreach (var mesh in gltf.Meshes)
            {
                if (mesh.Primitives.Count > MaxSubMeshesCount)
                {
                    validationMessages.Add(CreateCountValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_submesh, mesh.Name), mesh.Primitives.Count,
                        MaxSubMeshesCount));
                }

                if (IsSkinnedMesh(mesh))
                {
                    validationMessages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_skinnedmesh_not_allowed, mesh.Name),
                        ValidationMessage.MessageType.Error));
                }

                foreach (var primitive in mesh.Primitives)
                {
                    if (primitive.Mode != VGltf.Types.Mesh.PrimitiveType.ModeEnum.TRIANGLES)
                    {
                        validationMessages.Add(new ValidationMessage(
                            TranslationUtility.GetMessage(TranslationTable.cck_non_triangles_mode_set, mesh.Name, VGltf.Types.Mesh.PrimitiveType.ModeEnum.TRIANGLES.ToString(), primitive.Mode.ToString()),
                            ValidationMessage.MessageType.Error));
                        continue;
                    }
                    if (primitive.Indices == null)
                    {
                        continue;
                    }

                    triangleCount += LoadTrianglesCount(resourcesStore, primitive.Indices.Value);
                }
            }

            if (triangleCount == 0)
            {
                validationMessages.Add(new ValidationMessage(TranslationTable.cck_triangle_required,
                    ValidationMessage.MessageType.Error));
            }
            else if (triangleCount > maxTrianglesCount)
            {
                validationMessages.Add(CreateCountValidationMessage("Triangle", triangleCount, maxTrianglesCount));
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateTotalMeshNode(GltfContainer gltfContainer,
            int maxMeshCount)
        {
            var gltf = gltfContainer.Gltf;
            var meshNodes = gltf.Nodes.Count(node => node.Mesh.HasValue);

            if (meshNodes <= maxMeshCount)
            {
                return Enumerable.Empty<ValidationMessage>();
            }

            return new[]
            {
                CreateCountValidationMessage("Mesh Renderer", meshNodes, maxMeshCount)
            };
        }

        internal static IEnumerable<ValidationMessage> ValidateMaterial(GltfContainer gltfContainer,
            int maxMaterialsCount)
        {
            var validationMessages = new List<ValidationMessage>();
            var gltf = gltfContainer.Gltf;

            if (gltf.Materials == null)
            {
                return validationMessages;
            }
            if (gltf.Materials.Count > maxMaterialsCount)
            {
                validationMessages.Add(
                    CreateCountValidationMessage("Material", gltf.Materials.Count, maxMaterialsCount));
            }

            return validationMessages;
        }

        internal static IEnumerable<ValidationMessage> ValidateTexture(GltfContainer gltfContainer,
            int maxTexturesCount)
        {
            var validationMessages = new List<ValidationMessage>();

            var gltf = gltfContainer.Gltf;
            var resourcesStore = new ResourcesStore(gltfContainer, new ResourceLoaderFromEmbedOnly());

            if (gltf.Textures == null)
            {
                return validationMessages;
            }

            if (gltf.Textures.Count > maxTexturesCount)
            {
                validationMessages.Add(CreateCountValidationMessage("Texture", gltf.Textures.Count, maxTexturesCount));
            }

            foreach (var texture in gltf.Textures)
            {
                if (texture.Source == null)
                {
                    continue;
                }

                var imageIndex = texture.Source.Value;
                var image = gltf.Images[imageIndex];
                if (image.MimeType != Image.MimeTypeImageJpeg && image.MimeType != Image.MimeTypeImagePng)
                {
                    validationMessages.Add(new ValidationMessage(
                        TranslationUtility.GetMessage(TranslationTable.cck_image_unsupported_format, image.Name, image.MimeType, Image.MimeTypeImagePng, Image.MimeTypeImageJpeg),
                        ValidationMessage.MessageType.Error));
                }

                if (image.Uri != null)
                {
                    validationMessages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_image_uri_not_allowed, image.Name),
                        ValidationMessage.MessageType.Error));
                }

                var textureSize = LoadTextureSize(resourcesStore, imageIndex);
                if (textureSize.x > MaxTextureSize || textureSize.y > MaxTextureSize)
                {
                    validationMessages.Add(new ValidationMessage(
                        TranslationUtility.GetMessage(TranslationTable.cck_image_resolution_limit, image.Name, textureSize.x, textureSize.y, MaxTextureSize, MaxTextureSize),
                        ValidationMessage.MessageType.Error));
                }
            }

            return validationMessages;
        }

        static bool IsSkinnedMesh(VGltf.Types.Mesh gltfMesh)
        {
            if (gltfMesh.Primitives.Count == 0)
            {
                return false;
            }
            var primaryAttributes = gltfMesh.Primitives[0].Attributes;
            return primaryAttributes.ContainsKey(VGltf.Types.Mesh.PrimitiveType.AttributeName.WEIGHTS_0) && primaryAttributes.ContainsKey(VGltf.Types.Mesh.PrimitiveType.AttributeName.JOINTS_0);
        }

        static int LoadTrianglesCount(ResourcesStore resourcesStore, int indicesIndex)
        {
            var gltf = resourcesStore.Gltf;

            var buf = resourcesStore.GetOrLoadTypedBufferByAccessorIndex(indicesIndex);
            var accessor = gltf.Accessors[indicesIndex];
            if (accessor.Type != Accessor.TypeEnum.Scalar ||
                accessor.ComponentType == Accessor.ComponentTypeEnum.FLOAT)
            {
                return 0;
            }
            return buf.GetPrimitivesAsInt().ToArray().Length / 3;
        }

        static Vector2Int LoadTextureSize(ResourcesStore resourcesStore, int imageIndex)
        {
            Vector2Int textureSize;
            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            try
            {
                var gltfImgResource = resourcesStore.GetOrLoadImageResourceAt(imageIndex);

                var imageBuffer = new byte[gltfImgResource.Data.Count];
                if (gltfImgResource.Data.Array != null)
                {
                    Array.Copy(gltfImgResource.Data.Array, gltfImgResource.Data.Offset, imageBuffer, 0,
                        gltfImgResource.Data.Count);
                }
                texture.LoadImage(imageBuffer);
                textureSize = new Vector2Int(texture.height, texture.width);
            }
            finally
            {
                Object.DestroyImmediate(texture);
            }

            return textureSize;
        }

        static ValidationMessage CreateCountValidationMessage(string targetName, int count, int maxCount)
        {
            return new ValidationMessage(
                TranslationUtility.GetMessage(TranslationTable.cck_count_limit_exceeded, targetName, maxCount, count)
                , ValidationMessage.MessageType.Error);
        }
    }
}
