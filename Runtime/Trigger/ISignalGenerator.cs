namespace ClusterVR.CreatorKit.Trigger
{
    public interface ISignalGenerator
    {
        bool TryGet(out StateValue value);
    }
}
