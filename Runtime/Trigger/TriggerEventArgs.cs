using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger
{
    public sealed class TriggerEventArgs : EventArgs
    {
        public TriggerParam[] TriggerParams { get; }
        public GameObject CollidedObject { get; }
        public bool DontOverride { get; }

        public TriggerEventArgs(TriggerParam[] triggerParams, GameObject collidedObject = null,
            bool dontOverride = false)
        {
            TriggerParams = triggerParams;
            CollidedObject = collidedObject;
            DontOverride = dontOverride;
        }
    }
}
