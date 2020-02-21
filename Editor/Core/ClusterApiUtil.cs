using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core
{
    public static class ClusterApiUtil
    {
        const string ClusterCreatorKit = "ClusterCreatorKit";
        const string JsonMimeType = "application/json";

        const string ContentTypeHeaderKey = "Content-Type";
        const string AccessTokenHeaderKey = "X-Cluster-Access-Token";
        const string AppVersionHeaderKey = "X-Cluster-App-Version";
        const string DeviceNameHeaderKey = "X-Cluster-Device";
        const string PlatformHeaderKey = "X-Cluster-Platform";
        const string PlatformVersionHeaderKey = "X-Cluster-Platform-Version";

        public static UnityWebRequest CreateUnityWebRequest(string accessToken, string url, string method)
        {
            var www = new UnityWebRequest(url, method);

            www.SetRequestHeader(ContentTypeHeaderKey, JsonMimeType);
            www.SetRequestHeader(AccessTokenHeaderKey, accessToken);
            www.SetRequestHeader(AppVersionHeaderKey, Constants.GetCreatorKitVersion());
            www.SetRequestHeader(DeviceNameHeaderKey, ClusterCreatorKit);
            www.SetRequestHeader(PlatformHeaderKey, ClusterCreatorKit);
            www.SetRequestHeader(PlatformVersionHeaderKey, Application.unityVersion);

            return www;
        }

        public static UnityWebRequest CreateUnityWebRequest(string url, string method)
        {
            var www = new UnityWebRequest(url, method);

            www.SetRequestHeader(ContentTypeHeaderKey, JsonMimeType);
            www.SetRequestHeader(AppVersionHeaderKey, Constants.GetCreatorKitVersion());
            www.SetRequestHeader(DeviceNameHeaderKey, ClusterCreatorKit);
            www.SetRequestHeader(PlatformHeaderKey, ClusterCreatorKit);
            www.SetRequestHeader(PlatformVersionHeaderKey, Application.unityVersion);

            return www;
        }
    }
}
