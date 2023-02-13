using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class ItemAudioSetList : MonoBehaviour, IItemAudioSetList, IIdContainer
    {
        [SerializeField] ItemAudioSet[] itemAudioSets;

        public IEnumerable<ItemAudioSet> ItemAudioSets => itemAudioSets;
        IEnumerable<string> IIdContainer.Ids => itemAudioSets.Select(s => s.Id);

        public void Construct(ItemAudioSet[] itemAudioSets)
        {
            this.itemAudioSets = itemAudioSets;
        }
    }
}
