namespace ClusterVR.CreatorKit.Trigger
{
    public interface IGlobalTrigger : ITrigger
    {
        event GlobalTriggerEventHandler TriggerEvent;
    }

    public delegate void GlobalTriggerEventHandler(TriggerEventArgs e);
}
