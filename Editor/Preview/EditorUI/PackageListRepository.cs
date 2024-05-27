using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.EditorUI
{
    [InitializeOnLoad]
    public static class PackageListRepository
    {
        static PackageCollection packageCollection;
        static StatusCode status = StatusCode.Failure;
        static float lastUpdateTime;
        const string jsonPackageListKey = "packageList";
        const string jsonLastUpdateTimeKey = "lastUpdateTime";

        static PackageListRepository()
        {
            _ = UpdatePackageList(default);
        }

        public static async Task UpdatePackageList(CancellationToken cancellationToken)
        {
            while (status == StatusCode.InProgress)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10), cancellationToken);
            }

            if (!ShouldUpdate())
            {
                return;
            }

            var request = Client.List();
            status = StatusCode.InProgress;
            try
            {
                while (request.Status == StatusCode.InProgress)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(10), cancellationToken);
                }
            }
            catch (Exception)
            {
                status = StatusCode.Failure;
                throw;
            }

            if (request.Status == StatusCode.Failure)
            {
                status = StatusCode.Failure;
                Debug.LogError(TranslationTable.cck_package_list_fetch_failed);
                throw new Exception();
            }

            Debug.Log(TranslationTable.cck_package_list_fetch_success);
            status = StatusCode.Success;
            SavePackageList(request.Result);
            PlayerPrefs.SetFloat(jsonLastUpdateTimeKey, (float) EditorApplication.timeSinceStartup);
        }

        static bool ShouldUpdate()
        {
            return status == StatusCode.Failure || //取得失敗時
                EditorApplication.timeSinceStartup < PlayerPrefs.GetFloat(jsonLastUpdateTimeKey) || //Editor再起動時
                EditorApplication.timeSinceStartup - PlayerPrefs.GetFloat(jsonLastUpdateTimeKey) > 30; // 前回取得から30秒経過時
        }

        static void SavePackageList(PackageCollection packageCollection)
        {
            var json = JsonUtility.ToJson(packageCollection);
            PlayerPrefs.SetString(jsonPackageListKey, json);
            PlayerPrefs.Save();
        }

        static PackageCollection LoadPackageList()
        {
            var json = PlayerPrefs.GetString(jsonPackageListKey);
            return JsonUtility.FromJson<PackageCollection>(json);
        }

        public static bool Contain(string packageName)
        {
            if (status == StatusCode.Failure)
            {
                throw new Exception(TranslationTable.cck_package_list_fetch_error);
            }

            return LoadPackageList().Any(x => x.packageId.Contains(packageName));
        }
    }
}
