using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VGltf.Unity
{
    public static class Utils
    {
        public static void Destroy(UnityEngine.Object o)
        {
            if (o == null)
            {
                return;
            }

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                UnityEngine.Object.DestroyImmediate(o);
            }
            else
#endif
            {
                UnityEngine.Object.Destroy(o);
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
    
        public struct DebugStopwatch : System.IDisposable
        {
            readonly System.Diagnostics.Stopwatch _stopwatch;
            readonly string _name;

            public DebugStopwatch(string name)
            {
                _name = name;
                _stopwatch = new System.Diagnostics.Stopwatch();

                _stopwatch.Start();
            }

            public void Dispose()
            {
                _stopwatch.Stop();

                float elapsed = (float)_stopwatch.Elapsed.TotalMilliseconds;
                Debug.Log($"D '{_name}': {elapsed}ms");
            }
        }

        public static DebugStopwatch MeasureAndPrintTime(string name)
        {
            return new DebugStopwatch(name);
        }
    }
}
