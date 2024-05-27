using System;
using System.IO;
using System.Linq;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class SceneBuildDependenciesCollector
    {
        public static string[] Collect(string sceneAssetPath, BuildTarget target)
        {
            var lastBuildReportPath = "Library/LastBuild.buildreport";
            var lastWriteTime = File.GetLastWriteTimeUtc(lastBuildReportPath);

            BuildAndCleanUp(sceneAssetPath, target);

            if (File.GetLastWriteTimeUtc(lastBuildReportPath) == lastWriteTime)
            {
                throw new Exception(TranslationUtility.GetMessage(TranslationTable.cck_last_build_report_not_updated, lastBuildReportPath));
            }

            var copiedBuildReportPath = AssetDatabase.GenerateUniqueAssetPath($"Assets/LastBuild.buildreport");
            CopyAndImport(lastBuildReportPath, copiedBuildReportPath);
            try
            {
                var buildReport = AssetDatabase.LoadAssetAtPath<BuildReport>(copiedBuildReportPath);
                try
                {
                    return GetAssetsForBuild(buildReport);
                }
                finally
                {
                    Resources.UnloadAsset(buildReport);
                }
            }
            finally
            {
                AssetDatabase.DeleteAsset(copiedBuildReportPath);
            }
        }

        static void BuildAndCleanUp(string assetPath, BuildTarget target)
        {
            var assetBundleName = "BuildForCollectDependency";
            var assetBundleBuild = new AssetBundleBuild
            {
                assetBundleName = assetBundleName,
                assetNames = new[] { assetPath },
            };

            var outputPath = $"{Application.temporaryCachePath}/{new Guid()}";
            var options = BuildAssetBundleOptions.ForceRebuildAssetBundle;
            Directory.CreateDirectory(outputPath);
            BuildPipeline.BuildAssetBundles(outputPath, new[] { assetBundleBuild }, options, target);
            Directory.Delete(outputPath, true);
        }

        static string[] GetAssetsForBuild(BuildReport buildReport)
        {
            return buildReport.packedAssets
                .Where(p => Path.GetExtension(p.shortPath) == ".sharedAssets")
                .SelectMany(p => p.contents)
                .Where(c => !string.IsNullOrEmpty(c.sourceAssetPath))
                .Where(IsAssetForBuild)
                .Select(c => c.sourceAssetPath)
                .Distinct()
                .ToArray();
        }

        static void CopyAndImport(string src, string dest)
        {
            File.Copy(src, dest);
            try
            {
                AssetDatabase.ImportAsset(dest);
            }
            catch (Exception)
            {
                File.Delete(dest);
                throw;
            }
        }

        static bool IsAssetForBuild(PackedAssetInfo packedAssetInfo)
        {
            var path = packedAssetInfo.sourceAssetPath;
            if (string.IsNullOrEmpty(path) || path == "Resources/unity_builtin_extra")
            {
                return false;
            }
            if (packedAssetInfo.type == typeof(MonoScript))
            {
                return false;
            }

            return true;
        }
    }
}
