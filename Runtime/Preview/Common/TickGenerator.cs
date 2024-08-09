using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.Common
{
    [AddComponentMenu("")]
    public sealed class TickGenerator : MonoBehaviour
    {
        static TickGenerator instance;

        public static TickGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    var gameObject = new GameObject(nameof(TickGenerator))
                    {
                        hideFlags = HideFlags.HideAndDontSave & ~HideFlags.DontSaveInEditor,
                    };
                    instance = gameObject.AddComponent<TickGenerator>();
                }
                return instance;
            }
        }

        public event Action OnTick;

        void Update()
        {
            OnTick?.Invoke();
        }
    }
}
