using System.Collections.Generic;
using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public sealed class AccessoryValidator : IGltfValidator
    {
        const int MaxTrianglesCount = 2000;
        const int MaxTotalMeshCount = 8;

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
            validationMessages.AddRange(GltfValidator.ValidateMesh(gltfContainer, MaxTrianglesCount)); // 8Mesh, 2subMeshまで、skinnedMesh不可、TRIANGLESのみ
            validationMessages.AddRange(GltfValidator.ValidateTotalMeshNode(gltfContainer, MaxTotalMeshCount)); // のべmesh数8まで
            validationMessages.AddRange(GltfValidator.ValidateMaterial(gltfContainer)); // 2マテリアルまで
            validationMessages.AddRange(GltfValidator.ValidateTexture(gltfContainer)); // 3tex、8192pxまで

            return validationMessages;
        }
    }
}
