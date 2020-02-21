using System;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class PostUploadAssetPolicyPayload
    {
        public string fileType;
        public string fileName;
        public long fileSize;
    }
}
