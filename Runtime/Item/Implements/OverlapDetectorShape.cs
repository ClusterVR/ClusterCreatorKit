namespace ClusterVR.CreatorKit.Item.Implements
{
    public sealed class OverlapDetectorShape : BaseShape, IOverlapDetectorShape
    {
        public override bool IsTrigger => true;
    }
}
