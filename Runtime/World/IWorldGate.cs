using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IWorldGate
    {
        event OnEnterWorldGateEventHandler OnEnterWorldGateEvent;
        event OnExitWorldGateEventHandler OnExitWorldGateEvent;
    }

    public delegate void OnEnterWorldGateEventHandler(OnEnterWorldGateEventArgs e);

    public delegate void OnExitWorldGateEventHandler(OnExitWorldGateEventArgs e);

    public sealed class OnEnterWorldGateEventArgs : EventArgs
    {
        public string WorldOrEventId { get; }
        public GameObject EnterObject { get; }
        public string Key { get; }
        public bool ConfirmTransition { get; }

        public OnEnterWorldGateEventArgs(string worldOrEventId, GameObject enterObject, string key, bool confirmTransition)
        {
            WorldOrEventId = worldOrEventId;
            EnterObject = enterObject;
            Key = key;
            ConfirmTransition = confirmTransition;
        }
    }

    public sealed class OnExitWorldGateEventArgs : EventArgs
    {
        public GameObject ExitObject { get; }

        public OnExitWorldGateEventArgs(GameObject exitObject)
        {
            ExitObject = exitObject;
        }
    }
}
