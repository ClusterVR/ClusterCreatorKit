using UnityEngine;
using VGltf;

namespace ClusterVR.CreatorKit.ItemExporter
{
    public interface IItemExporter
    {
        public GltfContainer ExportAsGltfContainer(GameObject go);
    }
}
