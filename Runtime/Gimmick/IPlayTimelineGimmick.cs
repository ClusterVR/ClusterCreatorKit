using System;
using UnityEngine.Playables;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IPlayTimelineGimmick
    {
        PlayableDirector PlayableDirector { get; }
        DateTime LastTriggeredAt { get; }
        event Action OnPlay;
    }
}
