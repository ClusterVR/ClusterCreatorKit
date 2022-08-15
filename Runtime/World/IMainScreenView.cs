using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IMainScreenView
    {
        float AspectRatio { get; }

        void UpdateContent(Texture texture, bool requiresYFlip = false);
        event Action OnDestroyed;
    }
}
