namespace ClusterVR.CreatorKit.Item.Implements
{
    public sealed class OverlapSourceShape : BaseShape, IOverlapSourceShape
    {
        protected override bool IsTrigger => true;
    }
}
