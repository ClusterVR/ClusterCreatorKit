using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class UploadAssetService
    {
        readonly string accessToken;

        public UploadAssetService(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public async Task<AssetUploadPolicy> UploadAsync(ExportedSceneInfo exportedSceneInfo, BuildTarget target, bool isMainScene, UploadRequestID uploadRequestId, CancellationToken cancellationToken)
        {
            var fileInfo = new FileInfo(exportedSceneInfo.BuiltAssetBundlePath);
            var payload = new PostUploadAssetPolicyPayload(exportedSceneInfo.AssetIdsDependsOn, target.GetFileType(), fileInfo.Name, fileInfo.Length, isMainScene ? "main" : "sub");
            var policy = await APIServiceClient.PostUploadAssetPolicy(uploadRequestId, payload, accessToken,
                JsonConvert.DeserializeObject<AssetUploadPolicy>, cancellationToken);

            await UploadAsync(exportedSceneInfo.BuiltAssetBundlePath, policy, cancellationToken);

            return policy;
        }

        public async Task<AssetUploadPolicy> UploadAsync(ExportedVenueAssetInfo venueAssetPath, BuildTarget target, UploadRequestID uploadRequestId, CancellationToken cancellationToken)
        {
            var fileInfo = new FileInfo(venueAssetPath.BuiltAssetBundlePath);
            var payload = new PostUploadVenueAssetPoliciesPayload(target.GetFileType(), fileInfo.Name, fileInfo.Length);
            var policy = await APIServiceClient.PostUploadVenueAssetPolicies(uploadRequestId, payload, accessToken,
                JsonConvert.DeserializeObject<AssetUploadPolicy>, cancellationToken);

            await UploadAsync(venueAssetPath.BuiltAssetBundlePath, policy, cancellationToken);

            return policy;
        }

        async Task UploadAsync(string filePath, AssetUploadPolicy policy, CancellationToken cancellationToken)
        {
            var fileBytes = ReadFile(filePath);

            if (policy == null || fileBytes == null)
            {
                throw new Exception("unknown error");
            }

            var form = BuildFormSections(fileBytes, policy);
            using var uploadFileWebRequest = UnityWebRequest.Post(policy.uploadUrl, form);

            uploadFileWebRequest.SendWebRequest();
            while (!uploadFileWebRequest.isDone)
            {
                await Task.Delay(50, cancellationToken);
            }

            if (uploadFileWebRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                throw new Exception(uploadFileWebRequest.error);
            }
            else if (uploadFileWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                throw new Exception(uploadFileWebRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log($"Success Upload {policy.fileType}");
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
    }
}
