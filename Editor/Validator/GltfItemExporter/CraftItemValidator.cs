using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Repository;
using UnityEngine;
using VGltf;
using VGltf.Types;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public sealed class CraftItemValidator : IGltfValidator
    {
        const int MaxTrianglesCount = 5000;
        const int MaxMaterialsCount = 8;
        const int MaxTexturesCount = 12;

        public IEnumerable<ValidationMessage> Validate(GameObject gameObject)
        {
            var validationMessages = new List<ValidationMessage>();
            validationMessages.AddRange(GltfValidator.ValidateName(gameObject));
            return validationMessages;
        }

        public IEnumerable<ValidationMessage> Validate(GltfContainer gltfContainer)
        {
            var validationMessages = new List<ValidationMessage>();

            validationMessages.AddRange(GltfValidator.ValidateScene(gltfContainer));
            validationMessages.AddRange(GltfValidator.ValidateNode(gltfContainer));
            validationMessages.AddRange(GltfValidator.ValidateMesh(gltfContainer, MaxTrianglesCount));
            validationMessages.AddRange(GltfValidator.ValidateMaterial(gltfContainer, MaxMaterialsCount));
            validationMessages.AddRange(GltfValidator.ValidateTexture(gltfContainer, MaxTexturesCount));

            return validationMessages;
        }
    }
}
