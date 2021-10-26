namespace ClusterVR.CreatorKit.Editor.Preview.RoomState
{
    public static class RoomStateKey
    {
        public static string GetGlobalKey(string key) => GetGlobalKeyPrefix() + key;
        public static string GetPlayerKey(string key) => GetPlayerKeyPrefix() + key;
        public static string GetItemKey(ulong itemId, string key) => GetItemKeyPrefix(itemId) + key;
        public static string GetGlobalKeyPrefix() => "_g.";
        public static string GetPlayerKeyPrefix() => "_p.";
        public static string GetItemKeyPrefix(ulong itemId) => $"_i.{itemId}.";
    }
}
