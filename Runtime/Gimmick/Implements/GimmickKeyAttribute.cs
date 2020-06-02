using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public class GimmickKeyAttribute : PropertyAttribute
    {
        public Target[] TargetSelectables { get; }

        public GimmickKeyAttribute(params Target[] targetSelectables)
        {
            TargetSelectables = targetSelectables;
        }
    }

    public class ItemGimmickKeyAttribute : GimmickKeyAttribute
    {
        public ItemGimmickKeyAttribute()
            : base(Target.Item, Target.Global)
        {
        }
    }

    public class PlayerGimmickKeyAttribute : GimmickKeyAttribute
    {
        public PlayerGimmickKeyAttribute()
            : base(Target.Player, Target.Global)
        {
        }
    }
}