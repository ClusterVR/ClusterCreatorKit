using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface IMainScreenView
    {
        void UpdateContent(Texture texture, bool requiresYFlip = false);
        event Action OnDestroyed;
    }
}
