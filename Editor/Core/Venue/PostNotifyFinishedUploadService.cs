using System;
using System.Collections;
using System.Text;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public class PostNotifyFinishedUploadService
    {
        readonly string accessToken;
        readonly VenueID venueId;
        readonly UploadRequestID uploadRequestId;
        readonly bool isPublish;
        readonly Action<VenueUploadRequestCompletionResponse> onSuccess;
        readonly Action<Exception> onError;

        bool isProcessing;
        public bool IsProcessing => isProcessing;

        public PostNotifyFinishedUploadService(
            string accessToken,
            VenueID venueId,
            UploadRequestID uploadRequestId,
            bool isPublish,
            Action<VenueUploadRequestCompletionResponse> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.venueId = venueId;
            this.uploadRequestId = uploadRequestId;
            this.isPublish = isPublish;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run()
        {
            EditorCoroutine.Start(PostNotifyFinishedUpload());
        }

        IEnumerator PostNotifyFinishedUpload()
        {
            isProcessing = true;
            // 互換性のためRequestParameterとBody両方に入れる
            var notifyFinishedUrl = $"{Constants.VenueApiBaseUrl}/v1/venues/{venueId.Value}/upload/{uploadRequestId.Value}/done?isPublish={isPublish}";
            var notifyFinishedRequst = ClusterApiUtil.CreateUnityWebRequest(accessToken, notifyFinishedUrl,UnityWebRequest.kHttpVerbPOST);
            notifyFinishedRequst.downloadHandler = new DownloadHandlerBuffer();
            var payload = new PostNotifyFinishedUploadPayload {isPublish = isPublish};
            notifyFinishedRequst.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(payload)));

            notifyFinishedRequst.SendWebRequest();

            while (!notifyFinishedRequst.isDone)
            {
                yield return null;
            }

            if (notifyFinishedRequst.isNetworkError)
            {
                HandleError(new Exception(notifyFinishedRequst.error));
                yield break;
            }
            if (notifyFinishedRequst.isHttpError)
            {
                HandleError(new Exception(notifyFinishedRequst.downloadHandler.text));
                yield break;
            }

            try
            {
                Debug.Log(notifyFinishedRequst.downloadHandler.text);
                var response = JsonUtility.FromJson<VenueUploadRequestCompletionResponse>(notifyFinishedRequst.downloadHandler.text);
                onSuccess?.Invoke(response);
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
