using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class Group
    {
        [SerializeField] string id;
        [SerializeField] string name;
        [SerializeField] string description;
        [SerializeField] string type;
        [SerializeField] Member[] members;

        public GroupID Id => new GroupID(id);
        public string Name => name;
        public string Description => description;
        public GroupType GroupType => (GroupType) Enum.Parse(typeof(GroupType), type);
        public IEnumerable<Member> Members => members;
    }

    public enum GroupType
    {
        Single = 1,
        Multiple = 2
    }
}
