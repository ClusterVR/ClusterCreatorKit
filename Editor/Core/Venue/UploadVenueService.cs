using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public enum UploadState
    {
        PreProcess,
        Windows,
        Mac,
        Android,
        IOS,
//        UnityPackage,
        PostProcess,
    }

    /// <summary>
    /// Notice: UnityPackageが2GBを超えることがあり、現状のアップロード方法だとそこまで対応できていないため一旦Uploadをやめる
    /// ToDo: こっちで対応 https://cluster.atlassian.net/browse/CLUSTER-3523
    /// </summary>
    public class UploadVenueService
    {
        readonly string accessToken;
        readonly Json.Venue venue;
        readonly Action<VenueUploadRequestCompletionResponse> onSuccess;
        readonly Action<Exception> onError;

        bool isProcessing;
        public bool IsProcessing => isProcessing;

        UploadRequestID uploadRequestId;

        readonly Dictionary<UploadState, bool> uploadStatus;
        public IDictionary<UploadState, bool> UploadStatus => uploadStatus;

        VenueUploadRequestCompletionResponse completionResponse;

        public UploadVenueService(
            string accessToken,
            Json.Venue venue,
            Action<VenueUploadRequestCompletionResponse> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venue = venue;
            this.onSuccess = onSuccess;
            this.onError = onError;

            // TODO: Setに書き換え
            uploadStatus = new Dictionary<UploadState, bool>
            {
                {UploadState.PreProcess, false},
                {UploadState.Windows, false},
                {UploadState.Mac, false},
                {UploadState.Android, false},
                {UploadState.IOS, false},
//                {UploadState.UnityPackage, false},
                {UploadState.PostProcess, false}
            };
        }

        public void Run()
        {
            if (!File.Exists(EditorPrefsUtils.LastBuildWin))
            {
                onError?.Invoke(new FileNotFoundException("Windows Build"));
                return;
            }

            if (!File.Exists(EditorPrefsUtils.LastBuildMac))
            {
                onError?.Invoke(new FileNotFoundException("Mac Build"));
                return;
            }

            if (!File.Exists(EditorPrefsUtils.LastBuildAndroid))
            {
                onError?.Invoke(new FileNotFoundException("Android Build"));
                return;
            }

            if (!File.Exists(EditorPrefsUtils.LastBuildIOS))
            {
                onError?.Invoke(new FileNotFoundException("iOS Build"));
                return;
            }

            // if (!File.Exists(EditorPrefsUtils.LastExportPackage))
            // {
            //     onError?.Invoke(new FileNotFoundException("UnityPackage Export"));
            //     return;
            // }

            EditorCoroutine.Start(UploadVenue());
        }

        IEnumerator UploadVenue()
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
                uploadRequest.Run();

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
//            var unityPackageUploadProcess = uploadStatus[UploadState.UnityPackage];

            if (!uploadStatus[UploadState.Windows])
            {
                var winAssetUploadService = new UploadAssetService(
                    accessToken,
                    "assetbundle/win",
                    EditorPrefsUtils.LastBuildWin,
                    uploadRequestId,
                    request =>
                    {
                        Debug.Log($"success win asset upload, uploaded url : {request.url}");
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
                    EditorPrefsUtils.LastBuildMac,
                    uploadRequestId,
                    request =>
                    {
                        Debug.Log($"success mac asset upload, uploaded url : {request.url}");
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
                    EditorPrefsUtils.LastBuildAndroid,
                    uploadRequestId,
                    request =>
                    {
                        Debug.Log($"success android asset upload, uploaded url : {request.url}");
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
                    EditorPrefsUtils.LastBuildIOS,
                    uploadRequestId,
                    request =>
                    {
                        Debug.Log($"success ios asset upload, uploaded url : {request.url}");
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

            // if (!uploadStatus[UploadState.UnityPackage])
            // {
            //     var unityPackageUploadService = new UploadAssetService(
            //         accessToken,
            //         "unitypackage",
            //         EditorPrefsUtils.LastExportPackage,
            //         uploadRequestId,
            //         request =>
            //         {
            //             Debug.Log($"success unitypackage upload, uploaded url : {request.url}");
            //             unityPackageUploadProcess = true;
            //             uploadStatus[UploadState.UnityPackage] = true;
            //         },
            //         exception =>
            //         {
            //             HandleError(exception);
            //             unityPackageUploadProcess = true;
            //         }
            //     );
            //     uploadAssetServiceList.Add(unityPackageUploadService);
            // }

            uploadAssetServiceList.ForEach(x => x.Run());

            //while (!winAssetUploadProcess || !macAssetUploadProcess || !androidAssetUploadProcess || !unityPackageUploadProcess)
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
                    request =>
                    {
                        Debug.Log($"notify finished upload request, Request ID : {request.UploadRequestId}");
                        uploadRequestId = null;
                        postNotifyFinishProcess = true;
                        uploadStatus[UploadState.PostProcess] = true;
                        completionResponse = request;
                    },
                    exception =>
                    {
                        HandleError(exception);
                        postNotifyFinishProcess = true;
                    }
                );
                notifyFinishedRequest.Run();

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
