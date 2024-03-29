using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Collider)), DisallowMultipleComponent, RequireIsTriggerSettingsOfShapeAttribute]
    public abstract class BaseShape : MonoBehaviour
    {
        public abstract bool IsTrigger { get; }

        void Awake()
        {
            SetCollidersIsTrigger();
        }

        void OnValidate()
        {
            SetCollidersIsTrigger();
        }

        void SetCollidersIsTrigger()
        {
            var colliders = GetComponents<Collider>();
            foreach (var c in colliders)
            {
                c.isTrigger = IsTrigger;
            }
        }
    }
}
