using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class UploadedCraftItemTemplateInfoWindow : EditorWindow
    {
        readonly UploadedCraftItemTemplateInfoView infoView = new();
        RequireTokenAuthView tokenAuthView;

        [MenuItem(TranslationTable.cck_cluster_craftitem_info_fetch, priority = 304)]
        public static void Open()
        {
            var window = GetWindow<UploadedCraftItemTemplateInfoWindow>();
            window.minSize = new Vector2(640, 530);
            window.titleContent = new GUIContent(TranslationTable.cck_craftitem_info_fetch);
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
            infoView.Dispose();
        }

        VisualElement CreateView()
        {
            tokenAuthView = new RequireTokenAuthView(infoView);
            var view = tokenAuthView.CreateView();
            view.SetEnabled(true);

            return view;
        }
    }
}
