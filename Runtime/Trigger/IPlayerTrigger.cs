namespace ClusterVR.CreatorKit.Trigger
{
    public interface IPlayerTrigger
    {
        event PlayerTriggerEventHandler TriggerEvent;
    }

    public delegate void PlayerTriggerEventHandler(IPlayerTrigger sender, TriggerEventArgs e);
}