using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.AccessoryTemplate;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class UploadAccessoryTemplateService : IItemUploadService
    {
        const string FileName = "accessory.zip";
        const string ContentType = "application/zip";

        string accessToken;

        public string UploadedItemsManagementUrl => Constants.WebBaseUrl + "/account/contents/accessories";

        public void SetAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public Task<string> UploadItemAsync(byte[] binary, CancellationToken cancellationToken)
        {
            return UploadAsync(string.Empty, binary, cancellationToken);
        }

        public async Task<string> UploadAsync(string accessoryTemplateId, byte[] binary, CancellationToken cancellationToken)
        {
            var payload = new UploadAccessoryTemplatePoliciesPayload(accessoryTemplateId, ContentType, FileName, binary.Length);
            var policy = await APIServiceClient.PostAccessoryTemplatePolicies(payload, accessToken,
                JsonConvert.DeserializeObject<UploadAccessoryTemplatePoliciesResponse>,
                cancellationToken);

            var form = BuildFormSections(binary, FileName, ContentType, policy);
            using (var uploadFileWebRequest = UnityWebRequest.Post(policy.uploadUrl, form))
            {
                uploadFileWebRequest.SendWebRequest();
                while (!uploadFileWebRequest.isDone)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(50), cancellationToken);
                }

                if (uploadFileWebRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    throw new Exception(uploadFileWebRequest.error);
                }
                if (uploadFileWebRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    throw new Exception(uploadFileWebRequest.downloadHandler.text);
                }
            }

            var uploadStatusChecker = new UploadStatusChecker(accessToken, policy.statusApiUrl);
            await uploadStatusChecker.CheckUploadStatusAsync(cancellationToken);

            return policy.accessoryTemplateID;
        }

        static List<IMultipartFormSection> BuildFormSections(byte[] file, string fileName, string contentType, UploadAccessoryTemplatePoliciesResponse policy)
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
