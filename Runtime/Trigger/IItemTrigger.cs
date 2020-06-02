using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger
{
    public interface IItemTrigger
    {
        IItem Item { get; }
        event TriggerEventHandler TriggerEvent;
    }

    public delegate void TriggerEventHandler(object sender, TriggerEventArgs e);

    public class TriggerEventArgs : EventArgs
    {
        public ItemTriggerTarget Target { get; }
        public IItem SpecifiedTargetItem { get; }
        public GameObject CollidedObject { get; }
        public string Key { get; }
        public ParameterType Type { get; }
        public TriggerValue Value { get; }

        public TriggerEventArgs(ItemTriggerTarget target, IItem specifiedTargetItem, GameObject collidedObject, string key, ParameterType type, TriggerValue value)
        {
            Target = target;
            SpecifiedTargetItem = specifiedTargetItem;
            CollidedObject = collidedObject;
            Key = key;
            Type = type;
            Value = value;
        }
    }
}