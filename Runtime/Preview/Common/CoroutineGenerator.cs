#if UNITY_EDITOR
using System.Collections;
using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.Common
{
    [AddComponentMenu("")]
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
                        hideFlags = HideFlags.HideAndDontSave & ~HideFlags.DontSaveInEditor,
                    };
                    instance = gameObject.AddComponent<CoroutineGenerator>();
                }
                return instance;
            }
        }

        public static Coroutine StartStaticCoroutine(IEnumerator enumerable)
        {
            return Instance.StartCoroutine(enumerable);
        }

        public static void StopStaticCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                Instance.StopCoroutine(coroutine);
            }
        }
    }
}
#endif
