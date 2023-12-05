using System;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    [Serializable]
    public sealed class AssetBundlePath
    {
        [SerializeField] public BuildTarget Target;
        [SerializeField] public string Path;
        [SerializeField] public string SceneType;
        [SerializeField] public string[] AssetIdsDependsOn;

        public string FileType => Target.GetFileType();
    }
}
