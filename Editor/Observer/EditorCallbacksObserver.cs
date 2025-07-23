using System;
using System.Reflection;
using ClusterVR.CreatorKit.Editor.Analytics;
using UnityEditor;
using UnityEngine.Events;

namespace ClusterVR.CreatorKit.Editor.Observer
{
    [InitializeOnLoad]
    public static class EditorCallbacksObserver
    {
        static DateTime PreviewStartedAt;

        static ulong PreviewDurationMs
        {
            get
            {
                return PreviewStartedAt == default ? 0 : (ulong) (DateTime.Now - PreviewStartedAt).TotalMilliseconds;
            }
        }

        static EditorCallbacksObserver()
        {
            try
            {
                var projectWasLoadedProperty = typeof(EditorApplication)
                    .GetField("projectWasLoaded", BindingFlags.Static | BindingFlags.NonPublic);
                if (projectWasLoadedProperty != null)
                {
                    var projectWasLoaded = projectWasLoadedProperty.GetValue(null) as UnityAction;
                    projectWasLoaded += PanamaLogger.LogCckOpenUnity;
                    projectWasLoadedProperty.SetValue(null, projectWasLoaded);
                }
            }
            catch (Exception)
            {
            }

            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        static void PlayModeStateChanged(PlayModeStateChange playMode)
        {
            switch (playMode)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    PreviewStartedAt = DateTime.Now;
                    PanamaLogger.LogCckEditorPreviewStart();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    PanamaLogger.LogCckEditorPreviewStop(PreviewDurationMs);
                    break;
            }
        }
    }
}
