using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClusterVR.CreatorKit.World;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class AssetExporter
    {
        static readonly BuildTarget[] BuildTargets = { BuildTarget.StandaloneWindows, BuildTarget.StandaloneOSX, BuildTarget.Android, BuildTarget.iOS };

        public static void ExportCurrentSceneResource(string venueId)
        {
            BuiltAssetBundlePaths.instance.ClearSubScenes();
            BuiltAssetBundlePaths.instance.ClearVenueAssets();

            try
            {
                EditorUtility.DisplayProgressBar("Pre-Process Export Resource", "", 1 / 3f);

                var scene = SceneManager.GetActiveScene();
                if (scene.isDirty)
                {
                    EditorSceneManager.SaveScene(scene);
                }

                var subScenes = GatherSubScenes(scene);
                foreach (var (scenePath, _) in subScenes)
                {
                    var subSceneAsset = EditorSceneManager.GetSceneByPath(scenePath);
                    if (subSceneAsset.isDirty)
                    {
                        EditorSceneManager.SaveScene(subSceneAsset);
                    }
                }

                EditorUtility.DisplayProgressBar("Building Resources", "", 2 / 3f);

                var currentTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                var currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;
                try
                {
                    foreach (var buildTarget in BuildTargets)
                    {
                        BuildAssetBundles(scene.path, subScenes, venueId, buildTarget);
                    }
                }
                finally
                {
                    EditorUserBuildSettings.SwitchActiveBuildTarget(currentTargetGroup, currentBuildTarget);
                }

                EditorUtility.DisplayProgressBar("Post-Process Export Resource", "", 3 / 3f);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        static void BuildAssetBundles(string mainScenePath, IReadOnlyList<(string scenePath, string sceneName)> subScenes, string venueId, BuildTarget target)
        {
            var buildInfos = new List<(string tempScenePath, AssetBundleBuild assetBundleBuild, bool isMainScene, string[] assetIdsDependsOn)>();
            var venueAssetInfos = new List<AssetBundleBuild>();

            try
            {
                var mainSceneInfo = CreateSceneToBuild(mainScenePath, venueId, target, true);

                var assetIds = Array.Empty<string>();

                if (subScenes.Any())
                {
                    if (CreateVenueAssetBundle(mainSceneInfo.tempPath, target, out var venueAssetBundleBuild))
                    {
                        venueAssetInfos.Add(venueAssetBundleBuild);
                        assetIds = new[] { venueAssetBundleBuild.assetBundleName };
                    }
                }

                mainSceneInfo.assetIdsDependsOn = assetIds;
                buildInfos.Add(mainSceneInfo);

                foreach (var (scenePath, sceneName) in subScenes)
                {
                    var subSceneInfo = CreateSceneToBuild(scenePath, sceneName, target, false);
                    subSceneInfo.assetIdsDependsOn = assetIds;
                    buildInfos.Add(subSceneInfo);
                }

                var exportDirPath = $"{Application.temporaryCachePath}/{target}";
                if (!Directory.Exists(exportDirPath))
                {
                    Directory.CreateDirectory(exportDirPath);
                }

                var assetBundleBuilds = buildInfos.Select(i => i.assetBundleBuild).Concat(venueAssetInfos).ToArray();
                foreach (var assetBundleName in assetBundleBuilds.Select(b => b.assetBundleName))
                {
                    var exportFilePath = $"{exportDirPath}/{assetBundleName}";
                    if (File.Exists(exportFilePath))
                    {
                        File.Delete(exportFilePath);
                    }
                }

                Debug.Log($"Building to {exportDirPath}");
                BuildPipeline.BuildAssetBundles(exportDirPath, assetBundleBuilds, BuildAssetBundleOptions.ForceRebuildAssetBundle, target);

                foreach (var (_, assetBundleBuild, isMainScene, assetIdsDependsOn) in buildInfos)
                {
                    var exportFilePath = $"{exportDirPath}/{assetBundleBuild.assetBundleName}";
                    if (isMainScene)
                    {
                        BuiltAssetBundlePaths.instance.AddOrUpdateMainScene(target, exportFilePath, assetIdsDependsOn);
                    }
                    else
                    {
                        BuiltAssetBundlePaths.instance.AddSubScene(target, exportFilePath, assetIdsDependsOn);
                    }
                }

                foreach (var assetBundleBuild in venueAssetInfos)
                {
                    var exportFilePath = $"{exportDirPath}/{assetBundleBuild.assetBundleName}";
                    BuiltAssetBundlePaths.instance.AddVenueAsset(target, exportFilePath);
                }
            }
            finally
            {
                foreach (var (tempScenePath, _, _, _) in buildInfos)
                {
                    RemoveSceneToBuild(tempScenePath);
                }
            }
        }

        static (string tempPath, AssetBundleBuild assetBundleBuild, bool isMainScene, string[] assetIdsDependsOn) CreateSceneToBuild(string scenePath, string assetBundleName, BuildTarget target, bool isMainScene)
        {
            var tempPath = $"Assets/{assetBundleName}.unity";
            if (!AssetDatabase.CopyAsset(scenePath, tempPath))
            {
                throw new Exception($"Fail copy asset, {scenePath} to {tempPath}");
            }

            AssetBundleBuild assetBundleBuild;
            try
            {
                RemoveObjectsByPlatform(tempPath, target);

                assetBundleBuild = new AssetBundleBuild
                {
                    assetBundleName = assetBundleName,
                    assetNames = new[] { tempPath },
                };
            }
            catch
            {
                RemoveSceneToBuild(tempPath);
                throw;
            }
            return (tempPath, assetBundleBuild, isMainScene, Array.Empty<string>());
        }

        static bool CreateVenueAssetBundle(string scenePath, BuildTarget target, out AssetBundleBuild assetBundleBuild)
        {
            var assetNames = SceneBuildDependenciesCollector.Collect(scenePath, target);

            if (assetNames.Length == 0)
            {
                assetBundleBuild = default;
                return false;
            }

            var assetBundleName = $"asset_{Guid.NewGuid()}";
            assetBundleBuild = new AssetBundleBuild
            {
                assetNames = assetNames,
                assetBundleName = assetBundleName,
            };
            return true;
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

        static void RemoveSceneToBuild(string tempPath)
        {
            AssetDatabase.DeleteAsset(tempPath);
        }

        static IReadOnlyList<(string scenePath, string sceneName)> GatherSubScenes(Scene mainScene)
        {
            var subScenes = new List<(string scenePath, string sceneName)>();
            var subScenesCandidates = mainScene.GetRootGameObjects()
                .SelectMany(x => x.GetComponentsInChildren<ISubScene>(true))
                .Select(s => (UnityScene: s.UnityScene, SubSceneName: s.SceneName))
                .ToArray();
            foreach (var (unityScene, sceneName) in subScenesCandidates)
            {
                if (unityScene == null)
                {
                    continue;
                }

                var subSceneAssetPath = AssetDatabase.GetAssetPath(unityScene);
                if (string.IsNullOrEmpty(subSceneAssetPath))
                {
                    throw new Exception($"サブシーン({unityScene.name})が見つかりません");
                }

                subScenes.Add((subSceneAssetPath, sceneName));
            }

            return subScenes;
        }
    }
}
