using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.RoomState
{
    [Serializable]
    public sealed class PersistedRoomStateData
    {
        [SerializeField] RoomStateSegment player;
        public RoomStateSegment Player => player;

        public PersistedRoomStateData(RoomStateSegment player)
        {
            this.player = player;
        }
    }

    [Serializable]
    public sealed class RoomStateSegment
    {
        [SerializeField] State[] state;
        public State[] State => state;

        public RoomStateSegment(State[] state)
        {
            this.state = state;
        }
    }

    [Serializable]
    public sealed class State
    {
        [SerializeField] string key;
        [SerializeField] StateValue value;

        public string Key => key;
        public StateValue Value => value;

        public State(string key, StateValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
