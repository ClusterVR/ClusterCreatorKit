using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ItemTemplate
{
    [Serializable]
    public sealed class ProductUgcRelatedToItemTemplate
    {
        [SerializeField] bool canSeeDetail;
        [SerializeField] string name;
        [SerializeField] User.User ownerUser;
        [SerializeField] string productUgcId;

        public bool CanSeeDetail => canSeeDetail;
        public string Name => name;
        public User.User OwnerUser => ownerUser;
        public string ProductUgcId => productUgcId;
    }
}
