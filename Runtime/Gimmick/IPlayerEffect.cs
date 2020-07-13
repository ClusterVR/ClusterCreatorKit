namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IPlayerEffect : IPlayerGimmick
    {
        event PlayerEffectEventHandler OnRun;
    }
    public delegate void PlayerEffectEventHandler(IPlayerEffect sender);
}