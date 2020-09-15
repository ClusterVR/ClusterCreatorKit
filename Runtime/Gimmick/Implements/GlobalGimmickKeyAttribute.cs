using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public abstract class GlobalGimmickKeyAttribute : PropertyAttribute
    {
        public abstract List<GimmickTarget> TargetSelectables { get; }

        public virtual string FormatTarget(GimmickTarget target)
        {
            return target.ToString();
        }
    }

    public class ConsistentlySyncGlobalGimmickKeyAttribute : GlobalGimmickKeyAttribute
    {
        public override List<GimmickTarget> TargetSelectables => new List<GimmickTarget> {GimmickTarget.Global, GimmickTarget.Item};
    }

    public class LocalizableGlobalGimmickKeyAttribute : GlobalGimmickKeyAttribute
    {
        public override List<GimmickTarget> TargetSelectables => new List<GimmickTarget> {GimmickTarget.Global, GimmickTarget.Item};

        public override string FormatTarget(GimmickTarget target)
        {
            switch (target)
            {
                case GimmickTarget.Player:
                    return "LocalPlayer";
                default:
                    return target.ToString();
            }
        }
    }
}