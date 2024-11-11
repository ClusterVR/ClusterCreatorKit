using UnityEngine;

namespace ClusterVR.CreatorKit.Extensions
{
    public static class TransformExtensions
    {
        public static bool IsDecendantOrSelf(this Transform target, Transform mayParent)
        {
            if (target == mayParent) return true;
            var parent = target.parent;
            if (parent == null) return false;
            return IsDecendantOrSelf(parent, mayParent);
        }
    }
}
