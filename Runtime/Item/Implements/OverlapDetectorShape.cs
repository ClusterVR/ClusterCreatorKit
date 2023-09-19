namespace ClusterVR.CreatorKit.Item.Implements
{
    public sealed class OverlapDetectorShape : BaseShape, IOverlapDetectorShape
    {
        protected override bool IsTrigger => true;
    }
}
