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
        }

        [MenuItem("Cluster/Preview/UseVR/EnableVR", true)]
        public static bool EnableVRMenuValidate()
        {
            return !EnabledVR();
        }

        [MenuItem("Cluster/Preview/UseVR/DisableVR", false, 1)]
        public static void DisableVRMenu()
        {
            PlayerPrefs.SetInt(UseVRKey, 0);
        }

        [MenuItem("Cluster/Preview/UseVR/DisableVR", true)]
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
