using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Editor.Preview.RoomState
{
    public sealed class RoomStateRepository
    {
        readonly Dictionary<string, StateValue> values = new Dictionary<string, StateValue>();

        public void Update(string key, StateValue value)
        {
            values[key] = value;
        }

        public bool TryGetValue(string key, out StateValue value)
        {
            return values.TryGetValue(key, out value);
        }
    }
}
