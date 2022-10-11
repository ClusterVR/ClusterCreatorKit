using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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

        VenueUploadRequestCompletionResponse completionResponse;

        UploadRequestID uploadRequestId;

        public UploadVenueService(
            string accessToken,
            Venue.Venue venue,
            WorldDescriptor worldDescriptor,
            Action<VenueUploadRequestCompletionResponse> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venue = venue;
            this.worldDescriptor = worldDescriptor;
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
            if (!File.Exists(BuiltAssetBundlePaths.instance.Find(BuildTarget.StandaloneWindows)))
            {
                onError?.Invoke(new FileNotFoundException("Windows Build"));
                return;
            }

            if (!File.Exists(BuiltAssetBundlePaths.instance.Find(BuildTarget.StandaloneOSX)))
            {
                onError?.Invoke(new FileNotFoundException("Mac Build"));
                return;
            }

            if (!File.Exists(BuiltAssetBundlePaths.instance.Find(BuildTarget.Android)))
            {
                onError?.Invoke(new FileNotFoundException("Android Build"));
                return;
            }

            if (!File.Exists(BuiltAssetBundlePaths.instance.Find(BuildTarget.iOS)))
            {
                onError?.Invoke(new FileNotFoundException("iOS Build"));
                return;
            }

            EditorCoroutine.Start(UploadVenue(cancellationToken));
        }

        IEnumerator UploadVenue(CancellationToken cancellationToken)
        {
            isProcessing = true;

            if (!uploadStatus[UploadState.PreProcess])
            {
                var postUploadRequestProcess = false;
                var uploadRequest = new PostUploadRequestService(
                    accessToken,
                    venue.VenueId,
                    request =>
                    {
                        Debug.Log($"make new upload request, Request ID : {request.UploadRequestId}");
                        uploadRequestId = request.UploadRequestId;
                        postUploadRequestProcess = true;
                        uploadStatus[UploadState.PreProcess] = true;
                    },
                    exception =>
                    {
                        HandleError(exception);
                        postUploadRequestProcess = true;
                    }
                );
                uploadRequest.Run(cancellationToken);

                while (!postUploadRequestProcess)
                {
                    yield return null;
                }
            }

            if (!uploadStatus[UploadState.PreProcess])
            {
                yield break;
            }

            var uploadAssetServiceList = new List<UploadAssetService>();

            var winAssetUploadProcess = uploadStatus[UploadState.Windows];
            var macAssetUploadProcess = uploadStatus[UploadState.Mac];
            var androidAssetUploadProcess = uploadStatus[UploadState.Android];
            var iosAssetUploadProcess = uploadStatus[UploadState.IOS];

            if (!uploadStatus[UploadState.Windows])
            {
                var winAssetUploadService = new UploadAssetService(
                    accessToken,
                    "assetbundle/win",
                    BuiltAssetBundlePaths.instance.Find(BuildTarget.StandaloneWindows),
                    uploadRequestId,
                    request =>
                    {
                        Debug.Log($"success win asset upload, uploaded url : {request.uploadUrl}");
                        winAssetUploadProcess = true;
                        uploadStatus[UploadState.Windows] = true;
                    },
                    exception =>
                    {
                        HandleError(exception);
                        winAssetUploadProcess = true;
                    }
                );
                uploadAssetServiceList.Add(winAssetUploadService);
            }

            if (!uploadStatus[UploadState.Mac])
            {
                var macAssetUploadService = new UploadAssetService(
                    accessToken,
                    "assetbundle/mac",
                    BuiltAssetBundlePaths.instance.Find(BuildTarget.StandaloneOSX),
                    uploadRequestId,
                    request =>
                    {
                        Debug.Log($"success mac asset upload, uploaded url : {request.uploadUrl}");
                        macAssetUploadProcess = true;
                        uploadStatus[UploadState.Mac] = true;
                    },
                    exception =>
                    {
                        HandleError(exception);
                        macAssetUploadProcess = true;
                    }
                );
                uploadAssetServiceList.Add(macAssetUploadService);
            }

            if (!uploadStatus[UploadState.Android])
            {
                var androidAssetUploadService = new UploadAssetService(
                    accessToken,
                    "assetbundle/android",
                    BuiltAssetBundlePaths.instance.Find(BuildTarget.Android),
                    uploadRequestId,
                    request =>
                    {
                        Debug.Log($"success android asset upload, uploaded url : {request.uploadUrl}");
                        androidAssetUploadProcess = true;
                        uploadStatus[UploadState.Android] = true;
                    },
                    exception =>
                    {
                        HandleError(exception);
                        androidAssetUploadProcess = true;
                    }
                );
                uploadAssetServiceList.Add(androidAssetUploadService);
            }

            if (!uploadStatus[UploadState.IOS])
            {
                var iosAssetUploadService = new UploadAssetService(
                    accessToken,
                    "assetbundle/ios",
                    BuiltAssetBundlePaths.instance.Find(BuildTarget.iOS),
                    uploadRequestId,
                    request =>
                    {
                        Debug.Log($"success ios asset upload, uploaded url : {request.uploadUrl}");
                        iosAssetUploadProcess = true;
                        uploadStatus[UploadState.IOS] = true;
                    },
                    exception =>
                    {
                        HandleError(exception);
                        iosAssetUploadProcess = true;
                    }
                );
                uploadAssetServiceList.Add(iosAssetUploadService);
            }

            uploadAssetServiceList.ForEach(x => x.Run(cancellationToken));

            while (!winAssetUploadProcess || !macAssetUploadProcess || !androidAssetUploadProcess || !iosAssetUploadProcess)
            {
                yield return null;
            }

            if (!uploadStatus[UploadState.Windows] ||
                !uploadStatus[UploadState.Mac] ||
                !uploadStatus[UploadState.Android] ||
                !uploadStatus[UploadState.IOS])
            {
                yield break;
            }

            if (!uploadStatus[UploadState.PostProcess])
            {
                var postNotifyFinishProcess = false;
                var notifyFinishedRequest = new PostNotifyFinishedUploadService(
                    accessToken,
                    venue.VenueId,
                    uploadRequestId,
                    false,
                    worldDescriptor,
                    response =>
                    {
                        Debug.Log($"notify finished upload request, Request ID : {response.UploadRequestId}");
                        uploadRequestId = null;
                        postNotifyFinishProcess = true;
                        uploadStatus[UploadState.PostProcess] = true;
                        completionResponse = response;
                    },
                    exception =>
                    {
                        HandleError(exception);
                        postNotifyFinishProcess = true;
                    }
                );
                notifyFinishedRequest.Run(cancellationToken);

                while (!postNotifyFinishProcess)
                {
                    yield return null;
                }
            }

            if (!uploadStatus[UploadState.PostProcess])
            {
                yield break;
            }

            onSuccess?.Invoke(completionResponse);
            isProcessing = false;
        }

        void HandleError(Exception e)
        {
            Debug.LogException(e);
            isProcessing = false;
            onError?.Invoke(e);
        }
    }
}
