using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class WorldItemReferenceList : MonoBehaviour, IWorldItemReferenceList, IIdContainer
    {
        [SerializeField] WorldItemReferenceListEntry[] worldItemReferences = {};

        public IReadOnlyCollection<IWorldItemReferenceListEntry> WorldItemReferences => worldItemReferences;
        IEnumerable<string> IIdContainer.Ids => worldItemReferences.Select(a => a.Id);
    }
}
