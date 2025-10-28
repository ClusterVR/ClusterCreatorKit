using UnityEngine.Rendering;

namespace ClusterVR.CreatorKit.Common
{
    public static class RenderPipelineUtils
    {
        public static bool IsUrp() => GraphicsSettings.currentRenderPipeline != null;
    }
}
