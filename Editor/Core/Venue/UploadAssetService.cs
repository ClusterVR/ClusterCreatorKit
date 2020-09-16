using System;
using System.Collections;
using System.IO;
using System.Text;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public class UploadAssetService
    {
        readonly string accessToken;
        readonly string filePath;
        readonly UploadRequestID uploadRequestId;
        readonly PostUploadAssetPolicyPayload payload;
        readonly Action<AssetUploadPolicy> onSuccess;
        readonly Action<Exception> onError;

        bool isProcessing;
        public bool IsProcessing => isProcessing;

        public UploadAssetService(
            string accessToken,
            string fileType,
            string filePath,
            UploadRequestID uploadRequestId,
            Action<AssetUploadPolicy> onSuccess = null,
            Action<Exception> onError = null
        )
        {
            this.accessToken = accessToken;
            this.filePath = filePath;
            this.uploadRequestId = uploadRequestId;
            this.onSuccess = onSuccess;
            this.onError = onError;

            var fileInfo = new FileInfo(filePath);
            payload = new PostUploadAssetPolicyPayload
            {
                fileType = fileType,
                fileName = fileInfo.Name,
                fileSize = fileInfo.Length
            };
        }

        public void Run()
        {
            EditorCoroutine.Start(Upload());
        }

        IEnumerator Upload()
        {
            isProcessing = true;

            var getPolicyUrl = $"{Constants.VenueApiBaseUrl}/v1/upload/venue/{uploadRequestId.Value}/policies";
            var getPolicyWebRequest =
                ClusterApiUtil.CreateUnityWebRequest(accessToken, getPolicyUrl, UnityWebRequest.kHttpVerbPOST);
            getPolicyWebRequest.downloadHandler = new DownloadHandlerBuffer();
            getPolicyWebRequest.uploadHandler =
                new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(payload)));

            getPolicyWebRequest.SendWebRequest();
            while (!getPolicyWebRequest.isDone)
            {
                yield return null;
            }

            if (getPolicyWebRequest.isNetworkError)
            {
                HandleError(new Exception(getPolicyWebRequest.error));
            }
            else if (getPolicyWebRequest.isHttpError)
            {
                HandleError(new Exception(getPolicyWebRequest.downloadHandler.text));
            }
            else
            {
                var json = getPolicyWebRequest.downloadHandler.text;

                AssetUploadPolicy policy = null;
                try
                {
                    policy = JsonConvert.DeserializeObject<AssetUploadPolicy>(json);
                }
                catch (Exception e)
                {
                    HandleError(e);
                    yield break;
                }

                byte[] fileBytes = null;
                try
                {
                    fileBytes = ReadFile(filePath);
                }
                catch (Exception e)
                {
                    HandleError(e);
                    yield break;
                }

                if (policy == null || fileBytes == null)
                {
                    HandleError(new Exception("unknown error"));
                    yield break;
                }

                var form = BuildForm(fileBytes, policy);
                var uploadFileWebRequest = UnityWebRequest.Post(policy.uploadUrl, form);

                uploadFileWebRequest.SendWebRequest();
                while (!uploadFileWebRequest.isDone)
                {
                    yield return null;
                }

                if (uploadFileWebRequest.isNetworkError)
                {
                    HandleError(new Exception(uploadFileWebRequest.error));
                }
                else if (uploadFileWebRequest.isHttpError)
                {
                    HandleError(new Exception(uploadFileWebRequest.downloadHandler.text));
                }
                else
                {
                    Debug.Log($"Success Upload {policy.fileType}");
                    onSuccess?.Invoke(policy);
                    isProcessing = false;
                }
            }
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

        void HandleError(Exception e)
        {
            Debug.LogException(e);
            onError?.Invoke(e);
            isProcessing = false;
        }
    }
}
