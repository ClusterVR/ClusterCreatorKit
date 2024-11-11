using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    [Serializable]
    public struct AttachTarget
    {
        [SerializeField, IdString] string id;
        [SerializeField] Transform node;

        public string Id => id;
        public Transform Node => node;

        public void Construct(string id, Transform node)
        {
            this.id = id;
            this.node = node;
        }
    }
}
