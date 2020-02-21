using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public class PatchVenueSettingService
    {
        readonly string accessToken;
        readonly VenueID venueId;
        readonly PatchVenuePayload payload;
        readonly string thumbnailImagePath;
        readonly Action<Json.Venue> onSuccess;
        readonly Action<Exception> onError;

        bool isProcessing;
        public bool IsProcessing => isProcessing;

        /// thumbnailImagePath = "" のとき: payload.thumbnailUrlsが使われる
        /// thumbnailImagePath != "" のとき: 新しくアップロードされてpayload.thumbnailUrlsは無視される
        public PatchVenueSettingService(
            string accessToken,
            VenueID venueId,
            PatchVenuePayload payload,
            string thumbnailImagePath = "",
            Action<Json.Venue> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venueId = venueId;
            this.payload = payload;
            this.thumbnailImagePath = thumbnailImagePath;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run()
        {
            EditorCoroutine.Start(PatchVenue());
        }

        bool isUploading;

        IEnumerator PatchVenue()
        {
            isProcessing = true;
            isUploading = false;

            if (!string.IsNullOrEmpty(thumbnailImagePath))
            {
                var uploadThumbnail = new UploadThumbnailService(
                    accessToken,
                    thumbnailImagePath,
                    policy =>
                    {
                        isUploading = false;
                        payload.thumbnailUrls = new List<ThumbnailUrl>() {new ThumbnailUrl(policy.url)};
                    },
                    exception =>
                    {
                        isUploading = false;
                        HandleError(exception);
                    }
                );
                isUploading = true;
                uploadThumbnail.Run();

                while (isUploading)
                {
                    yield return null;
                }

                if (!isProcessing)
                {
                    yield break;
                }
            }

            EditorCoroutine.Start(PatchVenueCore());
        }


        IEnumerator PatchVenueCore()
        {
            var patchVenueUrl = $"{Constants.VenueApiBaseUrl}/v1/venues/{venueId.Value}";
            var postVenueRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, patchVenueUrl, "PATCH");
            postVenueRequest.downloadHandler = new DownloadHandlerBuffer();
            postVenueRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(payload)));

            postVenueRequest.SendWebRequest();

            while (!postVenueRequest.isDone)
            {
                yield return null;
            }

            if (postVenueRequest.isNetworkError)
            {
                HandleError(new Exception(postVenueRequest.error));
                yield break;
            }

            if (postVenueRequest.isHttpError)
            {
                HandleError(new Exception(postVenueRequest.downloadHandler.text));
                yield break;
            }

            try
            {
                Debug.Log(postVenueRequest.downloadHandler.text);
                var venue = JsonUtility.FromJson<Json.Venue>(postVenueRequest.downloadHandler.text);
                onSuccess?.Invoke(venue);
            }
            catch (Exception e)
            {
                HandleError(e);
            }

            isProcessing = false;
        }

        void HandleError(Exception e)
        {
            Debug.LogException(e);
            onError?.Invoke(e);
            isProcessing = false;
        }
    }
}
