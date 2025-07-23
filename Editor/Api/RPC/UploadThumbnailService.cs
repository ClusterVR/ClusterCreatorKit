using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Translation;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class UploadThumbnailService
    {
        const string ContentType = "image/jpeg";

        readonly string accessToken;
        readonly string filePath;
        readonly PostUploadThumbnailPolicyPayload payload;

        public UploadThumbnailService(
            string accessToken,
            string filePath)
        {
            this.accessToken = accessToken;
            this.filePath = filePath;

            var fileInfo = new FileInfo(filePath);
            payload = new PostUploadThumbnailPolicyPayload(ContentType, fileInfo.Name, fileInfo.Length);
        }

        public async Task<ThumbnailUploadPolicy> RunAsync(CancellationToken cancellationToken)
        {
            return await UploadAsync(cancellationToken);
        }

        async Task<ThumbnailUploadPolicy> UploadAsync(CancellationToken cancellationToken)
        {
            var policy = await APIServiceClient.PostUploadThumbnailPolicy(payload, accessToken,
                JsonConvert.DeserializeObject<ThumbnailUploadPolicy>, cancellationToken);
            await UploadAsync(policy, cancellationToken);
            return policy;
        }

        async Task UploadAsync(ThumbnailUploadPolicy policy, CancellationToken cancellationToken)
        {
            var fileBytes = ReadFile(filePath);

            if (policy == null || fileBytes == null)
            {
                throw new Exception("unknown error");
            }

            var form = BuildFormSections(fileBytes, policy);
            var uploadFileWebRequest = UnityWebRequest.Post(policy.uploadUrl, form);

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
                Debug.Log(TranslationTable.cck_success_upload_thumbnail);
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

        static List<IMultipartFormSection> BuildFormSections(byte[] file, ThumbnailUploadPolicy policy)
        {
            var form = new List<IMultipartFormSection>();

            foreach (var (key, value) in policy.form)
            {
                form.Add(new MultipartFormDataSection(key, value.ToString()));
            }

            form.Add(new MultipartFormFileSection("file", file, policy.fileName, policy.contentType));
            return form;
        }
    }
}
