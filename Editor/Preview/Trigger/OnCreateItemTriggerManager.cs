using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Trigger;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class OnCreateItemTriggerManager
    {
        readonly ItemCreator itemCreator;

        public OnCreateItemTriggerManager(ItemCreator itemCreator)
        {
            itemCreator.OnCreateCompleted += Invoke;
        }

        void Invoke(IItem item)
        {
            Invoke(item.gameObject.GetComponents<IOnCreateItemTrigger>());
        }

        public void Invoke(IEnumerable<IOnCreateItemTrigger> onCreateItemTriggers)
        {
            foreach (var onCreateItemTrigger in onCreateItemTriggers)
            {
                onCreateItemTrigger.Invoke();
            }
        }
    }
}