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

    public class ItemTimerTriggerParamAttribute : ItemTriggerParamAttribute
    {
        public ItemTimerTriggerParamAttribute()
            : base(TriggerTarget.Item)
        {
        }
    }

    public class ItemTriggerLotteryTriggerParamAttribute : ItemTriggerParamAttribute
    {
        public ItemTriggerLotteryTriggerParamAttribute()
            : base(TriggerTarget.Item, TriggerTarget.Player)
        {
        }
    }

    public class PlayerTriggerParamAttribute : TriggerParamAttribute
    {
        protected PlayerTriggerParamAttribute(params TriggerTarget[] targetSelectables)
            : base(targetSelectables)
        {
        }

        public PlayerTriggerParamAttribute()
            : base(TriggerTarget.Player, TriggerTarget.SpecifiedItem, TriggerTarget.Global)
        {
        }
    }

    public class PlayerOperationTriggerParamAttribute : PlayerTriggerParamAttribute
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