using System;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class LocalizableGlobalGimmickAttribute : Attribute
    {
        public enum Condition
        {
            InPlayerLocal,
            Always
        }

        public Condition LocalizableCondition { get; }

        public LocalizableGlobalGimmickAttribute(Condition condition)
        {
            LocalizableCondition = condition;
        }
    }
}
