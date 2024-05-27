using ClusterVR.CreatorKit.Translation;
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
            wnd.titleContent = new GUIContent(TranslationTable.cck_package_installer);
        }

        public void OnEnable()
        {
            var root = rootVisualElement;

            if (packageStates.TimeLine && packageStates.TMPro && packageStates.PostProcessingStack &&
                packageStates.OpenVR)
            {
                VisualElement existLabel = new Label(TranslationTable.cck_all_packages_imported);
                root.Add(existLabel);
                return;
            }

            VisualElement notExistLabel = new Label(TranslationTable.cck_missing_preview_packages);
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

            VisualElement certificationLabel = new Label(TranslationTable.cck_import_packages_prompt);

            var acceptButton = new Button(() => ImportPackages(packageStates));
            var declineButton = new Button(Close);

            acceptButton.text = TranslationTable.cck_yes;
            declineButton.text = TranslationTable.cck_no;

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
