using System;
using System.Collections.Generic;
using UnityEngine;
using VGltf;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class UploadingItem : IDisposable
    {
        public readonly GameObject Item;

        public readonly bool IsValid;

        public readonly string Name;
        public readonly Vector3Int Size;

        public readonly GltfContainer Gltf;
        public readonly Texture2D Thumbnail;

        public readonly List<ValidationMessage> ValidationMessages;

        public UploadingItem(GameObject item, bool isValid, string name, Vector3Int size, GltfContainer gltf, Texture2D thumbnail, List<ValidationMessage> validationMessages)
        {
            Item = item;
            IsValid = isValid;
            Name = name;
            Size = size;
            Gltf = gltf;
            Thumbnail = thumbnail;
            ValidationMessages = validationMessages;
        }

        public void Dispose()
        {
            Object.DestroyImmediate(Thumbnail);
        }
    }
}
