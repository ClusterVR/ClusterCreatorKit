
namespace ClusterVR.CreatorKit.Editor.Preview.RoomState
{
    public static class RoomStateKey
    {
        public static string GetGlobalKey(string key) => $"_g.{key}";
        public static string GetPlayerKey(string key) => $"_p.{key}";
        public static string GetItemKey(ulong itemId, string key) => $"_i.{itemId}.{key}";
    }
}