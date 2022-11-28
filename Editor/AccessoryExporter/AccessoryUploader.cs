using System;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Builder;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.AccessoryExporter
{
    public class AccessoryUploader
    {
        public static async Task<string> UploadAccessory(string accessoryTemplateId, GameObject gameObject)
        {
            try
            {
                var builder = new AccessoryTemplateBuilder();
                var zipBinary = await builder.Build(gameObject);
                var uploadService = new UploadAccessoryTemplateService();
                uploadService.SetAccessToken(EditorPrefsUtils.SavedAccessToken.Token);
                accessoryTemplateId = await uploadService.UploadAsync(accessoryTemplateId, zipBinary, default);
                Debug.Log($"Upload completed accessoryTemplateId: {accessoryTemplateId}", gameObject);
                return accessoryTemplateId;
            }
            catch (Exception e)
            {
                Debug.LogException(e, gameObject);
            }

            return null;
        }
    }
}
