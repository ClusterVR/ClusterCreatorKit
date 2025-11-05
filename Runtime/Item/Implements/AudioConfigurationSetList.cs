using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class AudioConfigurationSetList : MonoBehaviour, IAudioConfigurationSetList, IIdContainer
    {
        [SerializeField] AudioConfigurationSet[] audioConfigurationSets;

        public IEnumerable<AudioConfigurationSet> AudioConfigurationSets => audioConfigurationSets;
        IEnumerable<string> IIdContainer.Ids => audioConfigurationSets.Select(s => s.Id);

        public void Construct(AudioConfigurationSet[] audioConfigurationSets)
        {
            this.audioConfigurationSets = audioConfigurationSets;
        }
    }
}
