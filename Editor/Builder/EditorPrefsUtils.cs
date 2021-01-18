using ClusterVR.CreatorKit.Editor.Api.RPC;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class EditorPrefsUtils
    {
        const string AlreadyShownAboutWindowKey = "ClusterAlreadyShownAboutWindow";
        const string AccessTokenSaveKey = "cluster_sdk_access_token";
        const string TmpUserIdKey = "ClusterTmpUserId";
        const string EnableSendingAnalyticsDataKey = "ClusterEnableSendingAnalyticsData";
        const string LastBuildWinKey = "LastBuildWin";
        const string LastBuildMacKey = "LastBuildMac";
        const string LastBuildAndroidKey = "LastBuildAndroid";
        const string LastBuildIOSKey = "LastBuildIOS";
        const string LastExportPackageKey = "LastExportPackage";
        const string OpenWorldManagementPageAfterUploadKey = "OpenWorldManagementPageAfterUpload";

        public static bool HasAlreadyShownAboutWindow
        {
            get => EditorPrefs.GetBool(AlreadyShownAboutWindowKey, false);
            set => EditorPrefs.SetBool(AlreadyShownAboutWindowKey, value);
        }

        public static AuthenticationInfo SavedAccessToken
        {
            get => new AuthenticationInfo(EditorPrefs.GetString(AccessTokenSaveKey, ""));
            set => EditorPrefs.SetString(AccessTokenSaveKey, value?.RawValue);
        }

        public static string TmpUserId
        {
            get => EditorPrefs.GetString(TmpUserIdKey, "");
            set => EditorPrefs.SetString(TmpUserIdKey, value);
        }

        public static bool EnableSendingAnalyticsData
        {
            get => EditorPrefs.GetBool(EnableSendingAnalyticsDataKey, true);
            set => EditorPrefs.SetBool(EnableSendingAnalyticsDataKey, value);
        }

        public static string LastBuildWin
        {
            get => EditorPrefs.GetString(LastBuildWinKey, "");
            set => EditorPrefs.SetString(LastBuildWinKey, value);
        }

        public static string LastBuildMac
        {
            get => EditorPrefs.GetString(LastBuildMacKey, "");
            set => EditorPrefs.SetString(LastBuildMacKey, value);
        }

        public static string LastBuildAndroid
        {
            get => EditorPrefs.GetString(LastBuildAndroidKey, "");
            set => EditorPrefs.SetString(LastBuildAndroidKey, value);
        }

        public static string LastBuildIOS
        {
            get => EditorPrefs.GetString(LastBuildIOSKey, "");
            set => EditorPrefs.SetString(LastBuildIOSKey, value);
        }

        public static string LastExportPackage
        {
            set => EditorPrefs.SetString(LastExportPackageKey, value);
        }

        public static bool OpenWorldManagementPageAfterUpload
        {
            get => EditorPrefs.GetBool(OpenWorldManagementPageAfterUploadKey, true);
            set => EditorPrefs.SetBool(OpenWorldManagementPageAfterUploadKey, value);
        }
    }
}
