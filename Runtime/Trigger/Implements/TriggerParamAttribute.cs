using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    public abstract class TriggerParamAttribute : PropertyAttribute
    {
        public TriggerTarget[] TargetSelectables { get; }
        public virtual string ValueLabelText => "Value";

        protected TriggerParamAttribute(params TriggerTarget[] targetSelectables)
        {
            TargetSelectables = targetSelectables;
        }

        public virtual string FormatTarget(TriggerTarget target)
        {
            return target.ToString();
        }

        protected static string FormatTargetForItemTrigger(TriggerTarget target)
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

    public class ConstantTriggerParamAttribute : TriggerParamAttribute
    {
        protected ConstantTriggerParamAttribute(params TriggerTarget[] targetSelectables)
            : base(targetSelectables)
        {
        }
    }

    public class ItemConstantTriggerParamAttribute : ConstantTriggerParamAttribute
    {
        protected ItemConstantTriggerParamAttribute(params TriggerTarget[] targetSelectables)
            : base(targetSelectables)
        {
        }

        public ItemConstantTriggerParamAttribute()
            : this(TriggerTarget.Item, TriggerTarget.SpecifiedItem, TriggerTarget.Player, TriggerTarget.Global)
        {
        }

        public override string FormatTarget(TriggerTarget target)
        {
            return FormatTargetForItemTrigger(target);
        }
    }

    public sealed class CollideItemTriggerParamAttribute : ItemConstantTriggerParamAttribute
    {
        public CollideItemTriggerParamAttribute()
            : base(TriggerTarget.Item, TriggerTarget.SpecifiedItem, TriggerTarget.Player,
                TriggerTarget.CollidedItemOrPlayer, TriggerTarget.Global)
        {
        }
    }

    public sealed class ItemTimerTriggerParamAttribute : ItemConstantTriggerParamAttribute
    {
        public ItemTimerTriggerParamAttribute()
            : base(TriggerTarget.Item)
        {
        }
    }

    public sealed class ItemTriggerLotteryTriggerParamAttribute : ItemConstantTriggerParamAttribute
    {
        public ItemTriggerLotteryTriggerParamAttribute()
            : base(TriggerTarget.Item, TriggerTarget.Player)
        {
        }
    }

    public class PlayerConstantTriggerParamAttribute : ConstantTriggerParamAttribute
    {
        protected PlayerConstantTriggerParamAttribute(params TriggerTarget[] targetSelectables)
            : base(targetSelectables)
        {
        }

        public PlayerConstantTriggerParamAttribute()
            : base(TriggerTarget.Player, TriggerTarget.SpecifiedItem, TriggerTarget.Global)
        {
        }
    }

    public sealed class PlayerOperationTriggerParamAttribute : PlayerConstantTriggerParamAttribute
    {
        public PlayerOperationTriggerParamAttribute()
            : base(TriggerTarget.Player)
        {
        }
    }

    public sealed class InitializePlayerTriggerParamAttribute : PlayerConstantTriggerParamAttribute
    {
        public override string ValueLabelText => "Initial Value";

        public InitializePlayerTriggerParamAttribute()
            : base(TriggerTarget.Player)
        {
        }
    }

    public sealed class GlobalOperationTriggerParamAttribute : ConstantTriggerParamAttribute
    {
        public GlobalOperationTriggerParamAttribute()
            : base(TriggerTarget.Global)
        {
        }
    }

    public class VariableTriggerParamAttribute : TriggerParamAttribute
    {
        public ParameterType InputParameterType { get; }

        protected VariableTriggerParamAttribute(ParameterType inputParameterType, params TriggerTarget[] targetSelectables)
            : base(targetSelectables)
        {
            InputParameterType = inputParameterType;
        }
    }

    public class ItemVariableTriggerParamAttribute : VariableTriggerParamAttribute
    {
        protected ItemVariableTriggerParamAttribute(ParameterType inputParameterType, params TriggerTarget[] targetSelectables)
            : base(inputParameterType, targetSelectables)
        {
        }

        public ItemVariableTriggerParamAttribute(ParameterType inputParameterType)
            : this(inputParameterType, TriggerTarget.Item, TriggerTarget.SpecifiedItem, TriggerTarget.Player, TriggerTarget.Global)
        {
        }

        public override string FormatTarget(TriggerTarget target)
        {
            return FormatTargetForItemTrigger(target);
        }
    }
}
