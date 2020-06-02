using System;
using UnityEngine;

namespace ClusterVR.CreatorKit
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class RequiredObjectAttribute : PropertyAttribute
    {
        public abstract Type PropertyType { get; }
    }

    public sealed class RequiredTransform : RequiredObjectAttribute
    {
        public override Type PropertyType => typeof(Transform);
    }
}