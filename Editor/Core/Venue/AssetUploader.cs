using System;
using System.IO;
using System.Text;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public static class AssetUploader
    {
        public static string Upload(string accessToken, VenueID venueId)
        {
            var request = APIServiceClient.PostUploadRequest.CallSync(venueId, accessToken);
            AssetUpload(accessToken, "assetbundle/win", EditorPrefsUtils.LastBuildWin, request.UploadRequestId);
            AssetUpload(accessToken, "assetbundle/mac", EditorPrefsUtils.LastBuildMac, request.UploadRequestId);
            AssetUpload(accessToken, "assetbundle/android", EditorPrefsUtils.LastBuildAndroid, request.UploadRequestId);
            AssetUpload(accessToken, "assetbundle/ios", EditorPrefsUtils.LastBuildIOS, request.UploadRequestId);

            var response = APIServiceClient.PostNotifyFinishedUpload.CallSync((venueId, request.UploadRequestId), accessToken);
            return response.Url;
        }

        static void AssetUpload(
            string accessToken,
            string fileType,
            string filePath,
            UploadRequestID uploadRequestId)
        {
            var fileInfo = new FileInfo(filePath);
            var payload = new PostUploadAssetPolicyPayload
            {
                fileType = fileType,
                fileName = fileInfo.Name,
                fileSize = fileInfo.Length
            };

            var url = $"{Constants.VenueApiBaseUrl}/v1/upload/venue/{uploadRequestId.Value}/policies";
            Debug.LogFormat("Calling RPC: {0}", url);

            var webRequest = ClusterApiUtil.CreateUnityWebRequest(accessToken, url, UnityWebRequest.kHttpVerbPOST);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(payload)));
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
            var policy = JsonConvert.DeserializeObject<AssetUploadPolicy>(responseText);

            var fileBytes = ReadFile(filePath);

            if (policy == null || fileBytes == null)
            {
                throw new Exception("unknown error");
            }

            var form = BuildForm(fileBytes, policy);
            var uploadFileWebRequest = UnityWebRequest.Post(policy.uploadUrl, form);

            uploadFileWebRequest.SendWebRequest();
            while (!uploadFileWebRequest.isDone)
            {
                Thread.Sleep(50);
            }

            if (uploadFileWebRequest.isNetworkError)
            {
                throw new Exception(uploadFileWebRequest.error);
            }

            if (uploadFileWebRequest.isHttpError)
            {
                throw new Exception(uploadFileWebRequest.downloadHandler.text);
            }

            Debug.Log($"Success Upload {policy.fileType}");
        }

        static byte[] ReadFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[fs.Length];
                using (var ms = new MemoryStream())
                {
                    int read;
                    while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                return buffer;
            }
        }

        static WWWForm BuildForm(byte[] file, AssetUploadPolicy policy)
        {
            var form = new WWWForm();

            foreach (var field in policy.form)
            {
                form.AddField(field.Key, field.Value.ToString());
            }

            form.AddBinaryData("file", file, policy.fileName, "application/octet-stream");
            return form;
        }
    }
}
