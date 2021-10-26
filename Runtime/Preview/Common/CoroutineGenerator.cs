#if UNITY_EDITOR
using System.Collections;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.Common
{
    public sealed class CoroutineGenerator : MonoBehaviour
    {
        static CoroutineGenerator instance;

        static CoroutineGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    var gameObject = new GameObject(nameof(CoroutineGenerator))
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    instance = gameObject.AddComponent<CoroutineGenerator>();
                }
                return instance;
            }
        }

        public static void StartStaticCoroutine(IEnumerator enumerable)
        {
            Instance.StartCoroutine(enumerable);
        }
    }
}
#endif
