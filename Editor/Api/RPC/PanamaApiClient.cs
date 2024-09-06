using System.Text;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Proto;
using Google.Protobuf;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.RPC.Client
{
    public static class PanamaApiClient
    {
        static string BaseUrl => Api.RPC.Constants.PanamaApiBaseUrl;

        public static async void PostEventAsync(PanamaEvent panamaEvent)
        {
            var base64str = System.Convert.ToBase64String(panamaEvent.ToByteArray());
            var data = Encoding.UTF8.GetBytes(base64str);
            await PostAsync("/v1/event", data);
        }

        static async Task PostAsync(string path, byte[] data)
        {
            using var webRequest = new UnityWebRequest(BaseUrl + path, UnityWebRequest.kHttpVerbPOST)
            {
                uploadHandler = new UploadHandlerRaw(data),
                downloadHandler = new DownloadHandlerBuffer()
            };
            webRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            webRequest.SendWebRequest();

            while (!webRequest.isDone)
            {
                await Task.Delay(50);
            }
        }
    }
}
