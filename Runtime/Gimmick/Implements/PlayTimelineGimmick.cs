using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.Playables;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [DisallowMultipleComponent, RequireComponent(typeof(PlayableDirector))]
    public class PlayTimelineGimmick : MonoBehaviour, IPlayTimelineGimmick, IGlobalGimmick
    {
        [SerializeField, HideInInspector] PlayableDirector playableDirector;
        [SerializeField, ConsistentlySyncGlobalGimmickKey] GlobalGimmickKey globalGimmickKey;

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public PlayableDirector PlayableDirector => playableDirector;
        public DateTime LastTriggeredAt { get; private set; }
        public event Action OnPlay;
        IStopTimelineGimmick stopTimelineGimmick;

        void Start()
        {
            if (playableDirector == null) playableDirector = GetComponent<PlayableDirector>();
            stopTimelineGimmick = GetComponent<IStopTimelineGimmick>();
        }

        public void Run(GimmickValue value, DateTime current)
        {
            if (playableDirector == null) return;
            if (value.TimeStamp <= LastTriggeredAt) return;
            if (stopTimelineGimmick != null && value.TimeStamp < stopTimelineGimmick.LastTriggeredAt) return;
            LastTriggeredAt = value.TimeStamp;

            OnPlay?.Invoke();
            var time = playableDirector.initialTime + (current - value.TimeStamp).TotalSeconds;
            playableDirector.time = time;
            playableDirector.Play();
        }

        void OnValidate()
        {
            if (playableDirector == null || playableDirector.gameObject != gameObject) playableDirector = GetComponent<PlayableDirector>();
        }

        void Reset()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
    }
}
