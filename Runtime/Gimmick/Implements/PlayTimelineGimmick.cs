using System;
using ClusterVR.CreatorKit.Common;
using ClusterVR.CreatorKit.Item;
using UnityEngine;
using UnityEngine.Playables;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [DisallowMultipleComponent, RequireComponent(typeof(PlayableDirector))]
    public sealed class PlayTimelineGimmick : MonoBehaviour, IPlayTimelineGimmick, IGlobalGimmick, IRerunOnPauseResumedGimmick, ITimeProviderRequester
    {
        const float TimeDifferenceTolerantSeconds = 1f;

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
        ITimeProvider timeProvider;

        bool isInitialized;
        bool hasPlayed;
        double startTime;
        DateTime startDate;

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
            stopTimelineGimmick = GetComponent<IStopTimelineGimmick>();
            isInitialized = true;
        }

        void ITimeProviderRequester.SetTimeProvider(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
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
            EnforceInitialized();

            if (playableDirector == null || timeProvider == null)
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
            startTime = time;
            startDate = timeProvider.GetTime();
            hasPlayed = true;
        }

        void Update()
        {
            CorrectWhenTimeDiffers();
        }

        void CorrectWhenTimeDiffers()
        {
            if (!hasPlayed || timeProvider == null)
            {
                return;
            }

            if (playableDirector.state != PlayState.Playing)
            {
                return;
            }

            if (playableDirector.time == playableDirector.duration)
            {
                return;
            }

            if (playableDirector.duration < TimeDifferenceTolerantSeconds)
            {
                return;
            }

            var expectedTime = startTime + (timeProvider.GetTime() - startDate).TotalSeconds;
            var currentTime = playableDirector.time;
            if (playableDirector.extrapolationMode == DirectorWrapMode.Loop && expectedTime > 0)
            {
                expectedTime %= playableDirector.duration;
                var diff = Math.Abs(expectedTime - currentTime);
                if (diff > TimeDifferenceTolerantSeconds && diff < playableDirector.duration - TimeDifferenceTolerantSeconds)
                {
                    CorrectTime(expectedTime);
                }
            }
            else
            {
                var diff = Math.Abs(expectedTime - currentTime);
                if (diff > TimeDifferenceTolerantSeconds)
                {
                    CorrectTime(expectedTime);
                }
            }
        }

        void CorrectTime(double expectedTime)
        {
            playableDirector.time = expectedTime;
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
