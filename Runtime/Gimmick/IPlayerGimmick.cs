namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IPlayerGimmick : IGimmick
    {
        event PlayerGimmickEventHandler OnRun;
    }
    public delegate void PlayerGimmickEventHandler(IPlayerGimmick sender);
}