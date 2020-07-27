using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    public class TriggerParamAttribute : PropertyAttribute
    {
        public TriggerTarget[] TargetSelectables { get; }

        protected TriggerParamAttribute(params TriggerTarget[] targetSelectables)
        {
            TargetSelectables = targetSelectables;
        }

        public virtual string FormatTarget(TriggerTarget target)
        {
            return target.ToString();
        }
    }

    public class ItemTriggerParamAttribute : TriggerParamAttribute
    {
        protected ItemTriggerParamAttribute(params TriggerTarget[] targetSelectables)
            : base(targetSelectables)
        {
        }

        public ItemTriggerParamAttribute()
            : this(TriggerTarget.Item, TriggerTarget.SpecifiedItem, TriggerTarget.Player, TriggerTarget.Global)
        {
        }

        public override string FormatTarget(TriggerTarget target)
        {
            switch (target)
            {
                case TriggerTarget.Item:
                    return "This";
                case TriggerTarget.Player:
                    return "Owner";
                default:
                    return target.ToString();
            }
        }
    }

    public class CollideItemTriggerParamAttribute : ItemTriggerParamAttribute
    {
        public CollideItemTriggerParamAttribute()
            : base(TriggerTarget.Item, TriggerTarget.SpecifiedItem, TriggerTarget.Player, TriggerTarget.CollidedItemOrPlayer, TriggerTarget.Global)
        {
        }
    }

    public class ItemOperationTriggerParamAttribute : ItemTriggerParamAttribute
    {
        public ItemOperationTriggerParamAttribute()
            : base(TriggerTarget.Item)
        {
        }
    }

    public class PlayerOperationTriggerParamAttribute : TriggerParamAttribute
    {
        public PlayerOperationTriggerParamAttribute()
            : base(TriggerTarget.Player)
        {
        }
    }

    public class GlobalOperationTriggerParamAttribute : TriggerParamAttribute
    {
        public GlobalOperationTriggerParamAttribute()
            : base(TriggerTarget.Global)
        {
        }
    }
}