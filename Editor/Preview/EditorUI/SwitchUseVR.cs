using ClusterVR.CreatorKit.Editor.Analytics;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorUI
{
    public static class SwitchUseVR
    {
        const string UseVRKey = "UseVR";
        const string OpenVRPackageName = "openvr";

        [MenuItem("Cluster/Preview/UseVR/EnableVR", false, 1)]
        public static void EnableVRMenu()
        {
            PlayerPrefs.SetInt(UseVRKey, 1);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.ClusterPreview_EnableVR);
        }

        [MenuItem("Cluster/Preview/UseVR/EnableVR", true, 1)]
        public static bool EnableVRMenuValidate()
        {
            return !EnabledVR();
        }

        [MenuItem("Cluster/Preview/UseVR/DisableVR", false, 2)]
        public static void DisableVRMenu()
        {
            PlayerPrefs.SetInt(UseVRKey, 0);
            PanamaLogger.LogCckMenuItem(PanamaLogger.MenuItemType.ClusterPreview_DisableVR);
        }

        [MenuItem("Cluster/Preview/UseVR/DisableVR", true, 2)]
        public static bool DisableVRMenuValidate()
        {
            return EnabledVR();
        }

        public static bool EnabledVR()
        {
            return PackageListRepository.Contain(OpenVRPackageName) && PlayerPrefs.GetInt(UseVRKey, 0) == 1;
        }
    }
}
