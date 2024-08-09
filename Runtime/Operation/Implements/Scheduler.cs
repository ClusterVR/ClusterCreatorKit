using System;
using System.Collections;
using UnityEngine;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    [AddComponentMenu("")]
    public sealed class Scheduler : MonoBehaviour
    {
        static Scheduler instance;

        static Scheduler Instance
        {
            get
            {
                if (instance == null)
                {
                    var gameObject = new GameObject("Scheduler")
                    {
                        hideFlags = HideFlags.HideAndDontSave & ~HideFlags.DontSaveInEditor,
                    };
                    instance = gameObject.AddComponent<Scheduler>();
                }

                return instance;
            }
        }

        static IEnumerator DelayAction(TimeSpan dueTime, Action action, Cancellation cancellation)
        {
            if (dueTime <= TimeSpan.Zero)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSecondsRealtime((float) dueTime.TotalSeconds);
            }

            if (cancellation.IsDisposed)
            {
                yield break;
            }
            action.Invoke();
        }

        public static void Schedule(TimeSpan dueTime, Action action, Cancellation cancellation)
        {
            Instance.StartCoroutine(DelayAction(dueTime, action, cancellation));
        }

        public sealed class Cancellation : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                if (!IsDisposed)
                {
                    IsDisposed = true;
                }
            }
        }
    }
}
