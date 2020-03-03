using ClusterVR.CreatorKit.Editor.Preview.EditorUI;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Preview.PackageInstaller
{
    public static class PackageInstaller
    {
        [MenuItem("Cluster/Preview/PackageInstaller")]
        static void Initialize()
        {
            AsyncInitialize();
        }

        static async void AsyncInitialize()
        {
            await PackageListRepository.UpdatePackageList();
            var packageStates = new PackageStates(
                PackageListRepository.Contain("timeline"),
                PackageListRepository.Contain("textmeshpro"),
                PackageListRepository.Contain("postprocessing"),
                PackageListRepository.Contain("openvr")
            );

            PackageInstallerWindow.ShowWithState(packageStates);
        }
    }

    public readonly struct PackageStates
    {
        //各packageがインストールされているかどうかをboolで表現する
        public readonly bool TimeLine;
        public readonly bool TMPro;
        public readonly bool PostProcessingStack;
        public readonly bool OpenVR;

        public PackageStates(bool timeLine, bool tMPro, bool postProcessingStack, bool openVR)
        {
            TimeLine = timeLine;
            TMPro = tMPro;
            PostProcessingStack = postProcessingStack;
            OpenVR = openVR;
        }
    }
}