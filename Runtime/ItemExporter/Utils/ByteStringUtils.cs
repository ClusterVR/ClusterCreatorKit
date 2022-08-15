using Google.Protobuf;

namespace ClusterVR.CreatorKit.ItemExporter.Utils
{
    public static class ByteStringUtils
    {
        public static string ToSafeBase64(this ByteString data)
        {
            return data.ToBase64().EncodeUrlSafeBase64();
        }

        static string EncodeUrlSafeBase64(this string data)
        {
            return data
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}
