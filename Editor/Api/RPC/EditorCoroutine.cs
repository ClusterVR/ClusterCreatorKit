using System.Collections;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class EditorCoroutine
    {
        public static void Start(IEnumerator routine)
        {
            var coroutine = new EditorCoroutine(routine);
            coroutine.Start();
        }

        readonly IEnumerator routine;

        EditorCoroutine(IEnumerator routine)
        {
            this.routine = routine;
        }

        void Stop()
        {
            EditorApplication.update -= Update;
        }

        void Start()
        {
            EditorApplication.update += Update;
        }

        void Update()
        {
            if (!routine.MoveNext())
            {
                Stop();
            }
        }
    }
}
