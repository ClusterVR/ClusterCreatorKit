using System;
using ClusterVR.CreatorKit.Common;
using UnityEngine.Rendering;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public static class RenderPipelinePresetIdProvider
    {
        public static Func<string> CustomProvider { get; set; }

        public static string GetRenderPipelinePresetId()
        {
            if (CustomProvider != null)
            {
                var customId = CustomProvider();
                if (!string.IsNullOrEmpty(customId))
                {
                    return customId;
                }
            }

            return null;
        }
    }
}
