namespace ClusterVR.CreatorKit.Editor.Preview.RoomState
{
    public static class RoomStateKey
    {
        public static string GetGlobalKey(string key)
        {
            return $"_g.{key}";
        }

        public static string GetPlayerKey(string key)
        {
            return $"_p.{key}";
        }

        public static string GetItemKey(ulong itemId, string key)
        {
            return $"_i.{itemId}.{key}";
        }
    }
}
