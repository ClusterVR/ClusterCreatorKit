using System.Text.RegularExpressions;

namespace ClusterVR.CreatorKit.Constants
{
    public static class Component
    {
        public const int MaxIdLength = 64;
        public static readonly Regex ValidIdCharactersRegex = new(@"^[',\-.0-9A-Z_a-z]*$");
    }
}
