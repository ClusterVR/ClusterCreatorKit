using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public static class ApiClient
    {
        public static Task<TResp> Get<TReq, TResp>(TReq request, string accessToken, string url)
        {
            return CallWithRetry<TReq, TResp>(request, accessToken, url, UnityWebRequest.kHttpVerbGET);
        }

        public static Task<TResp> Post<TReq, TResp>(TReq request, string accessToken, string url)
        {
            return CallWithRetry<TReq, TResp>(request, accessToken, url, UnityWebRequest.kHttpVerbPOST);
        }

        public static Task<TResp> Post<TReq, TResp>(TReq request, string accessToken, string url,
            Func<string, TResp> deserializer)
        {
            return CallWithRetry(request, accessToken, url, UnityWebRequest.kHttpVerbPOST, deserializer);
        }

        public static Task<TResp> Patch<TReq, TResp>(TReq request, string accessToken, string url)
        {
            return CallWithRetry<TReq, TResp>(request, accessToken, url, "PATCH");
        }

        public static async Task PostAnalyticsEvent<TReq>(TReq request, string accessToken, string url)
        {
            using (var webRequest =
                ClusterApiUtil.CreateUnityWebRequestAsAnalytics(accessToken, url, UnityWebRequest.kHttpVerbPOST))
            {
                await Call(webRequest, request);
            }
        }

        static Task<TResp> CallWithRetry<TReq, TResp>(TReq request, string accessToken, string url, string httpVerb)
        {
            return CallWithRetry(request, accessToken, url, httpVerb, JsonDeserializer<TResp>);
        }

        static async Task<TResp> CallWithRetry<TReq, TResp>(TReq request, string accessToken, string url,
            string httpVerb, Func<string, TResp> deserializer, int numRetries = 3)
        {
            float backoffMs = 500;

            for (var i = 0; i < numRetries; i++)
            {
                try
                {
                    return await Call(request, accessToken, url, httpVerb, deserializer);
                }
                catch (Exception)
                {
                    await Task.Delay((int) backoffMs);
                    backoffMs *= 1.5f;
                }
            }

            return await Call(request, accessToken, url, httpVerb, deserializer);
        }

        static async Task<TResp> Call<TReq, TResp>(TReq request, string accessToken, string url, string httpVerb,
            Func<string, TResp> deserializer)
        {
            using (var webRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, url, httpVerb))
            {
                var responseText = await Call(webRequest, request);
                return deserializer(responseText);
            }
        }

        static async Task<string> Call<TReq>(UnityWebRequest webRequest, TReq request)
        {
            if (webRequest.method != UnityWebRequest.kHttpVerbGET && request != null)
            {
                webRequest.uploadHandler = new UploadHandlerRaw(Serialize(request));
            }

            webRequest.downloadHandler = new DownloadHandlerBuffer();

            webRequest.SendWebRequest();

            while (!webRequest.isDone)
            {
                await Task.Delay(50);
            }

            if (webRequest.isNetworkError)
            {
                throw new Exception(webRequest.error);
            }

            if (webRequest.isHttpError)
            {
                throw new Exception(webRequest.downloadHandler.text);
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
