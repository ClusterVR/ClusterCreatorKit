using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    public sealed class ItemGroupMember : ItemGroupBase, IItemGroupMember
    {
        [SerializeField] ItemGroupHost host;

        public IItemGroupHost Host => host;
    }
}
