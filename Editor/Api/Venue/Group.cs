using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    [Serializable]
    public sealed class Group
    {
        [SerializeField] string id;
        [SerializeField] string name;
        [SerializeField] string type;

        public GroupID Id => new GroupID(id);
        public string Name => name;
        public GroupType GroupType => (GroupType) Enum.Parse(typeof(GroupType), type);
    }

    public enum GroupType
    {
        Single = 1,
        Multiple = 2
    }
}
