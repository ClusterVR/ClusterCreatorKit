using System;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    [Serializable]
    public sealed class VenueAssetPath
    {
        [SerializeField] public BuildTarget Target;
        [SerializeField] public string Path;

        public string FileType => Target.GetFileType();
    }
}
