namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IPlayerEffectGimmick : IPlayerGimmick
    {
        event PlayerEffectEventHandler OnRun;
    }

    public interface IPlayerEffect
    {
    }

    public delegate void PlayerEffectEventHandler(IPlayerEffect effect);
}