using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Common;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [Serializable]
    public sealed class HumanoidAnimation : ScriptableObject, IHumanoidAnimation
    {
        [SerializeField] float length;
        [SerializeField] bool isLoop;
        [SerializeField] List<HumanoidAnimationCurve> curves;

        public float Length => length;
        public bool IsLoop => isLoop;
        public IReadOnlyList<IHumanoidAnimationCurve> Curves => curves;

        public void Construct(float length, bool isLoop, List<HumanoidAnimationCurve> curves)
        {
            this.length = length;
            this.isLoop = isLoop;
            this.curves = curves;
        }

        public PartialHumanPose Sample(float time)
        {
            if (curves == null)
            {
                return default;
            }

            var sampleTime = isLoop ? Mathf.Repeat(time, length) : Mathf.Clamp(time, 0f, length);

            Vector3? rootPosition = null;
            Quaternion? rootRotation = null;
            var muscles = new float?[HumanTrait.MuscleCount];
            foreach (var curve in curves)
            {
                var value = curve.Curve.Evaluate(sampleTime);
                switch (curve.PropertyName)
                {
                    case HumanoidAnimationCurvePropertyName.CenterTx:
                        ApplyPosition(ref rootPosition, value, 0);
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterTy:
                        ApplyPosition(ref rootPosition, value, 1);
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterTz:
                        ApplyPosition(ref rootPosition, value, 2);
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterQx:
                        ApplyRotation(ref rootRotation, value, 0);
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterQy:
                        ApplyRotation(ref rootRotation, value, 1);
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterQz:
                        ApplyRotation(ref rootRotation, value, 2);
                        break;
                    case HumanoidAnimationCurvePropertyName.CenterQw:
                        ApplyRotation(ref rootRotation, value, 3);
                        break;
                    default:
                        muscles[(int) curve.PropertyName] = value;
                        break;
                }
            }
            return new PartialHumanPose(rootPosition, rootRotation, muscles);
        }

        void ApplyPosition(ref Vector3? position, float value, int propertyIndex)
        {
            var result = position ?? new Vector3(0f, 0f, 0f);
            result[propertyIndex] = value;
            position = result;
        }

        void ApplyRotation(ref Quaternion? rotation, float value, int propertyIndex)
        {
            var result = rotation ?? new Quaternion(0f, 0f, 0f, 0f);
            result[propertyIndex] = value;
            rotation = result;
        }
    }
}
