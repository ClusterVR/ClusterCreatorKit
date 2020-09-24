using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Trigger.Implements
{
    public class OnJoinPlayerTrigger : MonoBehaviour, IOnJoinPlayerTrigger
    {
        [SerializeField, PlayerTriggerParam] TriggerParam[] triggers;
        public event PlayerTriggerEventHandler TriggerEvent;

        public void Invoke()
        {
            TriggerEvent?.Invoke(this, new TriggerEventArgs(triggers.Select(t => t.Convert()).ToArray()));
        }
    }
}