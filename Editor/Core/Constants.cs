using System.Reflection;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace ClusterVR.CreatorKit.Editor.Core
{
    public static class Constants
    {
        static BuildSetting buildSetting;

        static BuildSetting BuildSetting =>
            buildSetting ?? (buildSetting = JsonUtility.FromJson<BuildSetting>(Resources.Load<TextAsset>("BuildSetting").text));

        static string Host => BuildSetting.Host;

        public static string UserApiBaseUrl => $"https://user-api.{Host}";

        public static string VenueApiBaseUrl => $"https://api.{Host}";

        public static string WebBaseUrl => $"https://{Host}";

        public static string AccessTokenSaveKey => "cluster_sdk_access_token";

        //最後にビルドしたasset置くPathを保存するPrefsKey
        public static string LastBuildWinKey => "LastBuildWin";
        public static string LastBuildMacKey => "LastBuildMac";
        public static string LastBuildAndroidKey => "LastBuildAndroid";
        public static string LastBuildIOSKey => "LastBuildIOS";
        public static string LastExportPackageKey => "LastExportPackage";
        public static string OpenWorldManagementPageAfterUploadKey => "OpenWorldManagementPageAfterUpload";

        // package の version (SemVer)
        public static string GetCreatorKitVersion()
        {
            var type = MethodBase.GetCurrentMethod().DeclaringType;
            var assembly = Assembly.GetAssembly(type);
            var package = PackageInfo.FindForAssembly(assembly);
            return package.version;
        }
    }
}
