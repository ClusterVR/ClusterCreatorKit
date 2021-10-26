using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IWorldGate
    {
        event OnEnterWorldGateEventHandler OnEnterWorldGateEvent;
    }

    public delegate void OnEnterWorldGateEventHandler(OnEnterWorldGateEventArgs e);

    public sealed class OnEnterWorldGateEventArgs : EventArgs
    {
        public string WorldOrEventId { get; }
        public GameObject EnterObject { get; }
        public string Key { get; }

        public OnEnterWorldGateEventArgs(string worldOrEventId, GameObject enterObject, string key)
        {
            WorldOrEventId = worldOrEventId;
            EnterObject = enterObject;
            Key = key;
        }
    }
}
