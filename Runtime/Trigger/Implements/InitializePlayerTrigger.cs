using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    public sealed class InitializePlayerTrigger : MonoBehaviour, IInitializePlayerTrigger
    {
        [SerializeField, InitializePlayerTriggerParam] TriggerParam[] triggers;
        public event PlayerTriggerEventHandler TriggerEvent;
        IEnumerable<Trigger.TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());

        public void Invoke()
        {
            TriggerEvent?.Invoke(this,
                new TriggerEventArgs(triggers.Select(t => t.Convert()).ToArray(), dontOverride: true));
        }
    }
}
