using System;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.Playables;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [DisallowMultipleComponent, RequireComponent(typeof(PlayableDirector))]
    public sealed class PlayTimelineGimmick : MonoBehaviour, IPlayTimelineGimmick, IGlobalGimmick, IRerunOnPauseResumedGimmick
    {
        [SerializeField, HideInInspector] PlayableDirector playableDirector;
        [SerializeField] GlobalGimmickKey globalGimmickKey;

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
            if (playableDirector == null)
            {
                playableDirector = GetComponent<PlayableDirector>();
            }
            stopTimelineGimmick = GetComponent<IStopTimelineGimmick>();
        }

        public void Run(GimmickValue value, DateTime current)
        {
            Run(value, current, false);
        }

        void IRerunnableGimmick.Rerun(GimmickValue value, DateTime current)
        {
            Run(value, current, true);
        }

        void Run(GimmickValue value, DateTime current, bool useSameValue)
        {
            if (playableDirector == null)
            {
                return;
            }

            if (useSameValue)
            {
                if (value.TimeStamp < LastTriggeredAt)
                {
                    return;
                }
            }
            else
            {
                if (value.TimeStamp <= LastTriggeredAt)
                {
                    return;
                }
            }

            if (stopTimelineGimmick != null && value.TimeStamp < stopTimelineGimmick.LastTriggeredAt)
            {
                return;
            }
            LastTriggeredAt = value.TimeStamp;

            OnPlay?.Invoke();
            var time = playableDirector.initialTime + (current - value.TimeStamp).TotalSeconds;

            var duration = playableDirector.duration;
            const double minTime = long.MinValue * 1e-12;
            if (time < minTime)
            {
                if (duration == 0)
                {
                    time = minTime + 1d;
                }
                else
                {
                    time += duration * (1 + Math.Floor((minTime - time) / duration));
                }
            }
            else if (duration < time)
            {
                if (duration == 0)
                {
                    time = 1d;
                }
                else
                {
                    time = time % duration + duration;
                }
            }

            playableDirector.time = time;
            playableDirector.Play();
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
