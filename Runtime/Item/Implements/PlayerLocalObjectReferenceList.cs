using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class PlayerLocalObjectReferenceList : MonoBehaviour, IPlayerLocalObjectReferenceList, IIdContainer
    {
        [SerializeField] PlayerLocalObjectReferenceListEntry[] playerLocalObjectReferences = { };

        public IReadOnlyCollection<IPlayerLocalObjectReferenceListEntry> PlayerLocalObjectReferences => playerLocalObjectReferences;
        IEnumerable<string> IIdContainer.Ids => playerLocalObjectReferences.Select(a => a.Id);
    }
}
