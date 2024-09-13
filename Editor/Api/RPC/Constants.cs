namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public static class Constants
    {
        const string host = "cluster.mu";
        static string overridingHost = "";
        static string Host => IsOverridingHost ? overridingHost : host;
        public static bool IsOverridingHost => !string.IsNullOrEmpty(overridingHost);

        public static string ApiBaseUrl => $"https://api.{Host}";

        public static string WebBaseUrl => $"https://{Host}";

        public static void OverrideHost(string host)
        {
            overridingHost = host;
        }

        public const string PanamaApiBaseUrl = "https://panama.p.cl4r.net";
    }
}
