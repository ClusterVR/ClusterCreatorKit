using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    public class ItemTriggerAttribute : PropertyAttribute
    {
        public ItemTriggerTarget[] TargetSelectables { get; }

        protected ItemTriggerAttribute(params ItemTriggerTarget[] targetSelectables)
        {
            TargetSelectables = targetSelectables;
        }

        public ItemTriggerAttribute()
            : this(ItemTriggerTarget.This, ItemTriggerTarget.SpecifiedItem, ItemTriggerTarget.Owner, ItemTriggerTarget.Global)
        {
        }
    }

    public class CollideItemTriggerAttribute : ItemTriggerAttribute
    {
        public CollideItemTriggerAttribute()
            : base(ItemTriggerTarget.This, ItemTriggerTarget.SpecifiedItem, ItemTriggerTarget.Owner, ItemTriggerTarget.CollidedItemOrPlayer, ItemTriggerTarget.Global)
        {
        }
    }
}