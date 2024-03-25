using System.Diagnostics;
using ClusterVR.CreatorKit.Validator;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [Conditional("UNITY_EDITOR")]
    public sealed class RequireIsTriggerSettingsOfShapeAttribute : RequireIsTriggerSettingsAttributeBase
    {
        protected override bool IsTrigger(Behaviour target) => ((BaseShape) target).IsTrigger;
        protected override bool UseChildren(Behaviour target) => false;
    }
}
