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

        public static bool OpenWorldManagementPageAfterUpload
        {
            get => EditorPrefs.GetBool(OpenWorldManagementPageAfterUploadKey, true);
            set => EditorPrefs.SetBool(OpenWorldManagementPageAfterUploadKey, value);
        }
    }
}
