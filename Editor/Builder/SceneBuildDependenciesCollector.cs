using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class SceneBuildDependenciesCollector
    {
        public static IReadOnlyCollection<string> Collect(string sceneAssetPath)
        {
            var assetsForBuild = new HashSet<string>();
            var assetsNotForBuild = new HashSet<string>();

            var tempScenePath = $"Assets/{Guid.NewGuid()}.unity";
            AssetDatabase.CopyAsset(sceneAssetPath, tempScenePath);
            try
            {
                var scene = EditorSceneManager.OpenScene(tempScenePath, OpenSceneMode.Additive);
                try
                {
                    foreach (var root in scene.GetRootGameObjects())
                    {
                        UnpackAll(root);
                    }
                    EditorSceneManager.SaveScene(scene);
                }
                finally
                {
                    EditorSceneManager.CloseScene(scene, true);
                }

                foreach (var dependency in AssetDatabase.GetDependencies(tempScenePath, false))
                {
                    GatherAssetsForBuild(assetsForBuild, assetsNotForBuild, dependency);
                }
            }
            finally
            {
                AssetDatabase.DeleteAsset(tempScenePath);
            }

            return assetsForBuild;
        }

        static void UnpackAll(GameObject go)
        {
            if (PrefabUtility.IsOutermostPrefabInstanceRoot(go))
            {
                PrefabUtility.UnpackPrefabInstance(go, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            }
            else
            {
                foreach (Transform child in go.transform)
                {
                    UnpackAll(child.gameObject);
                }
            }
        }

        static void GatherAssetsForBuild(HashSet<string> assetsForBuild, HashSet<string> assetsNotForBuild, string target)
        {
            if (assetsForBuild.Contains(target) || assetsNotForBuild.Contains(target))
            {
                return;
            }
            var type = AssetDatabase.GetMainAssetTypeAtPath(target);
            if (type == typeof(SceneAsset) || type == typeof(MonoScript))
            {
                assetsNotForBuild.Add(target);
            }
            else
            {
                assetsForBuild.Add(target);
                var dependencies = type == typeof(GameObject) ? GetPrefabDependencies(target) : AssetDatabase.GetDependencies(target, false);
                foreach (var dependency in dependencies)
                {
                    GatherAssetsForBuild(assetsForBuild, assetsNotForBuild, dependency);
                }
            }
        }

        static IEnumerable<string> GetPrefabDependencies(string target)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(target);
            var tempPath = $"Assets/{Guid.NewGuid()}.prefab";
            var instance = UnityEngine.Object.Instantiate(prefab);
            try
            {
                PrefabUtility.SaveAsPrefabAsset(instance, tempPath);
                try
                {
                    return AssetDatabase.GetDependencies(tempPath, false);
                }
                finally
                {
                    AssetDatabase.DeleteAsset(tempPath);
                }
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(instance);
            }
        }
    }
}
