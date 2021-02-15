using System;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public static class ByteArrayExtension
    {
        public static string ToSafeBase64(this byte[] data)
        {
            return Convert.ToBase64String(data)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}
