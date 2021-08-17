using System;
using ClusterVR.CreatorKit.Gimmick;
using UnityEngine;
using UnityEngine.Playables;

namespace ClusterVR.CreatorKit.Gimmick.Supplements
{
    public sealed class PlayableSwitch : MonoBehaviour
    {
        PlayableDirector[] playableDirectors;
        DateTime lastTriggeredAt;

        void Start()
        {
            playableDirectors = GetComponentsInChildren<PlayableDirector>(true);
            foreach (var playableDirector in playableDirectors)
            {
                playableDirector.gameObject.SetActive(playableDirector.state == PlayState.Playing);
            }

            foreach (var playGimmick in GetComponentsInChildren<IPlayTimelineGimmick>(true))
            {
                playGimmick.OnPlay += () => OnPlay(playGimmick.LastTriggeredAt, playGimmick.PlayableDirector);
            }

            foreach (var stopGimmick in GetComponentsInChildren<IStopTimelineGimmick>(true))
            {
                stopGimmick.OnStopped += () => OnStopped(stopGimmick.gameObject);
            }
        }

        void OnPlay(DateTime triggeredAt, PlayableDirector played)
        {
            if (triggeredAt < lastTriggeredAt)
            {
                return;
            }
            foreach (var playableDirector in playableDirectors)
            {
                if (playableDirector == null)
                {
                    continue;
                }
                if (playableDirector == played)
                {
                    playableDirector.gameObject.SetActive(true);
                }
                else
                {
                    playableDirector.time = playableDirector.initialTime;
                    playableDirector.Evaluate();
                    playableDirector.Stop();
                    playableDirector.gameObject.SetActive(false);
                }
            }

            lastTriggeredAt = triggeredAt;
        }

        void OnStopped(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
