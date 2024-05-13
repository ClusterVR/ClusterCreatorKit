using System.Collections.Generic;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public sealed class ExportedAssetInfo
    {
        public readonly IReadOnlyList<ExportedPlatformAssetInfo> PlatformInfos;

        public ExportedAssetInfo(IReadOnlyList<ExportedPlatformAssetInfo> platformAssetInfos)
        {
            PlatformInfos = platformAssetInfos;
        }
    }

    public sealed class ExportedPlatformAssetInfo
    {
        public readonly BuildTarget BuildTarget;
        public readonly ExportedSceneInfo MainSceneInfo;
        public readonly IReadOnlyList<ExportedSceneInfo> SubSceneInfos;
        public readonly IReadOnlyList<ExportedVenueAssetInfo> VenueAssetInfos;

        public ExportedPlatformAssetInfo(BuildTarget buildTarget, ExportedSceneInfo mainSceneInfo, IReadOnlyList<ExportedSceneInfo> subSceneInfos, IReadOnlyList<ExportedVenueAssetInfo> venueAssetInfos)
        {
            BuildTarget = buildTarget;
            MainSceneInfo = mainSceneInfo;
            SubSceneInfos = subSceneInfos;
            VenueAssetInfos = venueAssetInfos;
        }
    }

    public sealed class ExportedSceneInfo
    {
        public readonly string BuiltAssetBundlePath;
        public readonly string[] AssetIdsDependsOn;

        public ExportedSceneInfo(string builtAssetBundlePath, string[] assetIdsDependsOn)
        {
            BuiltAssetBundlePath = builtAssetBundlePath;
            AssetIdsDependsOn = assetIdsDependsOn;
        }
    }

    public sealed class ExportedVenueAssetInfo
    {
        public readonly string Id;
        public readonly string BuiltAssetBundlePath;

        public ExportedVenueAssetInfo(string id, string builtAssetBundlePath)
        {
            Id = id;
            BuiltAssetBundlePath = builtAssetBundlePath;
        }
    }
}
