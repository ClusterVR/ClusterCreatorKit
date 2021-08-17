using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.MenuItems
{
    public static class MirrorMenuItem
    {
        [MenuItem("GameObject/3D Object/Mirror - cluster")]
        static void CreateMirror()
        {
            var mirrorPrefab =
                AssetDatabase.LoadAssetAtPath<GameObject>(
                    "Packages/mu.cluster.cluster-creator-kit/PackageResources/Prefabs/Mirror.prefab");
            var mirror = Object.Instantiate(mirrorPrefab, MenuItemUtilities.GetActiveContentsRoot());
            mirror.name = mirrorPrefab.name;
            Selection.activeGameObject = mirror;
        }
    }
}
