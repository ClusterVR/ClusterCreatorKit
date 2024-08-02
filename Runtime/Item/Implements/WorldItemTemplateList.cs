using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item))]
    [DisallowMultipleComponent]
    public sealed class WorldItemTemplateList : MonoBehaviour, IWorldItemTemplateList, IIdContainer, IItemTemplateContainer
    {
        [SerializeField] WorldItemTemplateListEntry[] worldItemTemplates;

        IEnumerable<IWorldItemTemplateListEntry> IWorldItemTemplateList.WorldItemTemplates =>
            ValidWorldItemTemplates();
        IEnumerable<string> IIdContainer.Ids => ValidWorldItemTemplates().Select(t => t.Id);

        public void Construct(WorldItemTemplateListEntry[] worldItemTemplates)
        {
            this.worldItemTemplates = worldItemTemplates;
        }

        public IEnumerable<ItemTemplateIdAndItem> ItemTemplates()
        {
            return ValidWorldItemTemplates().Select(x => new ItemTemplateIdAndItem(x.ItemTemplateId, x.WorldItemTemplate));
        }

        IEnumerable<WorldItemTemplateListEntry> ValidWorldItemTemplates()
        {
            if (worldItemTemplates == null)
            {
                return Enumerable.Empty<WorldItemTemplateListEntry>();
            }

            return worldItemTemplates.Where(x => x.WorldItemTemplate != null && x.WorldItemTemplate.gameObject != null);
        }
#if UNITY_EDITOR
        public void SetItemTemplateId(IItem item, ItemTemplateId id)
        {
            foreach (var entry in worldItemTemplates)
            {
                if (entry.WorldItemTemplate != null && entry.WorldItemTemplate.Equals(item))
                {
                    entry.ItemTemplateId = id;
                }
            }
        }

        public void MarkObjectDirty()
        {
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
