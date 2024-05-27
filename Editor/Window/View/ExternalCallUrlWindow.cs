using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class ExternalCallUrlWindow : EditorWindow
    {
        readonly ExternalCallUrlView externalCallUrlView = new();
        RequireTokenAuthView tokenAuthView;

        [MenuItem(TranslationTable.cck_cluster_external_communication_url, priority = 305)]
        public static void Open()
        {
            var window = GetWindow<ExternalCallUrlWindow>();
            window.titleContent = new GUIContent(TranslationTable.cck_external_communication_url);
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
            externalCallUrlView.Dispose();
        }

        VisualElement CreateView()
        {
            tokenAuthView = new RequireTokenAuthView(externalCallUrlView);
            var authView = tokenAuthView.CreateView();
            authView.SetEnabled(true);

            return authView;
        }
    }
}
