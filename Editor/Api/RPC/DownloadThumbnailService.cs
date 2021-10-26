using System;
using System.Collections;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class DownloadThumbnailService
    {
        static readonly Dictionary<ThumbnailUrl, Texture2D> cacheMap = new Dictionary<ThumbnailUrl, Texture2D>();
        readonly Action<Exception> onError;
        readonly Action<Texture2D> onSuccess;

        readonly ThumbnailUrl thumbnailUrl;

        public DownloadThumbnailService(
            ThumbnailUrl thumbnailUrl,
            Action<Texture2D> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.thumbnailUrl = thumbnailUrl;
            this.onSuccess = onSuccess;
            this.onError = onError;
        }

        public void Run()
        {
            EditorCoroutine.Start(GetVenues());
        }

        IEnumerator GetVenues()
        {
            Texture2D cache;
            if (cacheMap.TryGetValue(thumbnailUrl, out cache) && cache)
            {
                onSuccess?.Invoke(cache);
                yield break;
            }

            var downloadThumbnailRequest =
                ClusterApiUtil.CreateUnityWebRequest(thumbnailUrl.Url, UnityWebRequest.kHttpVerbGET);
            downloadThumbnailRequest.downloadHandler = new DownloadHandlerBuffer();

            downloadThumbnailRequest.SendWebRequest();

            while (!downloadThumbnailRequest.isDone)
            {
                yield return null;
            }

            if (downloadThumbnailRequest.isNetworkError)
            {
                HandleError(new Exception(downloadThumbnailRequest.error));
                yield break;
            }

            if (downloadThumbnailRequest.isHttpError)
            {
                HandleError(new Exception(downloadThumbnailRequest.downloadHandler.text));
                yield break;
            }

            try
            {
                var texture = new Texture2D(1, 1);
                texture.LoadImage(downloadThumbnailRequest.downloadHandler.data);
                texture.filterMode = FilterMode.Point;
                onSuccess?.Invoke(texture);
                cacheMap[thumbnailUrl] = texture;
            }
            catch (Exception e)
            {
                HandleError(e);
            }
        }

        void HandleError(Exception e)
        {
            Debug.LogException(e);
            onError?.Invoke(e);
            cacheMap.Remove(thumbnailUrl);
        }
    }
}
