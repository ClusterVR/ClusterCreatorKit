using System;
using System.Collections;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public class PostUploadRequestService
    {
        readonly string accessToken;
        readonly VenueID venueId;
        readonly Action<UploadRequest> onSuccess;
        readonly Action<Exception> onError;

        bool isProcessing;
        public bool IsProcessing => isProcessing;

        public PostUploadRequestService(
            string accessToken,
            VenueID venueId,
            Action<UploadRequest> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venueId = venueId;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run()
        {
            EditorCoroutine.Start(PostUploadRequest());
        }

        IEnumerator PostUploadRequest()
        {
            isProcessing = true;
            var postUploadRequestUrl = $"{Constants.VenueApiBaseUrl}/v1/venues/{venueId.Value}/upload/new";
            var postUploadRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, postUploadRequestUrl,UnityWebRequest.kHttpVerbPOST);
            postUploadRequest.downloadHandler = new DownloadHandlerBuffer();

            postUploadRequest.SendWebRequest();

            while (!postUploadRequest.isDone)
            {
                yield return null;
            }

            if (postUploadRequest.isNetworkError)
            {
                HandleError(new Exception(postUploadRequest.error));
                yield break;
            }
            if (postUploadRequest.isHttpError)
            {
                HandleError(new Exception(postUploadRequest.downloadHandler.text));
                yield break;
            }

            try
            {
                Debug.Log(postUploadRequest.downloadHandler.text);
                var uploadId = JsonUtility.FromJson<UploadRequest>(postUploadRequest.downloadHandler.text);
                onSuccess?.Invoke(uploadId);
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

        void HandleError(Exception e)
        {
            Debug.LogException(e);
            onError?.Invoke(e);
            isProcessing = false;
        }
    }
}
