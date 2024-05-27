using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.Validator;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.WorldGate
{
    [RequireComponent(typeof(Collider))]
    [RequireIsTriggerSettings]
    public sealed class WorldGate : MonoBehaviour, IWorldGate
    {
        [SerializeField, Tooltip(TranslationTable.cck_world_event_id)] string worldOrEventId;
        [SerializeField, Tooltip(TranslationTable.cck_warp_spawnpoint_key_optional)] string key;
        [SerializeField, Tooltip(TranslationTable.cck_confirm_ui_before_move)] bool confirmTransition;

        public event OnEnterWorldGateEventHandler OnEnterWorldGateEvent;
        public event OnExitWorldGateEventHandler OnExitWorldGateEvent;

        void OnTriggerEnter(Collider other)
        {
            OnEnterWorldGateEvent?.Invoke(new OnEnterWorldGateEventArgs(worldOrEventId, other.gameObject, key, confirmTransition));
        }

        void OnTriggerExit(Collider other)
        {
            OnExitWorldGateEvent?.Invoke(new OnExitWorldGateEventArgs(other.gameObject));
        }

        void OnValidate()
        {
            foreach (var col in GetComponentsInChildren<Collider>(true))
            {
                col.isTrigger = true;
            }
        }

        void Reset()
        {
            confirmTransition = true;
        }
    }
}
