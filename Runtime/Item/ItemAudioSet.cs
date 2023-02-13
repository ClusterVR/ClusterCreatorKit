using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    [Serializable]
    public struct ItemAudioSet
    {
        [SerializeField, IdString] string id;
        [SerializeField] AudioClip audioClip;
        [SerializeField] bool loop;

        public string Id => id;
        public AudioClip AudioClip => audioClip;
        public bool Loop => loop;

        public ItemAudioSet(string id, AudioClip audioClip, bool loop)
        {
            this.id = id;
            this.audioClip = audioClip;
            this.loop = loop;
        }
    }
}
