using ClusterVR.CreatorKit.Constants;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    [InitializeOnLoad]
    public static class LayerCollisionConfigurer
    {
        static LayerCollisionConfigurer()
        {
            SetupLayerCollision();
        }

        public static void SetupLayerCollision()
        {
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.Default, false);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.TransparentFX, false);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.IgnoreRaycast, true);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.Water, true);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.UI, true);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.InteractableItem, false);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.GrabbingItem, false);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.VenueLayer0, true);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.VenueLayer1, true);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.Performer, false);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.Audience, false);
            Physics.IgnoreLayerCollision(LayerName.Default, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.TransparentFX, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.IgnoreRaycast, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.Water, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.UI, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.InteractableItem, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.GrabbingItem, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.VenueLayer0, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.VenueLayer1, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.TransparentFX, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.IgnoreRaycast, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.Water, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.UI, false);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.InteractableItem, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.GrabbingItem, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.VenueLayer0, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.VenueLayer1, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.IgnoreRaycast, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.Water, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.UI, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.InteractableItem, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.GrabbingItem, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.VenueLayer0, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.VenueLayer1, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.Water, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.UI, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.InteractableItem, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.GrabbingItem, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.VenueLayer0, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.VenueLayer1, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.UI, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.InteractableItem, false);
            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.GrabbingItem, false);
            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.VenueLayer0, true);
            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.VenueLayer1, true);
            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.Performer, false);
            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.Audience, false);
            Physics.IgnoreLayerCollision(LayerName.InteractableItem, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.GrabbingItem, LayerName.GrabbingItem, false);
            Physics.IgnoreLayerCollision(LayerName.GrabbingItem, LayerName.VenueLayer0, true);
            Physics.IgnoreLayerCollision(LayerName.GrabbingItem, LayerName.VenueLayer1, true);
            Physics.IgnoreLayerCollision(LayerName.GrabbingItem, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.GrabbingItem, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.GrabbingItem, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.GrabbingItem, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.GrabbingItem, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.VenueLayer0, LayerName.VenueLayer0, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer0, LayerName.VenueLayer1, false);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer0, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer0, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer0, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer0, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer0, LayerName.VenueLayer2, false);

            Physics.IgnoreLayerCollision(LayerName.VenueLayer1, LayerName.VenueLayer1, false);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer1, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer1, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer1, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer1, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.VenueLayer1, LayerName.VenueLayer2, false);

            Physics.IgnoreLayerCollision(LayerName.PostProcessing, LayerName.PostProcessing, true);
            Physics.IgnoreLayerCollision(LayerName.PostProcessing, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.PostProcessing, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.PostProcessing, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.PostProcessing, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.PerformerOnly, LayerName.PerformerOnly, true);
            Physics.IgnoreLayerCollision(LayerName.PerformerOnly, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.PerformerOnly, LayerName.Audience, false);
            Physics.IgnoreLayerCollision(LayerName.PerformerOnly, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.Performer, LayerName.Performer, true);
            Physics.IgnoreLayerCollision(LayerName.Performer, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.Performer, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.Audience, LayerName.Audience, true);
            Physics.IgnoreLayerCollision(LayerName.Audience, LayerName.VenueLayer2, true);

            Physics.IgnoreLayerCollision(LayerName.VenueLayer2, LayerName.VenueLayer2, true);
        }
    }
}