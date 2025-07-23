using System;
using ClusterVR.CreatorKit.Editor.Repository;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class ItemViewModel
    {
        public UploadingItem UploadingItem;

        public event Action<ItemViewModel> OnRemoveButtonClicked;

        internal void InvokeOnRemoveButtonClicked(ItemViewModel itemViewModel)
        {
            OnRemoveButtonClicked?.Invoke(itemViewModel);
        }

        public GameObject Item => UploadingItem?.Item;

        public bool IsValid => UploadingItem?.IsValid ?? false;

        public string Name => UploadingItem?.Name;
        public Vector3Int Size => UploadingItem?.Size ?? default;

        public void SetUploadingItem(UploadingItem uploadingItem)
        {
            UploadingItem = null;
            UploadingItem = uploadingItem;
        }
    }
}
