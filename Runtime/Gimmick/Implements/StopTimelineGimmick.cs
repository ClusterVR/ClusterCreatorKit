using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.Playables;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [DisallowMultipleComponent, RequireComponent(typeof(PlayableDirector))]
    public sealed class StopTimelineGimmick : MonoBehaviour, IStopTimelineGimmick, IGlobalGimmick
    {
        [SerializeField, HideInInspector] PlayableDirector playableDirector;
        [SerializeField] GlobalGimmickKey globalGimmickKey;

        ItemId IGimmick.ItemId => globalGimmickKey.ItemId;
        GimmickTarget IGimmick.Target => globalGimmickKey.Key.Target;
        string IGimmick.Key => globalGimmickKey.Key.Key;
        ParameterType IGimmick.ParameterType => ParameterType.Signal;

        public DateTime LastTriggeredAt { get; private set; }
        public event Action OnStopped;
        IPlayTimelineGimmick playTimelineGimmick;

        bool isInitialized;

        void Start()
        {
            EnforceInitialized();
        }

        void EnforceInitialized()
        {
            if (isInitialized)
            {
                return;
            }
            if (playableDirector == null)
            {
                playableDirector = GetComponent<PlayableDirector>();
            }
            playTimelineGimmick = GetComponent<IPlayTimelineGimmick>();
            isInitialized = true;
        }

        public void Run(GimmickValue value, DateTime current)
        {
            EnforceInitialized();

            if (playableDirector == null)
            {
                return;
            }
            if (value.TimeStamp <= LastTriggeredAt)
            {
                return;
            }
            if (playTimelineGimmick != null && value.TimeStamp <= playTimelineGimmick.LastTriggeredAt)
            {
                return;
            }
            LastTriggeredAt = value.TimeStamp;

            playableDirector.time = playableDirector.initialTime;
            playableDirector.Evaluate();
            playableDirector.Stop();

            OnStopped?.Invoke();
        }

        void OnValidate()
        {
            if (playableDirector == null || playableDirector.gameObject != gameObject)
            {
                playableDirector = GetComponent<PlayableDirector>();
            }
        }

        void Reset()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
    }
}
