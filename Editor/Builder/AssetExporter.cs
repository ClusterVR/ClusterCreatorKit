using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class AssetExporter
    {
        public static void ExportCurrentSceneResource(string sceneName)
        {
            try
            {
                EditorUtility.DisplayProgressBar("Pre-Process Export Resource", "", 1 / 3f);

                var scene = SceneManager.GetActiveScene();
                if (scene.isDirty)
                {
                    EditorSceneManager.SaveScene(scene);
                }

                EditorUtility.DisplayProgressBar("Building Resources", "", 2 / 3f);
                BuildAssetBundles(scene, sceneName);

                EditorUtility.DisplayProgressBar("Post-Process Export Resource", "", 3 / 3f);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        static void BuildAssetBundles(Scene scene, string sceneName)
        {
            var currentTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;

            BuildAssetBundle(scene, sceneName, BuildTarget.StandaloneWindows);
            BuildAssetBundle(scene, sceneName, BuildTarget.StandaloneOSX);
            BuildAssetBundle(scene, sceneName, BuildTarget.Android);
            BuildAssetBundle(scene, sceneName, BuildTarget.iOS);

            EditorUserBuildSettings.SwitchActiveBuildTarget(currentTargetGroup, currentBuildTarget);
        }

        static void BuildAssetBundle(Scene scene, string sceneName, BuildTarget target)
        {
            var tempScenePath = $"Assets/{sceneName}.unity";
            var exportDirPath = $"{Application.temporaryCachePath}/{target}";
            var exportFilePath = $"{exportDirPath}/{sceneName}";
            BuiltAssetBundlePaths.instance.AddOrUpdate(target, exportFilePath);

            PreProcessBuildAssetBundle(scene, tempScenePath, sceneName, target);

            if (!Directory.Exists(exportDirPath))
            {
                Directory.CreateDirectory(exportDirPath);
            }

            if (File.Exists(exportFilePath))
            {
                File.Delete(exportFilePath);
            }

            Debug.Log($"Building to {exportDirPath}");
            BuildPipeline.BuildAssetBundles(exportDirPath, BuildAssetBundleOptions.None, target);

            PostProcessBuildAssetBundle(tempScenePath);
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
                return loweredName.StartsWith("[remove_if") && loweredName.Contains(target.ToString().ToLower());
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
                Object.DestroyImmediate(removeTarget);
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
    }
}
