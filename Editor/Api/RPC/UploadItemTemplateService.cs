using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.ItemTemplate;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class UploadItemTemplateService
    {
        const string FileName = "item.zip";
        const string ContentType = "application/zip";

        readonly string accessToken;

        public UploadItemTemplateService(
            string accessToken)
        {
            this.accessToken = accessToken;
        }

        public async Task<string> UploadAsync(byte[] binary, CancellationToken cancellationToken)
        {
            var payload = new UploadItemTemplatePoliciesPayload(ContentType, FileName, binary.Length);
            var policy = await APIServiceClient.PostItemTemplatePolicies(payload, accessToken,
                JsonConvert.DeserializeObject<UploadItemTemplatePoliciesResponse>,
                cancellationToken);

            var form = BuildFormSections(binary, FileName, ContentType, policy);
            using (var uploadFileWebRequest = UnityWebRequest.Post(policy.uploadUrl, form))
            {
                uploadFileWebRequest.SendWebRequest();
                while (!uploadFileWebRequest.isDone)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(50), cancellationToken);
                }

                if (uploadFileWebRequest.isNetworkError)
                {
                    throw new Exception(uploadFileWebRequest.error);
                }
                if (uploadFileWebRequest.isHttpError)
                {
                    throw new Exception(uploadFileWebRequest.downloadHandler.text);
                }
            }

            await CheckUploadStatusAsync(policy.statusApiUrl, cancellationToken);

            return policy.itemTemplateID;
        }

        async Task CheckUploadStatusAsync(string statusApiUrl, CancellationToken cancellationToken)
        {
            while (true)
            {
                var result = await ApiClient.GetStatus(accessToken, statusApiUrl, cancellationToken);
                var serializer = new VJson.JsonSerializer(typeof(UploadStatus));
                var uploadStatus = (UploadStatus) serializer.Deserialize(result);
                switch (uploadStatus.Status)
                {
                    case UploadStatus.StatusEnum.Validating:
                        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                        continue;
                    case UploadStatus.StatusEnum.Completed:
                        return;
                    case UploadStatus.StatusEnum.Error:
                        throw new Exception(uploadStatus.Reason);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        static List<IMultipartFormSection> BuildFormSections(byte[] file, string fileName, string contentType, UploadItemTemplatePoliciesResponse policy)
        {
            var form = new List<IMultipartFormSection>();

            foreach (var (key, value) in policy.form)
            {
                form.Add(new MultipartFormDataSection(key, value.ToString()));
            }

            form.Add(new MultipartFormFileSection("file", file, fileName, contentType));
            return form;
        }
    }
}
