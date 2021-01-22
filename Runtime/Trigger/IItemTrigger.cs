using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Trigger
{
    public interface IItemTrigger : ITrigger
    {
        IItem Item { get; }
        event TriggerEventHandler TriggerEvent;
    }

    public delegate void TriggerEventHandler(IItemTrigger sender, TriggerEventArgs e);
}
