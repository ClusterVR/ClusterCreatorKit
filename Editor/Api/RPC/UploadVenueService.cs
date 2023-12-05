using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Proto;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public enum UploadState
    {
        PreProcess,
        Windows,
        Mac,
        Android,
        IOS,
        PostProcess
    }

    public sealed class UploadVenueService
    {
        readonly string accessToken;
        readonly Action<Exception> onError;

        bool isProcessing;
        public bool IsProcessing => isProcessing;

        readonly Action<VenueUploadRequestCompletionResponse> onSuccess;

        readonly Dictionary<UploadState, bool> uploadStatus;
        public IDictionary<UploadState, bool> UploadStatus => uploadStatus;
        readonly Venue.Venue venue;

        readonly WorldDescriptor worldDescriptor;
        readonly bool isBeta;

        VenueUploadRequestCompletionResponse completionResponse;

        UploadRequestID uploadRequestId;

        public UploadVenueService(
            string accessToken,
            Venue.Venue venue,
            WorldDescriptor worldDescriptor,
            bool isBeta,
            Action<VenueUploadRequestCompletionResponse> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venue = venue;
            this.worldDescriptor = worldDescriptor;
            this.isBeta = isBeta;
            this.onSuccess = onSuccess;
            this.onError = onError;

            uploadStatus = new Dictionary<UploadState, bool>
            {
                { UploadState.PreProcess, false },
                { UploadState.Windows, false },
                { UploadState.Mac, false },
                { UploadState.Android, false },
                { UploadState.IOS, false },
                { UploadState.PostProcess, false }
            };
        }

        public void Run(CancellationToken cancellationToken)
        {
            if (!ValidatePlatformVenue(BuildTarget.StandaloneWindows))
            {
                return;
            }

            if (!ValidatePlatformVenue(BuildTarget.StandaloneOSX))
            {
                return;
            }

            if (!ValidatePlatformVenue(BuildTarget.Android))
            {
                return;
            }

            if (!ValidatePlatformVenue(BuildTarget.iOS))
            {
                return;
            }

            _ = UploadVenueAsync(cancellationToken);
        }

        bool ValidatePlatformVenue(BuildTarget target)
        {
            return ValidateMainSceneAssetBundle(target) && ValidateSubSceneAssetBundles(target) && ValidateVenueAssetBundles(target);
        }

        bool ValidateMainSceneAssetBundle(BuildTarget target)
        {
            var assetBundlePath = BuiltAssetBundlePaths.instance.FindMainScene(target);
            if (!File.Exists(assetBundlePath?.Path))
            {
                onError?.Invoke(new FileNotFoundException($"{target.DisplayName()} Main Scene Build"));
                return false;
            }

            if (!ValidateAssetDependsOn(target, assetBundlePath))
            {
                return false;
            }

            return true;
        }

        bool ValidateSubSceneAssetBundles(BuildTarget target)
        {
            var assetBundlePaths = BuiltAssetBundlePaths.instance.SelectBuildTargetAssetBundlePaths(target)
                .Where(x => x.SceneType == AssetSceneType.Sub);
            foreach (var assetBundlePath in assetBundlePaths)
            {
                if (!File.Exists(assetBundlePath.Path))
                {
                    onError?.Invoke(
                        new FileNotFoundException($"{target.DisplayName()} Sub Scene Build: ${assetBundlePath.Path}"));
                    return false;
                }

                if (!ValidateAssetDependsOn(target, assetBundlePath))
                {
                    return false;
                }
            }

            return true;
        }

        bool ValidateAssetDependsOn(BuildTarget target, AssetBundlePath assetBundlePath)
        {
            var assetIdsDependsOn = assetBundlePath.AssetIdsDependsOn;
            if (assetIdsDependsOn == null)
            {
                return true;
            }
            foreach (var assetIdDependsOn in assetIdsDependsOn)
            {
                if (!BuiltAssetBundlePaths.instance.SelectBuildTargetVenueAssetPaths(target).Any(asset => asset.Path.EndsWith(assetIdDependsOn, StringComparison.Ordinal)))
                {
                    onError?.Invoke(new Exception($"{target.DisplayName()} Venue Asset is missing: ${assetIdDependsOn}"));
                    return false;
                }
            }
            return true;
        }

        bool ValidateVenueAssetBundles(BuildTarget target)
        {
            var venueAssetPaths = BuiltAssetBundlePaths.instance.SelectBuildTargetVenueAssetPaths(target);
            foreach (var venueAssetPath in venueAssetPaths)
            {
                if (!File.Exists(venueAssetPath.Path))
                {
                    onError?.Invoke(new FileNotFoundException($"{target.DisplayName()} Venue Asset Build: ${venueAssetPath.Path}"));
                    return false;
                }
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
                Debug.Log($"make new upload request, Request ID : {uploadRequestRespose.UploadRequestId}");
                uploadRequestId = uploadRequestRespose.UploadRequestId;
                uploadStatus[UploadState.PreProcess] = true;

                await Task.WhenAll(new[]
                {
                    UploadAssetBundlesAsync(BuildTarget.StandaloneWindows, UploadState.Windows, cancellationToken),
                    UploadAssetBundlesAsync(BuildTarget.StandaloneOSX, UploadState.Mac, cancellationToken),
                    UploadAssetBundlesAsync(BuildTarget.Android, UploadState.Android, cancellationToken),
                    UploadAssetBundlesAsync(BuildTarget.iOS, UploadState.IOS, cancellationToken)
                });

                var notifyFinishedRequest = new PostNotifyFinishedUploadService(accessToken);
                completionResponse = await notifyFinishedRequest.PostNotifyFinishedUploadAsync(
                    venue.VenueId, uploadRequestId, worldDescriptor, cancellationToken);

                Debug.Log($"notify finished upload request, Request ID : {completionResponse.UploadRequestId}");
                uploadRequestId = null;
                uploadStatus[UploadState.PostProcess] = true;

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

        async Task UploadAssetBundlesAsync(BuildTarget target, UploadState state, CancellationToken cancellationToken)
        {
            var buildTargetName = target.DisplayName();
            var assetUploadService = new UploadAssetService(accessToken);
            foreach (var assetBundlePath in BuiltAssetBundlePaths.instance.SelectBuildTargetAssetBundlePaths(target))
            {
                var policy = await assetUploadService.UploadAsync(assetBundlePath, uploadRequestId, cancellationToken);
                Debug.Log($"success {buildTargetName} {assetBundlePath.SceneType} scene asset upload, uploaded url : {policy.uploadUrl}");
            }
            foreach (var venueAssetPath in BuiltAssetBundlePaths.instance.SelectBuildTargetVenueAssetPaths(target))
            {
                var policy = await assetUploadService.UploadAsync(venueAssetPath, uploadRequestId, cancellationToken);
                Debug.Log($"success {buildTargetName} venue asset upload, uploaded url : {policy.uploadUrl}");
            }
            uploadStatus[state] = true;
        }

        void HandleError(Exception e)
        {
            Debug.LogException(e);
            isProcessing = false;
            onError?.Invoke(e);
        }
    }
}
