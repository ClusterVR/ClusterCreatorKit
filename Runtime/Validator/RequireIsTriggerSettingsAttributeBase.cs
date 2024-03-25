using System.Diagnostics;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ClusterVR.CreatorKit.Validator
{
    [Conditional("UNITY_EDITOR")]
    public abstract class RequireIsTriggerSettingsAttributeBase : ComponentValidatorAttribute
    {
        protected abstract bool IsTrigger(Behaviour target);
        protected abstract bool UseChildren(Behaviour target);

#if UNITY_EDITOR
        public override bool Validate(Behaviour target, out MessageType type, out string message)
        {
            if (IsTrigger(target))
            {
                var meshColliders = UseChildren(target) ? target.GetComponentsInChildren<MeshCollider>(true) : target.GetComponents<MeshCollider>();
                if (meshColliders.Any(collider => !collider.convex))
                {
                    type = MessageType.Error;
                    message = $"{target.GetType().Name}でMesh Colliderを利用する際はConvexとis TriggerをTrueにしないと動作しません。";
                    return false;
                }
            }
            type = MessageType.None;
            message = "";
            return true;
        }
#endif
    }
}
