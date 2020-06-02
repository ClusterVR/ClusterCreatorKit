using System.Collections.Generic;
using ClusterVR.CreatorKit.Gimmick;

namespace ClusterVR.CreatorKit.Editor.Preview.RoomState
{
    public sealed class RoomStateRepository
    {
        readonly Dictionary<string, GimmickValue> values = new Dictionary<string, GimmickValue>();
        
        public void Update(string key, GimmickValue value)
        {
            values[key] = value;
        }

        public bool TryGetValue(string key, out GimmickValue value)
            => values.TryGetValue(key, out value);
    }
}