using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [Serializable]
    public sealed class HumanoidAnimationListEntry : IHumanoidAnimationListEntry
    {
        [SerializeField] string id;
#if UNITY_EDITOR
        [SerializeField] AnimationClip animation;
        public AnimationClip Animation => animation;
#endif
        [SerializeField, HideInInspector] HumanoidAnimation humanoidAnimation;

        public string Id => id;
        public IHumanoidAnimation HumanoidAnimation => humanoidAnimation;

        public HumanoidAnimationListEntry(string id, HumanoidAnimation humanoidAnimation)
        {
            this.id = id;
            this.humanoidAnimation = humanoidAnimation;
        }

#if UNITY_EDITOR
        public void SetHumanoidAnimation(HumanoidAnimation humanoidAnimation)
        {
            this.humanoidAnimation = humanoidAnimation;
        }
#endif
    }
}
