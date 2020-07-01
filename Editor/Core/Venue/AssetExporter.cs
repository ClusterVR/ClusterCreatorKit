using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
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

                var scene = SceneManager.GetActiveScene();
                if (scene.isDirty)
                {
                    EditorSceneManager.SaveScene(scene);
                }

                if (exportPackage)
                {
                    ExportUnityPackage($"{sceneName}.unitypackage", scene.path);
                }

                EditorUtility.DisplayProgressBar("Building Resources", "", 2/3f);
                BuildAssetBundles(scene, sceneName);

                EditorUtility.DisplayProgressBar("Post-Process Export Resource", "", 3/3f);

                EditorPrefsUtils.LastBuildWin =
                    $"{Application.temporaryCachePath}/{BuildTarget.StandaloneWindows}/{sceneName}";
                EditorPrefsUtils.LastBuildMac =
                    $"{Application.temporaryCachePath}/{BuildTarget.StandaloneOSX}/{sceneName}";
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

        static void ExportUnityPackage(string packageName, string tempPath)
        {
            EditorUtility.DisplayProgressBar("Exporting unitypackage", "", 1f);
            var exportPath = $"{Application.temporaryCachePath}/{packageName}";
            ExportSceneAsUnityPackage(tempPath, exportPath);
            EditorPrefsUtils.LastExportPackage = exportPath;
        }

        static void BuildAssetBundles(Scene scene, string sceneName)
        {
            var currentTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;

            BuildAssetBundle(scene, sceneName, BuildTarget.StandaloneWindows);
            BuildAssetBundle(scene, sceneName, BuildTarget.StandaloneOSX);
            BuildAssetBundle(scene, sceneName, BuildTarget.Android);
            BuildAssetBundle(scene, sceneName, BuildTarget.iOS);

            EditorUserBuildSettings.SwitchActiveBuildTarget(
                currentTargetGroup,
                currentBuildTarget
            );
        }

        static void BuildAssetBundle(Scene scene, string sceneName, BuildTarget target)
        {
            var tempPath = $"Assets/{sceneName}.unity";
            var exportPath = $"{Application.temporaryCachePath}/{target}";

            PreProcessBuildAssetBundle(scene, tempPath, sceneName, target);

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

            PostProcessBuildAssetBundle(tempPath);
        }

        static void PreProcessBuildAssetBundle(Scene scene, string tempPath, string assetBundleName, BuildTarget target)
        {
            var scenePath = scene.path;
            if (!AssetDatabase.CopyAsset(scenePath, tempPath))
            {
                throw new Exception($"Fail copy asset, {scenePath} to {tempPath}");
            }

            AssetDatabase.Refresh();
            AssetDatabase.RemoveUnusedAssetBundleNames();

            RemoveObjectsByPlatform(tempPath, target);

            var assetImporter = AssetImporter.GetAtPath(tempPath);
            assetImporter.assetBundleName = assetBundleName;
            assetImporter.SaveAndReimport();
        }

        static void RemoveObjectsByPlatform(string scenePath, BuildTarget target)
        {
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
            var scene = SceneManager.GetSceneByPath(scenePath);
            var rootGameObjects = scene.GetRootGameObjects();

            bool ShouldRemove(GameObject gameObject)
            {
                var loweredName = gameObject.name.ToLower();
                return loweredName.StartsWith("[remove_if") &&
                       loweredName.Contains(target.ToString().ToLower());
            }

            var removeTargets = new List<GameObject>();
            void GatherRemoveTarget(GameObject gameObject)
            {
                if (ShouldRemove(gameObject))
                {
                    removeTargets.Add(gameObject);
                    return;
                }

                foreach (Transform child in gameObject.transform)
                {
                    GatherRemoveTarget(child.gameObject);
                }
            }
            foreach (var rootGameObject in rootGameObjects)
            {
                GatherRemoveTarget(rootGameObject);
            }

            foreach (var removeTarget in removeTargets)
            {
                GameObject.DestroyImmediate(removeTarget);
                EditorSceneManager.MarkSceneDirty(scene);
            }

            if (scene.isDirty)
            {
                EditorSceneManager.SaveScene(scene);
            }

            EditorSceneManager.CloseScene(scene, true);
        }

        static void PostProcessBuildAssetBundle(string tempPath)
        {
            var assetImporter = AssetImporter.GetAtPath(tempPath);
            assetImporter.assetBundleName = string.Empty;
            assetImporter.SaveAndReimport();
            AssetDatabase.DeleteAsset(tempPath);
            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

        static void ExportSceneAsUnityPackage(string scenePath, string exportPath)
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
