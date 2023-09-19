namespace ClusterVR.CreatorKit.Item.Implements
{
    public sealed class InteractableShape : BaseShape, IInteractableShape
    {
        protected override bool IsTrigger => true;
    }
}
