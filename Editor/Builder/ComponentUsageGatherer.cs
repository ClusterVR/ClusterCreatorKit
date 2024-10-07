using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class ComponentUsageGatherer
    {
        static IEnumerable<GameObject> GatherRootGameObjects()
        {
            var scene = SceneManager.GetActiveScene();
            var sceneRootObjects = scene.GetRootGameObjects();
            var itemTemplates = ItemTemplateGatherer.GatherItemTemplates(scene).ToArray();
            return sceneRootObjects.Concat(itemTemplates.Select(t => t.gameObject)).ToArray();
        }

        static IEnumerable<T> GatherComponents<T>()
        {
            return GatherRootGameObjects()
                .SelectMany(x => x.GetComponentsInChildren<T>(true).Where(c => c != null));
        }

        static bool IsCreatorKitComponentType(Type type) => type.FullName?.StartsWith("ClusterVR.CreatorKit.") ?? false;

        public static Dictionary<string, int> GatherCreatorKitComponentUsage()
        {
            return GatherComponents<Component>()
                .GroupBy(c => c.GetType(), (type, components) => (Type: type, Count: components.Count()))
                .Where(kv => IsCreatorKitComponentType(kv.Type))
                .ToDictionary(kv => kv.Type.Name, kv => kv.Count);
        }
    }
}
