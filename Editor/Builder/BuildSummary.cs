using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public sealed class BuildSummary
    {
        public readonly IReadOnlyList<PlatformBuildSummary> PlatformSummaries;

        BuildSummary(IReadOnlyList<PlatformBuildSummary> platformSummaries)
        {
            PlatformSummaries = platformSummaries;
        }

        public static BuildSummary FromExportedAssetInfo(ExportedAssetInfo exportedAssetInfo)
        {
            return new BuildSummary(exportedAssetInfo.PlatformInfos
                .Select(platformInfo => TryGetPlatformSummary(platformInfo, out var summary) ? summary : null)
                .Where(summary => summary != null)
                .ToList());
        }

        static bool TryGetPlatformSummary(ExportedPlatformAssetInfo info, out PlatformBuildSummary summary)
        {
            if (!TryGetMainSceneSummary(info.MainSceneInfo, info.VenueAssetInfos, out var mainSceneSummary))
            {
                summary = default;
                return false;
            }

            var subSceneSummaries = info.SubSceneInfos
                .Select(subSceneInfo => TryGetSubSceneSummary(subSceneInfo, out var summary) ? summary : null)
                .Where(summary => summary != null)
                .ToList();

            summary = new PlatformBuildSummary(info.BuildTarget, mainSceneSummary, subSceneSummaries);
            return true;
        }

        static bool TryGetMainSceneSummary(ExportedSceneInfo sceneInfo, IReadOnlyList<ExportedVenueAssetInfo> venueAssetInfos, out SceneBuildSummary summary)
        {
            if (!TryGetFileSize(sceneInfo.BuiltAssetBundlePath, out var sceneSize))
            {
                summary = default;
                return false;
            }

            long totalVenueAssetSize = 0;
            foreach (var assetIdDependsOn in sceneInfo.AssetIdsDependsOn)
            {
                var asset = venueAssetInfos.FirstOrDefault(i => i.Id == assetIdDependsOn);
                if (asset != null && TryGetFileSize(asset.BuiltAssetBundlePath, out var venueAssetSize))
                {
                    totalVenueAssetSize += venueAssetSize;
                }
            }

            summary = new SceneBuildSummary(sceneSize, totalVenueAssetSize);
            return true;
        }

        static bool TryGetSubSceneSummary(ExportedSceneInfo subScene, out SceneBuildSummary summary)
        {
            if (!TryGetFileSize(subScene.BuiltAssetBundlePath, out var size))
            {
                summary = default;
                return false;
            }
            summary = new SceneBuildSummary(size, 0);
            return true;
        }

        static bool TryGetFileSize(string path, out long size)
        {
            if (!File.Exists(path))
            {
                size = default;
                return false;
            }
            size = new FileInfo(path).Length;
            return true;
        }
    }

    public sealed class PlatformBuildSummary
    {
        public readonly BuildTarget BuildTarget;
        public readonly SceneBuildSummary MainSceneSummary;
        public readonly IReadOnlyList<SceneBuildSummary> SubSceneSummaries;

        public PlatformBuildSummary(BuildTarget buildTarget, SceneBuildSummary mainSceneSummary, IReadOnlyList<SceneBuildSummary> subSceneSummaries)
        {
            BuildTarget = buildTarget;
            MainSceneSummary = mainSceneSummary;
            SubSceneSummaries = subSceneSummaries;
        }
    }

    public sealed class SceneBuildSummary
    {
        readonly long SceneSize;
        readonly long VenueAssetSize;

        public long TotalSize => SceneSize + VenueAssetSize;

        public SceneBuildSummary(long sceneSize, long venueAssetSize)
        {
            SceneSize = sceneSize;
            VenueAssetSize = venueAssetSize;
        }
    }
}
