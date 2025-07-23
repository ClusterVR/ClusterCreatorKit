using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
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
            RunAsync(CancellationToken.None).Forget();
        }

        async Task RunAsync(CancellationToken cancellationToken)
        {
            try
            {
                await GetVenuesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
                cacheMap.Remove(thumbnailUrl);
                throw;
            }
        }

        async Task GetVenuesAsync(CancellationToken cancellationToken)
        {
            Texture2D cache;
            if (cacheMap.TryGetValue(thumbnailUrl, out cache) && cache)
            {
                onSuccess?.Invoke(cache);
                return;
            }

            var downloadThumbnailRequest =
                ClusterApiUtil.CreateUnityWebRequest(thumbnailUrl.Url, UnityWebRequest.kHttpVerbGET);
            downloadThumbnailRequest.downloadHandler = new DownloadHandlerBuffer();

            downloadThumbnailRequest.SendWebRequest();

            while (!downloadThumbnailRequest.isDone)
            {
                await Task.Delay(50, cancellationToken);
            }

            if (downloadThumbnailRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                throw new Exception(downloadThumbnailRequest.error);
            }

            if (downloadThumbnailRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                throw new Exception(downloadThumbnailRequest.downloadHandler.text);
            }

            var texture = new Texture2D(1, 1);
            texture.LoadImage(downloadThumbnailRequest.downloadHandler.data);
            texture.filterMode = FilterMode.Point;
            onSuccess?.Invoke(texture);
            cacheMap[thumbnailUrl] = texture;
        }
    }
}
