using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IStopTimelineGimmick
    {
        GameObject gameObject { get; }
        DateTime LastTriggeredAt { get; }
        event Action OnStopped;
    }
}