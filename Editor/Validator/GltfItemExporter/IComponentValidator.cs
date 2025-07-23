using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Repository;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public interface IComponentValidator
    {
        public IEnumerable<ValidationMessage> Validate(GameObject gameObject, bool isBeta);
    }
}
