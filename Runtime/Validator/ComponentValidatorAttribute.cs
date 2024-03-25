using System;
using System.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ClusterVR.CreatorKit.Validator
{
    [Conditional("UNITY_EDITOR")]
    public abstract class ComponentValidatorAttribute : Attribute
    {
#if UNITY_EDITOR
        public abstract bool Validate(Behaviour target, out MessageType type, out string message);
#endif
    }
}
