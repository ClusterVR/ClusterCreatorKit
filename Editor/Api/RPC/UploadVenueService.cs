using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Proto;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public enum UploadPhase
    {
        PreProcess,
        Windows,
        Mac,
        IOS,
        Android,
        PostProcess
    }

    public sealed class UploadVenueService
    {
        readonly string accessToken;
        readonly Action<Exception> onError;

        bool isProcessing;
        public bool IsProcessing => isProcessing;

        readonly Action<VenueUploadRequestCompletionResponse> onSuccess;

        readonly Dictionary<UploadPhase, bool> uploadStatus;
        public IDictionary<UploadPhase, bool> UploadStatus => uploadStatus;
        readonly Venue.Venue venue;

        readonly WorldDescriptor worldDescriptor;
        readonly bool isBeta;
        readonly bool isPreview;
        readonly string[] productUgcIds;

        readonly ExportedAssetInfo exportedAssetInfo;

        VenueUploadRequestCompletionResponse completionResponse;

        UploadRequestID uploadRequestId;

        public UploadVenueService(
            string accessToken,
            Venue.Venue venue,
            WorldDescriptor worldDescriptor,
            bool isBeta,
            bool isPreview,
            string[] productUgcIds,
            ExportedAssetInfo exportedAssetInfo
        )
        {
            this.accessToken = accessToken;
            this.venue = venue;
            this.worldDescriptor = worldDescriptor;
            this.isBeta = isBeta;
            this.isPreview = isPreview;
            this.productUgcIds = productUgcIds;
            this.exportedAssetInfo = exportedAssetInfo;

            uploadStatus = BuildUploadStatus();
        }

        Dictionary<UploadPhase, bool> BuildUploadStatus()
        {
            var uploadStatus = new Dictionary<UploadPhase, bool>(6)
            {
                { UploadPhase.PreProcess, false },
                { UploadPhase.PostProcess, false }
            };

            foreach (var platformInfo in exportedAssetInfo.PlatformInfos)
            {
                uploadStatus[BuildTargetToPhase(platformInfo.BuildTarget)] = false;
            }
            return uploadStatus;
        }

        static UploadPhase BuildTargetToPhase(BuildTarget target) =>
            target switch
            {
                BuildTarget.StandaloneWindows => UploadPhase.Windows,
                BuildTarget.StandaloneOSX => UploadPhase.Mac,
                BuildTarget.iOS => UploadPhase.IOS,
                BuildTarget.Android => UploadPhase.Android,
                _ => throw new NotImplementedException(),
            };

        public void Run(CancellationToken cancellationToken)
        {
            RunAsync(cancellationToken).Forget();
        }

        public async Task<VenueUploadRequestCompletionResponse> RunAsync(CancellationToken cancellationToken)
        {
            foreach (var platformInfo in exportedAssetInfo.PlatformInfos)
            {
                ValidatePlatformVenue(platformInfo);
            }

            return await UploadVenueAsync(cancellationToken);
        }

        void ValidatePlatformVenue(ExportedPlatformAssetInfo platformAssetInfo)
        {
            {
                ValidateSceneAssetBundle(
                    platformAssetInfo.MainSceneInfo,
                    true,
                    platformAssetInfo.VenueAssetInfos,
                    platformAssetInfo.BuildTarget);
            }
            foreach (var subSceneInfo in platformAssetInfo.SubSceneInfos)
            {
                ValidateSceneAssetBundle(
                    subSceneInfo,
                    false,
                    platformAssetInfo.VenueAssetInfos,
                    platformAssetInfo.BuildTarget);
            }
            foreach (var venueAssetInfo in platformAssetInfo.VenueAssetInfos)
            {
                ValidateVenueAssetBundle(venueAssetInfo, platformAssetInfo.BuildTarget);
            }
        }

        static void ValidateSceneAssetBundle(ExportedSceneInfo sceneInfo, bool isMainScene, IReadOnlyList<ExportedVenueAssetInfo> venueAssetInfos, BuildTarget target)
        {
            var assetBundlePath = sceneInfo.BuiltAssetBundlePath;
            if (!File.Exists(assetBundlePath))
            {
                var message = isMainScene ?
                    TranslationUtility.GetMessage(TranslationTable.cck_main_scene_build, target.DisplayName()) :
                    TranslationUtility.GetMessage(TranslationTable.cck_sub_scene_build, target.DisplayName(), assetBundlePath);
                throw new FileNotFoundException(message);
            }

            foreach (var assetIdDependsOn in sceneInfo.AssetIdsDependsOn)
            {
                if (!venueAssetInfos.Select(i => i.Id).Contains(assetIdDependsOn))
                {
                    var message = TranslationUtility.GetMessage(TranslationTable.cck_venue_asset_missing, target.DisplayName(), assetIdDependsOn);
                    throw new Exception(message);
                }
            }
        }

        static void ValidateVenueAssetBundle(ExportedVenueAssetInfo venueAssetInfo, BuildTarget target)
        {
            var assetBundlePath = venueAssetInfo.BuiltAssetBundlePath;
            if (!File.Exists(assetBundlePath))
            {
                var message = TranslationUtility.GetMessage(TranslationTable.cck_venue_asset_build, target.DisplayName(), assetBundlePath);
                throw new FileNotFoundException(message);
            }
        }

        async Task<VenueUploadRequestCompletionResponse> UploadVenueAsync(CancellationToken cancellationToken)
        {
            isProcessing = true;

            try
            {
                var uploadRequest = new PostUploadRequestService(accessToken, isBeta, isPreview);
                var uploadRequestResponse = await uploadRequest.PostUploadRequestAsync(venue.VenueId, cancellationToken);
                Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_upload_request, uploadRequestResponse.UploadRequestId));
                uploadRequestId = uploadRequestResponse.UploadRequestId;
                uploadStatus[UploadPhase.PreProcess] = true;

                await Task.WhenAll(
                    exportedAssetInfo.PlatformInfos.Select(platformInfo => UploadAssetBundlesAsync(platformInfo, cancellationToken)));

                var notifyFinishedRequest = new PostNotifyFinishedUploadService(accessToken);
                completionResponse = await notifyFinishedRequest.PostNotifyFinishedUploadAsync(
                    venue.VenueId, uploadRequestId, worldDescriptor, isPreview, productUgcIds, cancellationToken);

                Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_notify_upload_finished, completionResponse.UploadRequestId));
                uploadRequestId = null;
                uploadStatus[UploadPhase.PostProcess] = true;

                return completionResponse;
            }
            finally
            {
                isProcessing = false;
            }
        }

        async Task UploadAssetBundlesAsync(ExportedPlatformAssetInfo platformAssetInfo, CancellationToken cancellationToken)
        {
            var target = platformAssetInfo.BuildTarget;
            var buildTargetName = target.DisplayName();
            var assetUploadService = new UploadAssetService(accessToken);
            {
                var policy = await assetUploadService.UploadAsync(platformAssetInfo.MainSceneInfo, target, true, uploadRequestId, cancellationToken);
                Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_success_upload_scene_with_url, buildTargetName, "main", policy.uploadUrl));
            }
            foreach (var subSceneInfo in platformAssetInfo.SubSceneInfos)
            {
                var policy = await assetUploadService.UploadAsync(subSceneInfo, target, false, uploadRequestId, cancellationToken);
                Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_success_upload_scene_with_url, buildTargetName, "sub", policy.uploadUrl));
            }
            foreach (var venueAssetInfo in platformAssetInfo.VenueAssetInfos)
            {
                var policy = await assetUploadService.UploadAsync(venueAssetInfo, target, uploadRequestId, cancellationToken);
                Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_success_upload_venue_with_url, buildTargetName, policy.uploadUrl));
            }
            uploadStatus[BuildTargetToPhase(target)] = true;
        }

    }
}
