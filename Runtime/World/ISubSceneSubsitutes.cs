using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public interface ISubSceneSubstitutes
    {
        ISubScene SubScene { get; }
        void SetActive(bool isActive);
    }
}
