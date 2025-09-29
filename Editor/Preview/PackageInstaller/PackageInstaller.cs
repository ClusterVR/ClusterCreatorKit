using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Preview.EditorUI;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Preview.PackageInstaller
{
    public static class PackageInstaller
    {
        [MenuItem("Cluster/Preview/PackageInstaller", priority = 112)]
        static void Initialize()
        {
            AsyncInitialize();
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.ClusterPreview_PackageInstaller);
        }

        static async void AsyncInitialize()
        {
            await PackageListRepository.UpdatePackageList(default);
#if UNITY_6000_0_OR_NEWER
            var packageStates = new PackageStates(
                PackageListRepository.Contain("timeline"),
                PackageListRepository.Contain("postprocessing")
            );
#else
            var packageStates = new PackageStates(
                PackageListRepository.Contain("timeline"),
                PackageListRepository.Contain("textmeshpro"),
                PackageListRepository.Contain("postprocessing")
            );
#endif

            PackageInstallerWindow.ShowWithState(packageStates);
        }
    }

#if UNITY_6000_0_OR_NEWER
    public readonly struct PackageStates
    {

        public readonly bool TimeLine;
        public readonly bool PostProcessingStack;

        public PackageStates(bool timeLine, bool postProcessingStack)
        {
            TimeLine = timeLine;
            PostProcessingStack = postProcessingStack;
        }

        public bool AllPackagesImported => TimeLine && PostProcessingStack;
    }
#else
    public readonly struct PackageStates
    {

        public readonly bool TimeLine;
        public readonly bool TMPro;
        public readonly bool PostProcessingStack;

        public PackageStates(bool timeLine, bool tMPro, bool postProcessingStack)
        {
            TimeLine = timeLine;
            TMPro = tMPro;
            PostProcessingStack = postProcessingStack;
        }

        public bool AllPackagesImported => TimeLine && TMPro && PostProcessingStack;
    }
#endif
}
