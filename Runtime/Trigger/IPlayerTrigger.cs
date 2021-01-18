namespace ClusterVR.CreatorKit.Trigger
{
    public interface IPlayerTrigger : ITrigger
    {
        event PlayerTriggerEventHandler TriggerEvent;
    }

    public delegate void PlayerTriggerEventHandler(IPlayerTrigger sender, TriggerEventArgs e);
}
