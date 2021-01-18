using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface ISafeArea
    {
        RectTransform RectTransform { get; }
        void SetSafeArea(Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax);
    }
}
