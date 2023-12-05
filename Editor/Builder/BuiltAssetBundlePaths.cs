using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public sealed class BuiltAssetBundlePaths : ScriptableSingleton<BuiltAssetBundlePaths>
    {
        [SerializeField] List<AssetBundlePath> assetBundlePaths = new();
        [SerializeField] List<VenueAssetPath> venueAssetPaths = new();

        public void AddOrUpdateMainScene(BuildTarget target, string path, string[] assetIdsDependsOn)
        {
            assetBundlePaths.RemoveAll(x => x.Target == target && x.SceneType == AssetSceneType.Main);
            assetBundlePaths.Add(new AssetBundlePath { Target = target, Path = path, SceneType = AssetSceneType.Main, AssetIdsDependsOn = assetIdsDependsOn });
        }

        public void AddSubScene(BuildTarget target, string path, string[] assetIdsDependsOn)
        {
            assetBundlePaths.Add(new AssetBundlePath { Target = target, Path = path, SceneType = AssetSceneType.Sub, AssetIdsDependsOn = assetIdsDependsOn });
        }

        public void AddVenueAsset(BuildTarget target, string path)
        {
            venueAssetPaths.Add(new VenueAssetPath { Target = target, Path = path });
        }

        public AssetBundlePath FindMainScene(BuildTarget target)
        {
            return assetBundlePaths.FirstOrDefault(x => x.Target == target && x.SceneType == AssetSceneType.Main);
        }

        public IEnumerable<AssetBundlePath> SelectBuildTargetAssetBundlePaths(BuildTarget target)
        {
            return assetBundlePaths.Where(x => x.Target == target);
        }

        public IEnumerable<VenueAssetPath> SelectBuildTargetVenueAssetPaths(BuildTarget target)
        {
            return venueAssetPaths.Where(x => x.Target == target);
        }

        public void ClearSubScenes()
        {
            assetBundlePaths.RemoveAll(x => x.SceneType == AssetSceneType.Sub);
        }

        public void ClearVenueAssets()
        {
            venueAssetPaths.Clear();
        }
    }
}
