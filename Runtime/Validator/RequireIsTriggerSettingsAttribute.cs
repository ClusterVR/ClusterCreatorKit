using System.Diagnostics;
using UnityEngine;

namespace ClusterVR.CreatorKit.Validator
{
    [Conditional("UNITY_EDITOR")]
    public sealed class RequireIsTriggerSettingsAttribute : RequireIsTriggerSettingsAttributeBase
    {
        readonly bool isTrigger;
        readonly bool useChildren;

        public RequireIsTriggerSettingsAttribute(bool isTrigger = true, bool useChildren = true)
        {
            this.isTrigger = isTrigger;
            this.useChildren = useChildren;
        }

        protected override bool IsTrigger(Behaviour target) => isTrigger;
        protected override bool UseChildren(Behaviour target) => useChildren;
    }
}
