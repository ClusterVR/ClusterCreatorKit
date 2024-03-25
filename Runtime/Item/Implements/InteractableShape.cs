namespace ClusterVR.CreatorKit.Item.Implements
{
    public sealed class InteractableShape : BaseShape, IInteractableShape
    {
        public override bool IsTrigger => true;
    }
}
