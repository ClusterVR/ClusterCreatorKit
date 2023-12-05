using System;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class BuildTargetExtensions
    {
        public static string DisplayName(this BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                    return "Windows";
                case BuildTarget.StandaloneOSX:
                    return "Mac";
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                default:
                    throw new Exception($"{target} はサポート外のビルドターゲットです");
            }
        }

        public static string GetFileType(this BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                    return "assetbundle/win";
                case BuildTarget.StandaloneOSX:
                    return "assetbundle/mac";
                case BuildTarget.Android:
                    return "assetbundle/android";
                case BuildTarget.iOS:
                    return "assetbundle/ios";
                default:
                    throw new Exception($"{target} はサポート外のビルドターゲットです");
            }
        }
    }
}
