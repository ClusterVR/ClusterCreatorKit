using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class ExternalCallUrlWindow : EditorWindow
    {
        readonly ExternalCallUrlView externalCallUrlView = new();
        RequireTokenAuthView tokenAuthView;

        [MenuItem("Cluster/外部通信(callExternal)接続先URL", priority = 305)]
        public static void Open()
        {
            var window = GetWindow<ExternalCallUrlWindow>();
            window.titleContent = new GUIContent("外部通信(callExternal)接続先URL");
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
