using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.Speaker
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class Speaker : MonoBehaviour, ISpeaker
    {
        AudioSource audioSource;

        public AudioSource AudioSource
        {
            get
            {
                if (audioSource == null)
                {
                    audioSource = GetComponent<AudioSource>();
                    Initialize();
                }
                return audioSource;
            }
        }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            Initialize();
        }

        void Initialize()
        {
            audioSource.enabled = true;
            audioSource.loop = true;
        }
    }
}
