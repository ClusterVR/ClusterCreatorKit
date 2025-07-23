using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Repository;
using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public interface IGltfValidator
    {

        public IEnumerable<ValidationMessage> Validate(GameObject gameObject);

        public IEnumerable<ValidationMessage> Validate(GltfContainer gltfContainer);
    }
}
