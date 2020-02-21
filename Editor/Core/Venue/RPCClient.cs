using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public class RPCClient<TReq, TResp>
    {
        readonly Func<TReq, string> apiUrlGenerator;
        readonly string httpVerb;

        public RPCClient(Func<TReq, string> apiUrlGenerator, string httpVerb)
        {
            this.apiUrlGenerator = apiUrlGenerator;
            this.httpVerb = httpVerb;
        }

        // Call with infinite retry with exponential backoff.
        public async Task<TResp> Call(TReq request, string accessToken, int numRetries = 3)
        {
            // TODO: Make retry strategy configurable.
            float backoffMs = 500;

            for (int i = 0; i < numRetries; i++)
            {
                try
                {
                    return await CallNoRetry(request, accessToken);
                }
                catch (Exception)
                {
                    await Task.Delay((int) backoffMs);
                    backoffMs *= 1.5f;
                }
            }

            return await CallNoRetry(request, accessToken);
        }

        async Task<TResp> CallNoRetry(TReq request, string accessToken)
        {
            var url = apiUrlGenerator(request);
            Debug.LogFormat("Calling RPC: {0}", url);

            var webRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, url, httpVerb);
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

            var responseText = webRequest.downloadHandler.text;
            Debug.LogFormat("Calling RPC ResponseText: {0}", responseText);
            return JsonUtility.FromJson<TResp>(responseText);
        }

        public TResp CallSync(TReq request, string accessToken, int numRetries = 3)
        {
            // TODO: Make retry strategy configurable.
            float backoffMs = 500;

            for (int i = 0; i < numRetries; i++)
            {
                try
                {
                    return CallNoRetrySync(request, accessToken);
                }
                catch (Exception)
                {
                    Thread.Sleep((int) backoffMs);
                    backoffMs *= 1.5f;
                }
            }

            return CallNoRetrySync(request, accessToken);
        }

        TResp CallNoRetrySync(TReq request, string accessToken)
        {
            var url = apiUrlGenerator(request);
            Debug.LogFormat("Calling RPC: {0}", url);

            var webRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, url, httpVerb);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SendWebRequest();

            while (!webRequest.isDone)
            {
                Thread.Sleep(50);
            }

            if (webRequest.isNetworkError)
            {
                throw new Exception(webRequest.error);
            }

            var responseText = webRequest.downloadHandler.text;
            Debug.LogFormat("Calling RPC ResponseText: {0}", responseText);
            return JsonUtility.FromJson<TResp>(responseText);
        }
    }
}
