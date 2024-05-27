namespace ClusterVR.CreatorKit.Translation
{
    public static class TranslationUtility
    {
        public static string GetMessage(string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
