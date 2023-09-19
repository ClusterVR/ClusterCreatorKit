using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ItemTemplate
{
    [Serializable]
    public sealed class OwnItemTemplate
    {
        [SerializeField] bool canCreate;
        [SerializeField] bool canHide;
        [SerializeField] string createdAt;
        [SerializeField] bool isBeta;
        [SerializeField] bool isProductized;
        [SerializeField] string itemTemplateId;
        [SerializeField] string name;
        [SerializeField] string productUgcId;
        [SerializeField] string slotIconUrl;
        [SerializeField] ProductUgcRelatedToItemTemplate storeProductUgc;
        [SerializeField] string url;

        public bool CanCreate => canCreate;
        public bool CanHide => canHide;
        public string CreateAt => createdAt;
        public bool IsBeta => isBeta;
        public bool IsProductized => isProductized;
        public string ItemTemplateId => itemTemplateId;
        public string Name => name;
        public string ProductUgcId => productUgcId;
        public string SlotIconUrl => slotIconUrl;
        public ProductUgcRelatedToItemTemplate StoreProductUgc => storeProductUgc;
        public string Url => url;
    }
}
