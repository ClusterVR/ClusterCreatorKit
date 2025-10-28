#if !CLUSTER_CREATOR_KIT_DISABLE_PREVIEW
using ClusterVR.CreatorKit.Common;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [InitializeOnLoad]
    public static class RenderPipelineChecker
    {
        const string RenderPipelineCheckedKey = "ClusterCreatorKitRenderPipelineChecker";
        static RenderPipelineChecker()
        {
            if (SessionState.GetBool(RenderPipelineCheckedKey, false))
            {
                return;
            }
            if (RenderPipelineUtils.IsUrp())
            {
                EditorUtility.DisplayDialog(TranslationTable.cck_attention,
                    TranslationUtility.GetMessage(TranslationTable.cck_render_pipeline_urp_warning),
                    TranslationTable.cck_close);
            }
            SessionState.SetBool(RenderPipelineCheckedKey, true);
        }
    }
}
#endif
