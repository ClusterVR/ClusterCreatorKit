using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Core
{
    public static class EditorPrefsUtils
    {
        public static string SavedAccessToken
        {
            get => EditorPrefs.GetString(Constants.AccessTokenSaveKey, "");
            set => EditorPrefs.SetString(Constants.AccessTokenSaveKey, value);
        }

        public static string LastBuildWin
        {
            get => EditorPrefs.GetString(Constants.LastBuildWinKey, "");
            set => EditorPrefs.SetString(Constants.LastBuildWinKey, value);
        }

        public static string LastBuildMac
        {
            get => EditorPrefs.GetString(Constants.LastBuildMacKey, "");
            set => EditorPrefs.SetString(Constants.LastBuildMacKey, value);
        }

        public static string LastBuildAndroid
        {
            get => EditorPrefs.GetString(Constants.LastBuildAndroidKey, "");
            set => EditorPrefs.SetString(Constants.LastBuildAndroidKey, value);
        }

        public static string LastBuildIOS
        {
            get => EditorPrefs.GetString(Constants.LastBuildIOSKey, "");
            set => EditorPrefs.SetString(Constants.LastBuildIOSKey, value);
        }

        public static string LastExportPackage
        {
            set => EditorPrefs.SetString(Constants.LastExportPackageKey, value);
        }

        public static bool OpenWorldManagementPageAfterUpload
        {
            get => EditorPrefs.GetBool(Constants.OpenWorldManagementPageAfterUploadKey, true);
            set => EditorPrefs.SetBool(Constants.OpenWorldManagementPageAfterUploadKey, value);
        }
    }
}
