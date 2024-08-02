using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [Serializable]
    public sealed class WorldItemReferenceListEntry : IWorldItemReferenceListEntry
    {
        [SerializeField] string id;
        [SerializeField] Item item;

        public string Id => id;
        public IItem Item => item;
    }
}
