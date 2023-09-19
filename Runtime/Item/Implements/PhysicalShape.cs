namespace ClusterVR.CreatorKit.Item.Implements
{
    public sealed class PhysicalShape : BaseShape, IPhysicalShape
    {
        protected override bool IsTrigger => false;
    }
}
