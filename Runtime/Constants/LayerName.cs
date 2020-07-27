namespace ClusterVR.CreatorKit.Constants
{
    public static partial class LayerName
    {
        public const int Default = 0;
        public const int TransparentFX = 1;
        public const int IgnoreRaycast = 2;
        public const int Water = 4;
        public const int UI = 5;
        public const int InteractableItem = 14;
        public const int GrabbingItem = 18;
        public const int VenueLayer0 = 19;
        public const int VenueLayer1 = 20;
        public const int PostProcessing = 21;
        public const int PerformerOnly = 22;
        public const int Performer = 23;
        public const int Audience = 24;
        public const int VenueLayer2 = 29;

        public const int InteractableItemMask = 1 << InteractableItem;
        public const int PostProcessingMask = 1 << PostProcessing;
    }
}
