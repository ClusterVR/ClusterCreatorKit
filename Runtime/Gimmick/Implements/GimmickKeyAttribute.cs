using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    public class GimmickKeyAttribute : PropertyAttribute
    {
        public GimmickTarget[] TargetSelectables { get; }

        public GimmickKeyAttribute(params GimmickTarget[] targetSelectables)
        {
            TargetSelectables = targetSelectables;
        }
    }

    public class ItemGimmickKeyAttribute : GimmickKeyAttribute
    {
        public ItemGimmickKeyAttribute()
            : base(GimmickTarget.Item, GimmickTarget.Global)
        {
        }
    }
}