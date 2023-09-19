using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class UploadedCraftItemTemplateInfoWindow : EditorWindow
    {
        readonly UploadedCraftItemTemplateInfoView infoView = new();
        RequireTokenAuthView tokenAuthView;

        [MenuItem("Cluster/クラフトアイテムの情報取得", priority = 304)]
        public static void Open()
        {
            var window = GetWindow<UploadedCraftItemTemplateInfoWindow>();
            window.minSize = new Vector2(640, 530);
            window.titleContent = new GUIContent("クラフトアイテムの情報取得");
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
