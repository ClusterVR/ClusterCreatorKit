using System;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class PostUploadThumbnailPolicyPayload
    {
        public string contentType;
        public string fileName;
        public long fileSize;
    }
}
