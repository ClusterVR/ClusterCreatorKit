using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class OnReceiveOwnershipItemTriggerManager
    {
        readonly ItemCreator itemCreator;

        public OnReceiveOwnershipItemTriggerManager(ItemCreator itemCreator)
        {
            itemCreator.OnCreateCompleted += InvokeOnCreate;
        }

        void InvokeOnCreate(IItem item)
        {
            Invoke(item.gameObject.GetComponents<IOnReceiveOwnershipItemTrigger>(), true);
        }

        public void InvokeOnStart(IEnumerable<IOnReceiveOwnershipItemTrigger> onReceiveOwnershipItemTriggers)
        {
            Invoke(onReceiveOwnershipItemTriggers, false);
        }

        void Invoke(IEnumerable<IOnReceiveOwnershipItemTrigger> onReceiveOwnershipItemTriggers, bool isVoluntary)
        {
            foreach (var onReceiveOwnershipItemTrigger in onReceiveOwnershipItemTriggers)
            {
                onReceiveOwnershipItemTrigger.Invoke(isVoluntary);
            }
        }
    }
}
