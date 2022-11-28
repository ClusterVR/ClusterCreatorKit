using System;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.Utils
{
    public static class BoundsCalculator
    {
        public static void CalcLocalBounds(GameObject gameObject, out Bounds? rendererBounds, out Bounds? colliderBounds)
        {
            var previewScene = EditorSceneManager.NewPreviewScene();
            try
            {
                var go = Object.Instantiate(gameObject);
                try
                {
                    SceneManager.MoveGameObjectToScene(go, previewScene);
                }
                catch
                {
                    Object.Destroy(go);
                    throw;
                }
                go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                rendererBounds = CalcTotalBounds(go.GetComponentsInChildren<Renderer>(true), r => r.bounds);
                colliderBounds = CalcTotalBounds(go.GetComponentsInChildren<Collider>(true), c => c.bounds);
            }
            finally
            {
                EditorSceneManager.ClosePreviewScene(previewScene);
            }
        }

        static Bounds? CalcTotalBounds<T>(T[] sources, Func<T, Bounds> boundsGetter)
        {
            if (sources == null || sources.Length == 0) return null;
            return sources
                .Select(boundsGetter)
                .Aggregate((result, b) =>
                {
                    result.Encapsulate(b);
                    return result;
                });
        }
    }
}
