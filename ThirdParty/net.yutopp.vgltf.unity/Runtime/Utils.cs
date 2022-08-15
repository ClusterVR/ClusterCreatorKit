using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VGltf.Unity
{
    public static class Utils
    {
        public static void Destroy(Object go)
        {
            if (go == null)
            {
                return;
            }

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                GameObject.DestroyImmediate(go);
            }
            else
#endif
            {
                GameObject.Destroy(go);
            }
        }

        public sealed class DestroyOnDispose<T> : System.IDisposable where T : Object
        {
            public T Value { get; private set; }

            public DestroyOnDispose(T obj)
            {
                Value = obj;
            }

            public void Dispose()
            {
                Utils.Destroy(Value);
                Value = null;
            }
        }
    }
}