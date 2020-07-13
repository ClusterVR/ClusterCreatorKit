using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger
{
    public class TriggerEventArgs : EventArgs
    {
        public TriggerParam[] TriggerParams { get; }
        public GameObject CollidedObject { get; }

        public TriggerEventArgs(TriggerParam[] triggerParams, GameObject collidedObject = null)
        {
            TriggerParams = triggerParams;
            CollidedObject = collidedObject;
        }
    }
}