using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [DisallowMultipleComponent]
    public sealed class HumanoidAnimationList : MonoBehaviour, IHumanoidAnimationList, IIdContainer
    {
        [SerializeField] HumanoidAnimationListEntry[] humanoidAnimations;

        public IReadOnlyCollection<IHumanoidAnimationListEntry> HumanoidAnimations => humanoidAnimations;
        IEnumerable<string> IIdContainer.Ids => humanoidAnimations.Select(a => a.Id);

#if UNITY_EDITOR
        public IReadOnlyCollection<HumanoidAnimationListEntry> RawHumanoidAnimations => humanoidAnimations;
#endif

        public void Construct(HumanoidAnimationListEntry[] humanoidAnimations)
        {
            this.humanoidAnimations = humanoidAnimations;
        }
    }
}
