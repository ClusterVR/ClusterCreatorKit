using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ClusterVR.CreatorKit.World
{
    public interface ISubScene
    {
        string SceneName { get; }
        event OnStayAffectableAreaEventHandler OnStayAffectableAreaEvent;
        event OnLeaveAffectableAreaEventHandler OnLeaveAffectableAreaEvent;

#if UNITY_EDITOR
        SceneAsset UnityScene { get; }
#endif
    }

    public delegate void OnStayAffectableAreaEventHandler(OnStayAffectableAreaEventArgs e);

    public sealed class OnStayAffectableAreaEventArgs : EventArgs
    {
        public GameObject StayObject { get; }
        public ISubScene SubScene { get; }

        public OnStayAffectableAreaEventArgs(GameObject stayObject, ISubScene subScene)
        {
            StayObject = stayObject;
            SubScene = subScene;
        }
    }

    public delegate void OnLeaveAffectableAreaEventHandler(OnLeaveAffectableAreaEventArgs e);

    public sealed class OnLeaveAffectableAreaEventArgs : EventArgs
    {
        public GameObject LeaveObject { get; }
        public ISubScene SubScene { get; }

        public OnLeaveAffectableAreaEventArgs(GameObject leaveObject, ISubScene subScene)
        {
            LeaveObject = leaveObject;
            SubScene = subScene;
        }
    }
}
