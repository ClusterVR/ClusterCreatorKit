using System;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.Editor.ItemExporter
{
    public sealed class ItemPreviewImage : IDisposable
    {
        const int TextureSize = 1024;

        readonly RenderTexture renderTexture = RenderTexture.GetTemporary(TextureSize, TextureSize);
        readonly Texture2D texture = new Texture2D(TextureSize, TextureSize);

        public byte[] CreatePNG(GameObject go)
        {
            if (go == null) throw new ArgumentNullException();
            var item = go.GetComponent<IItem>();
            if (item == null) throw new ArgumentException();

            var currentActiveRenderTexture = RenderTexture.active;

            var previewScene = EditorSceneManager.NewPreviewScene();

            try
            {
                var subject = Object.Instantiate(go);
                SceneManager.MoveGameObjectToScene(subject, previewScene);
                subject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

                var light = CreateLight();
                SceneManager.MoveGameObjectToScene(light.gameObject, previewScene);

                var camera = CreateCamera(EncapsulationBounds(subject));
                SceneManager.MoveGameObjectToScene(camera.gameObject, previewScene);
                camera.targetTexture = renderTexture;
                camera.scene = previewScene;
                camera.Render();
                RenderTexture.active = renderTexture;
                texture.ReadPixels(new Rect(0, 0, TextureSize, TextureSize), 0, 0);
                texture.Apply();
                return texture.EncodeToPNG();
            }
            finally
            {
                RenderTexture.active = currentActiveRenderTexture;
                EditorSceneManager.ClosePreviewScene(previewScene);
            }
        }

        static Bounds EncapsulationBounds(GameObject go)
        {
            var activeRenderers = go.GetComponentsInChildren<Renderer>();
            if (activeRenderers.Length == 0)
            {
                return new Bounds(Vector3.zero, Vector3.one);
            }

            return activeRenderers.Select(r => r.bounds)
                .Aggregate(((result, current) =>
                {
                    result.Encapsulate(current);
                    return result;
                }));
        }

        static Camera CreateCamera(Bounds bounds)
        {
            var go = new GameObject("Camera");
            var camera = go.AddComponent<Camera>();
            var rot = Quaternion.Euler(30f, 135f, 0f);
            var pos = bounds.center + Mathf.Max(10f, bounds.size.magnitude) * (rot * Vector3.back);
            camera.transform.SetPositionAndRotation(pos, rot);
            camera.cameraType = CameraType.Preview;
            camera.orthographic = true;
            camera.orthographicSize = Mathf.Max(bounds.size.magnitude, Constants.Constants.ItemPreviewMagnificationLimitDiagonalSize) * 0.6f;
            camera.backgroundColor = Color.clear;
            camera.clearFlags = CameraClearFlags.SolidColor;
            return camera;
        }

        static Light CreateLight()
        {
            var go = new GameObject("Light");
            var light = go.AddComponent<Light>();
            light.transform.rotation = Quaternion.Euler(40, 180, 0);
            light.type = LightType.Directional;
            light.color = Color.white;
            return light;
        }

        public void Dispose()
        {
            RenderTexture.ReleaseTemporary(renderTexture);
            Object.DestroyImmediate(texture);
        }
    }
}
