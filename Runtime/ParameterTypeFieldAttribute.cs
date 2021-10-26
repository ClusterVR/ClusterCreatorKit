using UnityEngine;

namespace ClusterVR.CreatorKit
{
    public sealed class ParameterTypeFieldAttribute : PropertyAttribute
    {
        public ParameterType[] Selectables { get; }

        public ParameterTypeFieldAttribute(params ParameterType[] selectables)
        {
            Selectables = selectables;
        }
    }
}
