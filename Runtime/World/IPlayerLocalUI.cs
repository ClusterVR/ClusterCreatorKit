using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IPlayerLocalUI : IPlayerLocal
    {
        void SetEnabled(bool enabled);
        RectTransform RectTransform { get; }
    }
}
