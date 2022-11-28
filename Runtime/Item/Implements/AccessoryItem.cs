using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class AccessoryItem : MonoBehaviour, IAccessoryItem
    {
        [SerializeField] AccessoryAttachableBoneName defaultAttachBoneName = AccessoryAttachableBoneName.Head;
        [SerializeField] Vector3 defaultAttachOffsetPosition;
        [SerializeField] Quaternion defaultAttachOffsetRotation = Quaternion.identity;

        Item item;

        public IItem Item
        {
            get
            {
                if (item != null)
                {
                    return item;
                }
                if (this == null)
                {
                    return null;
                }
                return item = GetComponent<Item>();
            }
        }

        public AccessoryAttachableBoneName DefaultAttachBoneName => defaultAttachBoneName;
        public Vector3 DefaultAttachOffsetPosition => defaultAttachOffsetPosition;
        public Quaternion DefaultAttachOffsetRotation => defaultAttachOffsetRotation;

#if UNITY_EDITOR
        public void UpdateDefaultAttachOffsetPosition(Vector3 position)
        {
            defaultAttachOffsetPosition = position;
        }

        public void UpdateDefaultAttachOffsetRotation(Quaternion rotation)
        {
            defaultAttachOffsetRotation = rotation;
        }
#endif
    }
}
