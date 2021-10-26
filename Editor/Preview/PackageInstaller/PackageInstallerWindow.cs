using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Preview.PackageInstaller
{
    public sealed class PackageInstallerWindow : EditorWindow
    {
        static PackageStates packageStates;

        public static void ShowWithState(PackageStates packageStates)
        {
            PackageInstallerWindow.packageStates = packageStates;
            var wnd = GetWindow<PackageInstallerWindow>();
            wnd.titleContent = new GUIContent("PackageInstaller");
        }

        public void OnEnable()
        {
            var root = rootVisualElement;

            if (packageStates.TimeLine && packageStates.TMPro && packageStates.PostProcessingStack &&
                packageStates.OpenVR)
            {
                VisualElement existLabel = new Label("プレビューに必要なパッケージはすべてインポートされています");
                root.Add(existLabel);
                return;
            }

            VisualElement notExistLabel = new Label("プレビューに必要なパッケージがインポートされていません");
            var notExistingPackage = new Label();
            if (!packageStates.TimeLine)
            {
                notExistingPackage.text += "TimeLine\n";
            }
            if (!packageStates.TMPro)
            {
                notExistingPackage.text += "TextMeshPro\n";
            }
            if (!packageStates.PostProcessingStack)
            {
                notExistingPackage.text += "PostProcessingStack\n";
            }
            if (!packageStates.OpenVR)
            {
                notExistingPackage.text += "OpenVR";
            }

            VisualElement certificationLabel = new Label("これらのパッケージをインポートしますか？");

            var acceptButton = new Button(() => ImportPackages(packageStates));
            var declineButton = new Button(Close);

            acceptButton.text = "はい";
            declineButton.text = "いいえ";

            var buttonsBox = new Box();
            buttonsBox.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.RowReverse);
            buttonsBox.Add(declineButton);
            buttonsBox.Add(acceptButton);

            root.Add(notExistLabel);
            root.Add(notExistingPackage);
            root.Add(certificationLabel);
            root.Add(buttonsBox);
        }

        void ImportPackages(PackageStates packageStates)
        {
            if (!packageStates.TimeLine)
            {
                Client.Add("com.unity.timeline");
            }
            if (!packageStates.TMPro)
            {
                Client.Add("com.unity.textmeshpro");
            }
            if (!packageStates.PostProcessingStack)
            {
                Client.Add("com.unity.postprocessing");
            }
            if (!packageStates.OpenVR)
            {
                Client.Add("com.unity.xr.openvr.standalone");
            }
            Close();
        }
    }
}
