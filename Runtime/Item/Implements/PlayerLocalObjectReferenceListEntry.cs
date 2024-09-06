using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [Serializable]
    public sealed class PlayerLocalObjectReferenceListEntry : IPlayerLocalObjectReferenceListEntry
    {
        [SerializeField] string id;
        [SerializeField] GameObject targetObject;

        public string Id => id;
        public GameObject GameObject => targetObject;
    }
}
