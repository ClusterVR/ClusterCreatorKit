using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [Serializable]
    public sealed class HumanoidAnimationCurve : IHumanoidAnimationCurve
    {
        [SerializeField] HumanoidAnimationCurvePropertyName propertyName;
        [SerializeField] AnimationCurve curve;

        public HumanoidAnimationCurvePropertyName PropertyName => propertyName;
        public AnimationCurve Curve => curve;

        public HumanoidAnimationCurve(HumanoidAnimationCurvePropertyName propertyName, AnimationCurve curve)
        {
            this.propertyName = propertyName;
            this.curve = curve;
        }
    }
}
