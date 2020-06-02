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
    public class CreateItemEventArgs : EventArgs
    {
        public ItemId SenderId;
        public ItemTemplateId TemplateId;
        public Vector3 Position;
        public Quaternion Rotation;
    }
}