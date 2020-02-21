using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public static class AssetExporter
    {
        public static void ExportCurrentSceneResource(string sceneName, bool exportPackage)
        {
            try
            {
                EditorUtility.DisplayProgressBar("Pre-Process Export Resource", "", 1/3f);

                var tempPath = $"Assets/{sceneName}.unity";

                ExportPreProcess(tempPath, sceneName);

                if (exportPackage)
                {
                    ExportUnityPackage($"{sceneName}.unitypackage", tempPath);
                }

                EditorUtility.DisplayProgressBar("Building Resources", "", 2/3f);
                BuildAssetBundles();

                EditorUtility.DisplayProgressBar("Post-Process Export Resource", "", 3/3f);
                ExportPostProcess(tempPath);

                EditorPrefsUtils.LastBuildWin =
                    $"{Application.temporaryCachePath}/{BuildTarget.StandaloneWindows}/{sceneName}";
                EditorPrefsUtils.LastBuildMac =
                    $"{Application.temporaryCachePath}/{BuildTarget.StandaloneOSX}/{sceneName}";
                // NOTE: Androidビルドはない場合もある
                EditorPrefsUtils.LastBuildAndroid =
                    $"{Application.temporaryCachePath}/{BuildTarget.Android}/{sceneName}";
                EditorPrefsUtils.LastBuildIOS =
                    $"{Application.temporaryCachePath}/{BuildTarget.iOS}/{sceneName}";
            }
            catch (Exception ex)
            {
                Debug.LogError($"Export Exception : {ex.Message}");
                throw;
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        static void ExportPreProcess(string tempPath, string assetBundleName)
        {
            var currentScenePath = SceneManager.GetActiveScene().path;

            if (!AssetDatabase.CopyAsset(currentScenePath, tempPath))
            {
                throw new Exception($"Fail copy asset, {currentScenePath} to {tempPath}");
            }

            AssetDatabase.Refresh();
            AssetDatabase.RemoveUnusedAssetBundleNames();

            var assetImporter = AssetImporter.GetAtPath(tempPath);
            assetImporter.assetBundleName = assetBundleName;
            assetImporter.SaveAndReimport();
        }

        static void ExportUnityPackage(string packageName, string tempPath)
        {
            EditorUtility.DisplayProgressBar("Exporting unitypackage", "", 1f);
            var exportPath = $"{Application.temporaryCachePath}/{packageName}";
            ExportCurrentSceneAsUnityPackage(tempPath, exportPath);
            EditorPrefsUtils.LastExportPackage = exportPath;
        }

        static void ExportPostProcess(string tempPath)
        {
            var assetImporter = AssetImporter.GetAtPath(tempPath);
            assetImporter.assetBundleName = string.Empty;
            assetImporter.SaveAndReimport();
            AssetDatabase.DeleteAsset(tempPath);
            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

        static void BuildAssetBundles()
        {
            var currentTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;

            BuildAssetBundleCore(BuildTarget.StandaloneWindows);
            BuildAssetBundleCore(BuildTarget.StandaloneOSX);
            BuildAssetBundleCore(BuildTarget.Android);
            BuildAssetBundleCore(BuildTarget.iOS);

            EditorUserBuildSettings.SwitchActiveBuildTarget(
                currentTargetGroup,
                currentBuildTarget
            );
        }

        static void BuildAssetBundleCore(BuildTarget target)
        {
            var exportPath = $"{Application.temporaryCachePath}/{target}";

            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }

            Debug.Log($"Building to {exportPath}");
            BuildPipeline.BuildAssetBundles(
                exportPath,
                BuildAssetBundleOptions.None,
                target
            );
        }

        static void ExportCurrentSceneAsUnityPackage(string scenePath, string exportPath)
        {
            ExportCurrentAssetAsUnityPackage(new List<string>
            {
                scenePath,
                "Assets/ClusterVR"
            }, exportPath);
        }

        static void ExportCurrentAssetAsUnityPackage(List<string> assetPaths, string destinationPath)
        {
            Debug.Log($"Exporting to {destinationPath}");
            AssetDatabase.ExportPackage(
                assetPaths.ToArray(),
                destinationPath,
                ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies |
                ExportPackageOptions.IncludeLibraryAssets
            );
        }
    }
}
