using System;
using ClusterVR.CreatorKit.Extensions;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [Serializable]
    public sealed class WorldItemTemplateListEntry : IWorldItemTemplateListEntry
    {
        [SerializeField] string id;
        [SerializeField, WorldItemTemplateOnly] Item worldItemTemplate;
        [SerializeField, HideInInspector] ItemTemplateId itemTemplateId;

        public string Id => id;

        public IItem WorldItemTemplate => worldItemTemplate;

        public ItemTemplateId ItemTemplateId
        {
            get => itemTemplateId;
#if UNITY_EDITOR
            set => itemTemplateId = value;
#endif
        }

        public WorldItemTemplateListEntry(string id, Item worldItemTemplate, ItemTemplateId itemTemplateId)
        {
            this.id = id;
            this.worldItemTemplate = worldItemTemplate;
            this.itemTemplateId = itemTemplateId;
        }
    }
}
