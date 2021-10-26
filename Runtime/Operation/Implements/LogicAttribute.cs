using System.Collections.Generic;
using ClusterVR.CreatorKit.Gimmick;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    public abstract class LogicAttribute : PropertyAttribute
    {
        public abstract List<TargetStateTarget> TargetStateTargetSelectables { get; }
        public abstract List<GimmickTarget> SourceStateTargetSelectables { get; }

        public virtual string FormatTargetStateTarget(TargetStateTarget target)
        {
            return target.ToString();
        }

        public virtual string FormatSourceTarget(GimmickTarget target)
        {
            return target.ToString();
        }
    }

    public sealed class ItemLogicAttribute : LogicAttribute
    {
        public override List<TargetStateTarget> TargetStateTargetSelectables => new List<TargetStateTarget>
            { TargetStateTarget.Item, TargetStateTarget.Player };

        public override List<GimmickTarget> SourceStateTargetSelectables => new List<GimmickTarget>
            { GimmickTarget.Item, GimmickTarget.Player, GimmickTarget.Global };

        public override string FormatTargetStateTarget(TargetStateTarget target)
        {
            switch (target)
            {
                case TargetStateTarget.Item:
                    return "This";
                case TargetStateTarget.Player:
                    return "Owner";
                default:
                    return target.ToString();
            }
        }

        public override string FormatSourceTarget(GimmickTarget target)
        {
            switch (target)
            {
                case GimmickTarget.Item:
                    return "This";
                case GimmickTarget.Player:
                    return "Owner";
                default:
                    return target.ToString();
            }
        }
    }

    public sealed class PlayerLogicAttribute : LogicAttribute
    {
        public override List<TargetStateTarget> TargetStateTargetSelectables =>
            new List<TargetStateTarget> { TargetStateTarget.Player };

        public override List<GimmickTarget> SourceStateTargetSelectables => new List<GimmickTarget>
            { GimmickTarget.Player, GimmickTarget.Global };
    }

    public sealed class GlobalLogicAttribute : LogicAttribute
    {
        public override List<TargetStateTarget> TargetStateTargetSelectables =>
            new List<TargetStateTarget> { TargetStateTarget.Global };

        public override List<GimmickTarget> SourceStateTargetSelectables =>
            new List<GimmickTarget> { GimmickTarget.Global };
    }
}
