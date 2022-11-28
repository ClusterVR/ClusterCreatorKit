using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public static class ApiClient
    {
        public static Task<TResp> Get<TReq, TResp>(TReq request, string accessToken, string url,
            CancellationToken cancellationToken)
        {
            return CallWithRetry<TReq, TResp>(request, accessToken, url, UnityWebRequest.kHttpVerbGET,
                cancellationToken);
        }

        public static Task<TResp> Post<TReq, TResp>(TReq request, string accessToken, string url,
            CancellationToken cancellationToken)
        {
            return CallWithRetry<TReq, TResp>(request, accessToken, url, UnityWebRequest.kHttpVerbPOST,
                cancellationToken);
        }

        public static Task<TResp> Post<TReq, TResp>(TReq request, string accessToken, string url,
            Func<string, TResp> deserializer, CancellationToken cancellationToken)
        {
            return CallWithRetry(request, accessToken, url, UnityWebRequest.kHttpVerbPOST, deserializer,
                cancellationToken);
        }

        public static Task<TResp> Patch<TReq, TResp>(TReq request, string accessToken, string url,
            CancellationToken cancellationToken)
        {
            return CallWithRetry<TReq, TResp>(request, accessToken, url, "PATCH", cancellationToken);
        }

        public static async Task PostAnalyticsEvent<TReq>(TReq request, string accessToken, string url,
            CancellationToken cancellationToken)
        {
            using (var webRequest = ClusterApiUtil.CreateUnityWebRequestAsAnalytics(accessToken, url, UnityWebRequest.kHttpVerbPOST))
            {
                await Call(webRequest, request, cancellationToken);
            }
        }

        public static async Task<string> GetStatus(string accessToken, string url, CancellationToken cancellationToken)
        {
            using (var webRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, url, UnityWebRequest.kHttpVerbGET))
            {
                return await Call(webRequest, cancellationToken);
            }
        }

        static Task<TResp> CallWithRetry<TReq, TResp>(TReq request, string accessToken, string url, string httpVerb,
            CancellationToken cancellationToken)
        {
            return CallWithRetry(request, accessToken, url, httpVerb, JsonDeserializer<TResp>, cancellationToken);
        }

        static async Task<TResp> CallWithRetry<TReq, TResp>(TReq request, string accessToken, string url,
            string httpVerb, Func<string, TResp> deserializer, CancellationToken cancellationToken, int numRetries = 3)
        {
            float backoffMs = 500;

            for (var i = 0; i < numRetries; i++)
            {
                try
                {
                    return await Call(request, accessToken, url, httpVerb, deserializer, cancellationToken);
                }
                catch (Exception)
                {
                    await Task.Delay((int) backoffMs, cancellationToken);
                    backoffMs *= 1.5f;
                }
            }

            return await Call(request, accessToken, url, httpVerb, deserializer, cancellationToken);
        }

        static async Task<TResp> Call<TReq, TResp>(TReq request, string accessToken, string url, string httpVerb,
            Func<string, TResp> deserializer, CancellationToken cancellationToken)
        {
            using (var webRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, url, httpVerb))
            {
                var responseText = await Call(webRequest, request, cancellationToken);
                return deserializer(responseText);
            }
        }

        static Task<string> Call<TReq>(UnityWebRequest webRequest, TReq request, CancellationToken cancellationToken)
        {
            if (webRequest.method != UnityWebRequest.kHttpVerbGET && request != null)
            {
                webRequest.uploadHandler = new UploadHandlerRaw(Serialize(request));
            }
            return Call(webRequest, cancellationToken);
        }

        static async Task<string> Call(UnityWebRequest webRequest, CancellationToken cancellationToken)
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            webRequest.SendWebRequest();

            while (!webRequest.isDone)
            {
                await Task.Delay(50, cancellationToken);
            }

            if (webRequest.isNetworkError)
            {
                throw new Exception(webRequest.error);
            }

            if (webRequest.GetResponseHeader("x-cluster-is-banned") != null)
            {
                throw new Exception("アカウントが停止されているため、使える機能が制限されています");
            }

            if (webRequest.isHttpError)
            {
                throw new HttpException((int)webRequest.responseCode, webRequest.downloadHandler.text);
            }

            return webRequest.downloadHandler.text;
        }

        static byte[] Serialize<TReq>(TReq req)
        {
            return Encoding.UTF8.GetBytes(JsonUtility.ToJson(req));
        }

        static TResp JsonDeserializer<TResp>(string json)
        {
            return JsonUtility.FromJson<TResp>(json);
        }
    }
}
