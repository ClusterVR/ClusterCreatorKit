using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    public sealed class OnJoinPlayerTrigger : MonoBehaviour, IOnJoinPlayerTrigger
    {
        [SerializeField, PlayerTriggerParam] TriggerParam[] triggers;
        public event PlayerTriggerEventHandler TriggerEvent;
        IEnumerable<Trigger.TriggerParam> ITrigger.TriggerParams => triggers.Select(t => t.Convert());

        public void Invoke()
        {
            TriggerEvent?.Invoke(this, new TriggerEventArgs(triggers.Select(t => t.Convert()).ToArray()));
        }
    }
}
