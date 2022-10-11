using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class UploadAssetService
    {
        readonly string accessToken;
        readonly string filePath;
        readonly UploadRequestID uploadRequestId;
        readonly PostUploadAssetPolicyPayload payload;
        readonly Action<AssetUploadPolicy> onSuccess;
        readonly Action<Exception> onError;

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
            payload = new PostUploadAssetPolicyPayload(fileType, fileInfo.Name, fileInfo.Length);
        }

        public void Run(CancellationToken cancellationToken)
        {
            _ = UploadAsync(cancellationToken);
        }

        async Task UploadAsync(CancellationToken cancellationToken)
        {
            try
            {
                var policy = await APIServiceClient.PostUploadAssetPolicy(uploadRequestId, payload, accessToken,
                    JsonConvert.DeserializeObject<AssetUploadPolicy>, cancellationToken);
                EditorCoroutine.Start(Upload(policy));
            }
            catch (Exception e)
            {
                HandleError(e);
            }
        }

        IEnumerator Upload(AssetUploadPolicy policy)
        {
            byte[] fileBytes;
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

            var form = BuildFormSections(fileBytes, policy);
            using var uploadFileWebRequest = UnityWebRequest.Post(policy.uploadUrl, form);

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

        static List<IMultipartFormSection> BuildFormSections(byte[] file, AssetUploadPolicy policy)
        {
            var form = new List<IMultipartFormSection>();

            foreach (var (key, value) in policy.form)
            {
                form.Add(new MultipartFormDataSection(key, value.ToString()));
            }

            form.Add(new MultipartFormFileSection("file", file, policy.fileName, "application/octet-stream"));
            return form;
        }

        void HandleError(Exception e)
        {
            Debug.LogException(e);
            onError?.Invoke(e);
        }
    }
}
