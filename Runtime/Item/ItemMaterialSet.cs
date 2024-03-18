using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    [Serializable]
    public struct ItemMaterialSet
    {
        [SerializeField, IdString] string id;
        [SerializeField] Material material;

        public string Id => id;
        public Material Material => material;

        public ItemMaterialSet(string id, Material material)
        {
            this.id = id;
            this.material = material;
        }
    }
}
