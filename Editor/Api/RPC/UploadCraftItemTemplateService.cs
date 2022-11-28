using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.ItemTemplate;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class UploadCraftItemTemplateService : IItemUploadService
    {
        const string FileName = "item.zip";
        const string ContentType = "application/zip";

        string accessToken;

        public string UploadedItemsManagementUrl => Constants.WebBaseUrl + "/account/contents/items";

        public void SetAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public async Task<string> UploadItemAsync(byte[] binary, CancellationToken cancellationToken)
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

            return policy.itemTemplateID;
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
