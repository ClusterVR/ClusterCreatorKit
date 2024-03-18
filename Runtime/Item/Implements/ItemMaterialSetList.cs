
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public class ItemMaterialSetList : MonoBehaviour, IItemMaterialSetList, IIdContainer
    {
        [SerializeField] ItemMaterialSet[] itemMaterialSets;

        public IEnumerable<ItemMaterialSet> ItemMaterialSets => itemMaterialSets;
        IEnumerable<string> IIdContainer.Ids => itemMaterialSets.Select(s => s.Id);

        public void Construct(ItemMaterialSet[] itemMaterialSets)
        {
            this.itemMaterialSets = itemMaterialSets;
        }
    }
}
