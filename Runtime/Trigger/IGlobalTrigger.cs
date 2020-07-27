namespace ClusterVR.CreatorKit.Trigger
{
    public interface IGlobalTrigger
    {
        event GlobalTriggerEventHandler TriggerEvent;
    }

    public delegate void GlobalTriggerEventHandler(TriggerEventArgs e);
}