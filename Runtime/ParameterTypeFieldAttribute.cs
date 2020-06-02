using UnityEngine;

namespace ClusterVR.CreatorKit
{
    public class ParameterTypeFieldAttribute : PropertyAttribute
    {
        public ParameterType[] Selectables { get; }

        public ParameterTypeFieldAttribute(params ParameterType[] selectables)
        {
            Selectables = selectables;
        }
    }
}