using System;
using System.Linq;
using System.Threading.Tasks;
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
            _ = UpdatePackageList();
        }

        public static async Task UpdatePackageList()
        {
            while (status == StatusCode.InProgress)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }

            if (!ShouldUpdate())
            {
                return;
            }

            var request = Client.List();
            status = StatusCode.InProgress;
            while (request.Status == StatusCode.InProgress)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }

            if (request.Status == StatusCode.Failure)
            {
                status = StatusCode.Failure;
                Debug.LogError("package一覧の取得に失敗しました");
                throw new Exception();
            }

            Debug.Log("package一覧の取得に成功しました");
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
                throw new Exception("package一覧の取得に失敗しています");
            }

            return LoadPackageList().Any(x => x.packageId.Contains(packageName));
        }
    }
}
