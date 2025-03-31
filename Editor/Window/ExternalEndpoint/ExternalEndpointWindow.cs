using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.ExternalEndpoint
{
    public sealed class ExternalEndpointWindow : EditorWindow
    {
        readonly ExternalEndpointView externalEndpointView = new();
        RequireTokenAuthView tokenAuthView;

        [MenuItem(TranslationTable.cck_cluster_external_communication_url, priority = 321)]
        public static void Open()
        {
            var window = GetWindow<ExternalEndpointWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_external_communication_url);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.Cluster_ExternalEndpoint);
        }

        void OnEnable()
        {
            var view = CreateView();
            rootVisualElement.Add(view);
        }

        void OnDisable()
        {
            rootVisualElement.Clear();
            if (tokenAuthView != null)
            {
                tokenAuthView.Dispose();
                tokenAuthView = null;
            }
        }

        void OnDestroy()
        {
            externalEndpointView.Dispose();
        }

        VisualElement CreateView()
        {
            tokenAuthView = new RequireTokenAuthView(externalEndpointView);
            var authView = tokenAuthView.CreateView();
            authView.SetEnabled(true);

            return authView;
        }
    }
}
