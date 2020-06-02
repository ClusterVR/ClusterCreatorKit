using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IWorldGate
    {
        event OnEnterWorldGateEventHandler OnEnterWorldGateEvent;
    }
    
    public delegate void OnEnterWorldGateEventHandler(OnEnterWorldGateEventArgs e);

    public class OnEnterWorldGateEventArgs : EventArgs
    {
        public string WorldOrEventId { get; }
        public GameObject EnterObject { get; }

        public OnEnterWorldGateEventArgs(string worldOrEventId, GameObject enterObject)
        {
            WorldOrEventId = worldOrEventId;
            EnterObject = enterObject;
        }
    }
}
