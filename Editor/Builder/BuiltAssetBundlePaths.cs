using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public sealed class BuiltAssetBundlePaths : ScriptableSingleton<BuiltAssetBundlePaths>
    {
        [SerializeField] List<AssetBundlePath> assetBundlePaths = new List<AssetBundlePath>();

        public void AddOrUpdate(BuildTarget target, string path)
        {
            var assetBundlePath = assetBundlePaths.FirstOrDefault(x => x.Target == target);
            if (assetBundlePath != null)
            {
                assetBundlePath.Path = path;
            }
            else
            {
                assetBundlePaths.Add(new AssetBundlePath { Target = target, Path = path });
            }
        }

        public string Find(BuildTarget target)
        {
            return assetBundlePaths.FirstOrDefault(x => x.Target == target)?.Path;
        }

        [Serializable]
        sealed class AssetBundlePath
        {
            [SerializeField] BuildTarget target;
            [SerializeField] string path;

            public BuildTarget Target
            {
                get => target;
                set => target = value;
            }

            public string Path
            {
                get => path;
                set => path = value;
            }
        }
    }
}
