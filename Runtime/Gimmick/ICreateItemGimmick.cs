using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface ICreateItemGimmick
    {
        IItem ItemTemplate { get; }
        ItemTemplateId ItemTemplateId { get; }
        event CreateItemEventHandler OnCreateItem;
    }

    public delegate void CreateItemEventHandler(CreateItemEventArgs args);

    public sealed class CreateItemEventArgs : EventArgs
    {
        public ItemId SenderId;
        public ItemTemplateId TemplateId;
        public Vector3 Position;
        public Quaternion Rotation;
    }

    public static class ICreateItemGimmickExtensions
    {
        public static bool IsValid(this ICreateItemGimmick createItemGimmick)
        {
            return createItemGimmick.ItemTemplate != null && createItemGimmick.ItemTemplate.gameObject != null;
        }
    }
}
