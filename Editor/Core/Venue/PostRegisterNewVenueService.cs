using System;
using System.Collections;
using System.Text;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public class PostRegisterNewVenueService
    {
        readonly string accessToken;
        readonly PostNewVenuePayload payload;
        readonly Action<Json.Venue> onSuccess;
        readonly Action<Exception> onError;

        bool isProcessing;
        public bool IsProcessing => isProcessing;

        public PostRegisterNewVenueService(
            string accessToken,
            PostNewVenuePayload payload,
            Action<Json.Venue> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.payload = payload;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run()
        {
            EditorCoroutine.Start(PostVenue());
        }

        IEnumerator PostVenue()
        {
            isProcessing = true;
            var getTeamsUrl = $"{Constants.VenueApiBaseUrl}/v1/venues";
            var postVenueRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, getTeamsUrl,UnityWebRequest.kHttpVerbPOST);
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
