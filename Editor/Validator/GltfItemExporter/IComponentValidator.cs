using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public interface IComponentValidator
    {
        public IEnumerable<ValidationMessage> Validate(GameObject gameObject);
    }
}
