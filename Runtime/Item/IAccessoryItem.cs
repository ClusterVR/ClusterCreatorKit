using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public interface IAccessoryItem
    {
        IItem Item { get; }

        Vector3 DefaultAttachOffsetPosition { get; }
        Quaternion DefaultAttachOffsetRotation { get; }
        AccessoryAttachableBoneName DefaultAttachBoneName { get; }
    }
}
