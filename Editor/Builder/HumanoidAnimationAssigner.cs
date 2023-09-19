using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class HumanoidAnimationAssigner
    {
        public static void Execute()
        {
            var scene = SceneManager.GetActiveScene();
            var rootGameObjects = scene.GetRootGameObjects()
                .Concat(ItemTemplateGatherer.GatherItemTemplates(scene).Select(i => i.gameObject));

            var humanoidAnimations = new Dictionary<AnimationClip, HumanoidAnimation>();

            foreach (var humanoidAnimationList in rootGameObjects.SelectMany(g => g.GetComponentsInChildren<HumanoidAnimationList>(true)))
            {
                var animations = humanoidAnimationList.RawHumanoidAnimations;
                if (animations == null)
                {
                    continue;
                }

                foreach (var animation in animations)
                {
                    var animationClip = animation.Animation;
                    if (!humanoidAnimations.TryGetValue(animationClip, out var humanoidAnimation))
                    {
                        humanoidAnimation = HumanoidAnimationBuilder.Build(animation.Animation);
                        humanoidAnimations.Add(animationClip, humanoidAnimation);
                    }
                    animation.SetHumanoidAnimation(humanoidAnimation);
                }
                if (!Application.isPlaying)
                {
                    EditorUtility.SetDirty(humanoidAnimationList);
                }
            }

            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(scene);
            }
        }
    }
}
