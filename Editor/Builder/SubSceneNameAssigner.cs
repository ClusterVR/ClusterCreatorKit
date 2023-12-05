using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class SubSceneNameAssigner
    {
        public static void Execute()
        {
            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();
            var hashSet = new HashSet<string>();
            foreach (var subScene in rootObjects.SelectMany(o => o.GetComponentsInChildren<World.SubScene>(true)))
            {
                while (string.IsNullOrEmpty(subScene.SceneName) || hashSet.Contains(subScene.SceneName))
                {
                    var subSceneName = GenerateSubSceneName();
                    subScene.SceneName = subSceneName;
                    if (!Application.isPlaying)
                    {
                        EditorUtility.SetDirty(subScene);
                        EditorSceneManager.MarkSceneDirty(scene);
                    }
                }

                hashSet.Add(subScene.SceneName);
            }
        }

        static string GenerateSubSceneName()
        {
            return "sub_" + Guid.NewGuid().ToString();
        }
    }
}
