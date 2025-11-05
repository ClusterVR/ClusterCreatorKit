using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    [Serializable]
    public struct AudioConfigurationSet
    {
        [SerializeField, IdString] string id;
        [SerializeField] AudioSource audioSource;

        public string Id => id;
        public AudioSource AudioSource => audioSource;

        public AudioConfigurationSet(string id, AudioSource audioSource)
        {
            this.id = id;
            this.audioSource = audioSource;
        }
    }
}
