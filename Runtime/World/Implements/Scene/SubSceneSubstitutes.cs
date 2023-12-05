using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.World
{
    public sealed class SubSceneSubstitutes : MonoBehaviour, ISubSceneSubstitutes
    {
        [SerializeField] SubScene subScene;

        ISubScene ISubSceneSubstitutes.SubScene => subScene;

        void ISubSceneSubstitutes.SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}
