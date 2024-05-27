using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
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

        readonly ExportedAssetInfo exportedAssetInfo;

        VenueUploadRequestCompletionResponse completionResponse;

        UploadRequestID uploadRequestId;

        public UploadVenueService(
            string accessToken,
            Venue.Venue venue,
            WorldDescriptor worldDescriptor,
            bool isBeta,
            bool isPreview,
            ExportedAssetInfo exportedAssetInfo,
            Action<VenueUploadRequestCompletionResponse> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venue = venue;
            this.worldDescriptor = worldDescriptor;
            this.isBeta = isBeta;
            this.isPreview = isPreview;
            this.onSuccess = onSuccess;
            this.onError = onError;
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
            foreach (var platformInfo in exportedAssetInfo.PlatformInfos)
            {
                if (!ValidatePlatformVenue(platformInfo))
                {
                    return;
                }
            }

            _ = UploadVenueAsync(cancellationToken);
        }

        bool ValidatePlatformVenue(ExportedPlatformAssetInfo platformAssetInfo)
        {
            {
                if (!ValidateSceneAssetBundle(platformAssetInfo.MainSceneInfo, true, platformAssetInfo.VenueAssetInfos, platformAssetInfo.BuildTarget))
                {
                    return false;
                }
            }
            foreach (var subSceneInfo in platformAssetInfo.SubSceneInfos)
            {
                if (!ValidateSceneAssetBundle(subSceneInfo, false, platformAssetInfo.VenueAssetInfos, platformAssetInfo.BuildTarget))
                {
                    return false;
                }
            }
            foreach (var venueAssetInfo in platformAssetInfo.VenueAssetInfos)
            {
                if (!ValidateVenueAssetBundle(venueAssetInfo, platformAssetInfo.BuildTarget))
                {
                    return false;
                }
            }
            return true;
        }

        bool ValidateSceneAssetBundle(ExportedSceneInfo sceneInfo, bool isMainScene, IReadOnlyList<ExportedVenueAssetInfo> venueAssetInfos, BuildTarget target)
        {
            var assetBundlePath = sceneInfo.BuiltAssetBundlePath;
            if (!File.Exists(assetBundlePath))
            {
                var message = isMainScene ?
                    TranslationUtility.GetMessage(TranslationTable.cck_main_scene_build, target.DisplayName()) :
                    TranslationUtility.GetMessage(TranslationTable.cck_sub_scene_build, target.DisplayName(), assetBundlePath);
                onError?.Invoke(new FileNotFoundException(message));
                return false;
            }

            foreach (var assetIdDependsOn in sceneInfo.AssetIdsDependsOn)
            {
                if (!venueAssetInfos.Select(i => i.Id).Contains(assetIdDependsOn))
                {
                    var message = TranslationUtility.GetMessage(TranslationTable.cck_venue_asset_missing, target.DisplayName(), assetIdDependsOn);
                    onError?.Invoke(new Exception(message));
                    return false;
                }
            }

            return true;
        }

        bool ValidateVenueAssetBundle(ExportedVenueAssetInfo venueAssetInfo, BuildTarget target)
        {
            var assetBundlePath = venueAssetInfo.BuiltAssetBundlePath;
            if (!File.Exists(assetBundlePath))
            {
                var message = TranslationUtility.GetMessage(TranslationTable.cck_venue_asset_build, target.DisplayName(), assetBundlePath);
                onError?.Invoke(new FileNotFoundException(message));
                return false;
            }
            return true;
        }

        async Task UploadVenueAsync(CancellationToken cancellationToken)
        {
            isProcessing = true;

            try
            {
                var uploadRequest = new PostUploadRequestService(accessToken, isBeta);
                var uploadRequestRespose = await uploadRequest.PostUploadRequestAsync(venue.VenueId, cancellationToken);
                Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_upload_request, uploadRequestRespose.UploadRequestId));
                uploadRequestId = uploadRequestRespose.UploadRequestId;
                uploadStatus[UploadPhase.PreProcess] = true;

                await Task.WhenAll(
                    exportedAssetInfo.PlatformInfos.Select(platformInfo => UploadAssetBundlesAsync(platformInfo, cancellationToken)));

                var notifyFinishedRequest = new PostNotifyFinishedUploadService(accessToken);
                completionResponse = await notifyFinishedRequest.PostNotifyFinishedUploadAsync(
                    venue.VenueId, uploadRequestId, worldDescriptor, isPreview, cancellationToken);

                Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_notify_upload_finished, completionResponse.UploadRequestId));
                uploadRequestId = null;
                uploadStatus[UploadPhase.PostProcess] = true;

                onSuccess?.Invoke(completionResponse);
            }
            catch (Exception e)
            {
                HandleError(e);
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

        void HandleError(Exception e)
        {
            Debug.LogException(e);
            isProcessing = false;
            onError?.Invoke(e);
        }
    }
}
