/* ========= Copyright 2016-2017, HTC Corporation. All rights reserved. =========== */

using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Extensions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace ClusterVR.CreatorKit.World.Implements.Mirror
{
    [DisallowMultipleComponent, RequireComponent(typeof(MeshRenderer))]
    public class Mirror : MonoBehaviour, IMirror
    {
#if CLUSTER_QUEST
        const int MaxResolution = 768;
#elif UNITY_IOS || UNITY_ANDROID
        const int MaxResolution = 1024;
#else
        const int MaxResolution = 2048;
#endif
        const int MsaaLevel = 4;

        const string ShaderName = "ClusterCreatorKit/Mirror";
        const string ObjectSpaceKeyword = "USE_OBJECT_SPACE";
        const string MirrorRenderingKeyword = "IS_MIRROR_RENDERING";

        static readonly Rect FullViewport = new Rect(0f, 0f, 1f, 1f);

        static readonly int LeftEyeTextureId = Shader.PropertyToID("_LeftEyeTexture");
        static readonly int RightEyeTextureId = Shader.PropertyToID("_RightEyeTexture");

        static bool IsUrp() => RenderPipelineManager.currentPipeline != null;

        readonly struct MirrorRenderData
        {
            public readonly Vector3 MirrorPosition;
            public readonly Vector3 MirrorNormal;
            public readonly Matrix4x4 Reflection;
            public readonly Matrix4x4 WorldToCameraMatrix;
            public readonly Matrix4x4 ProjectionMatrix;
            public readonly Vector3 WorldSpaceCameraPos;
            public readonly Vector3 ReflectedWorldSpaceCameraPos;

            public MirrorRenderData(Vector3 mirrorPosition,
                Vector3 mirrorNormal,
                Matrix4x4 reflection,
                Matrix4x4 worldToCameraMatrix,
                Matrix4x4 projectionMatrix,
                Vector3 worldSpaceCameraPos,
                Vector3 reflectedWorldSpaceCameraPos)
            {
                MirrorPosition = mirrorPosition;
                MirrorNormal = mirrorNormal;
                Reflection = reflection;
                WorldToCameraMatrix = worldToCameraMatrix;
                ProjectionMatrix = projectionMatrix;
                WorldSpaceCameraPos = worldSpaceCameraPos;
                ReflectedWorldSpaceCameraPos = reflectedWorldSpaceCameraPos;
            }
        }

        static bool insideRendering;

        Camera preRenderCamera;
        Renderer cachedRenderer;
        Material material;
        RenderTexture renderTexture;
        bool isRenderPrepared;

        protected virtual LayerMask CullingMask { get; } = 704609495;

        static readonly LayerMask AvatarOnlyCullingMask = 25363648;

        protected virtual bool UseLightweightStereo =>
#if CLUSTER_QUEST
            true;
#else
            false;
#endif
        const float MonauralAcceptableDistanceRateToConvergence = 0.1f;

        bool HasMirrorMaterial() => material != null && (material.HasProperty(LeftEyeTextureId) || material.HasProperty(RightEyeTextureId));

        bool HasSize()
        {
            var scale = transform.lossyScale;
            return !Mathf.Approximately(scale.x, 0f) &&
                !Mathf.Approximately(scale.y, 0f) &&
                !Mathf.Approximately(scale.z, 0f);
        }

        (Camera camera, int frameCount)? preservingFor;

        readonly Dictionary<Camera, int> cameraLastRenderedFrame = new();
        int lastCleanupFrame;

        void Start()
        {
            cachedRenderer = GetComponent<Renderer>();
            var sharedMaterials = RendererMaterialUtility.GetSharedMaterials(cachedRenderer);
            if (sharedMaterials.Length == 0)
            {
                return;
            }
            var targetMaterial = sharedMaterials[0];
            if (targetMaterial == null)
            {
                return;
            }

            material = Instantiate(targetMaterial);
            if (material.shader.name == ShaderName)
            {
                material.shader = Shader.Find(ShaderName);
            }

            var newMaterials = new Material[sharedMaterials.Length];
            newMaterials[0] = material;
            Array.Copy(sharedMaterials, 1, newMaterials, 1, sharedMaterials.Length - 1);
            RendererMaterialUtility.SetMaterials(cachedRenderer, newMaterials);

            preRenderCamera = new GameObject("PreRenderCamera").AddComponent<Camera>();
            preRenderCamera.transform.SetParent(transform, worldPositionStays: false);
            preRenderCamera.enabled = false;
        }

        void OnEnable()
        {
            RenderPipelineManager.beginContextRendering += OnBeginContextRendering;
            RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
            RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.update += OnEditorUpdate;
#endif
        }

        void OnDisable()
        {
            RenderPipelineManager.beginContextRendering -= OnBeginContextRendering;
            RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
            RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.update -= OnEditorUpdate;
#endif
            cameraLastRenderedFrame.Clear();
        }

        void OnBeginContextRendering(ScriptableRenderContext _, List<Camera> __)
        {
            var frameCount = Time.frameCount;
            if (lastCleanupFrame != frameCount)
            {
                cameraLastRenderedFrame.Clear();
                lastCleanupFrame = frameCount;
            }
        }

        void OnBeginCameraRendering(ScriptableRenderContext _, Camera camera)
        {
            if (insideRendering) return;
#if UNITY_EDITOR
            if (camera.cameraType is CameraType.SceneView or CameraType.Preview) return;
#endif
            if (!HasMirrorMaterial()) return;

            var frameCount = Time.frameCount;
            var isSecondRendering = cameraLastRenderedFrame.TryGetValue(camera, out var lastFrame) && lastFrame == frameCount;
            cameraLastRenderedFrame[camera] = frameCount;

            if (CheckPreserved(camera)) return;
            if (isRenderPrepared) return;
            if (!HasSize()) return;
            if (!WillBeRenderedBy(camera)) return;

            if (camera.stereoEnabled)
            {
                var eye = isSecondRendering ? Camera.StereoscopicEye.Right : Camera.StereoscopicEye.Left;
                Prepare(camera, camera.GetStereoViewMatrix(eye), camera.GetStereoProjectionMatrix(eye));
            }
            else
            {
                Prepare(camera, camera.worldToCameraMatrix, camera.projectionMatrix);
            }
        }

        void OnWillRenderObject()
        {
            if (insideRendering) return;
            if (IsUrp()) return;
            if (!HasMirrorMaterial()) return;
            var currentCamera = Camera.current;
            if (CheckPreserved(currentCamera)) return;
            if (isRenderPrepared) return;
            if (!HasSize()) return;

            if (currentCamera.stereoEnabled)
            {
                var eye = (Camera.StereoscopicEye) currentCamera.stereoActiveEye;
                Prepare(currentCamera, currentCamera.GetStereoViewMatrix(eye), currentCamera.GetStereoProjectionMatrix(eye));
            }
            else
            {
                Prepare(currentCamera, currentCamera.worldToCameraMatrix, currentCamera.projectionMatrix);
            }
        }

        bool CheckPreserved(Camera targetCamera)
        {
            var currentRender = (targetCamera, Time.frameCount);
            if (preservingFor is { } preserving)
            {
                if (preserving == currentRender)
                {
                    preservingFor = null;
                    return true;
                }
                else
                {
                    preservingFor = null;
                    OnEndRendering();
                }
            }
            return false;
        }

        void Prepare(Camera targetCamera, Matrix4x4 worldToCameraMatrix, Matrix4x4 projectionMatrix)
        {
            insideRendering = true;

            Shader.EnableKeyword(MirrorRenderingKeyword);
            GL.invertCulling = true;

            var mirrorPosition = transform.position;
            var mirrorNormal = -transform.forward;
            var reflection = CalculateReflectionMatrix(GetPlane(mirrorPosition, mirrorNormal));
            var worldSpaceCameraPos = targetCamera.transform.position;
            var reflectedWorldSpaceCameraPos = reflection.MultiplyPoint3x4(worldSpaceCameraPos);
            var mirrorData = new MirrorRenderData(
                mirrorPosition: mirrorPosition,
                mirrorNormal: mirrorNormal,
                reflection: reflection,
                worldToCameraMatrix: worldToCameraMatrix,
                projectionMatrix: projectionMatrix,
                worldSpaceCameraPos: worldSpaceCameraPos,
                reflectedWorldSpaceCameraPos: reflectedWorldSpaceCameraPos);

            preRenderCamera.transform.position = mirrorData.ReflectedWorldSpaceCameraPos;

            preRenderCamera.nearClipPlane = targetCamera.nearClipPlane;
            preRenderCamera.farClipPlane = targetCamera.farClipPlane;

            if (UseLightweightStereo && targetCamera.stereoEnabled)
            {
                var monauralAcceptableDistance = targetCamera.stereoConvergence *
                    MonauralAcceptableDistanceRateToConvergence * targetCamera.transform.lossyScale.z;
                var distance = Vector3.Dot(mirrorData.MirrorPosition - mirrorData.WorldSpaceCameraPos, -mirrorData.MirrorNormal);
                if (distance < monauralAcceptableDistance)
                {
                    PrepareScreenSpace(mirrorData, AvatarOnlyCullingMask);
                }
                else
                {
                    PrepareObjectSpace(mirrorData, targetCamera, CullingMask);
                    preservingFor = (targetCamera, Time.frameCount);
                }
            }
            else
            {
                PrepareScreenSpace(mirrorData, CullingMask);
            }

            isRenderPrepared = true;

            GL.invertCulling = false;
            Shader.DisableKeyword(MirrorRenderingKeyword);

            insideRendering = false;
        }

        void PrepareObjectSpace(in MirrorRenderData mirrorData, Camera targetCamera, LayerMask cullingMask)
        {
            GetRenderTextureSize(out var width, out var height);
            var texture = GetOrCreateRenderTexture(width, height);

            var cullingMatrices = new[]
            {
                targetCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left) * targetCamera.GetStereoViewMatrix(Camera.StereoscopicEye.Left),
                targetCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right) * targetCamera.GetStereoViewMatrix(Camera.StereoscopicEye.Right)
            };
            RenderObjectSpace(mirrorData, texture, targetCamera.farClipPlane, cullingMatrices, cullingMask);

            material.EnableKeyword(ObjectSpaceKeyword);
            material.SetTexture(LeftEyeTextureId, texture);
            material.SetTexture(RightEyeTextureId, texture);
        }

        void PrepareScreenSpace(in MirrorRenderData mirrorData, LayerMask cullingMask)
        {
            GetRenderTextureSize(out var width, out var height);

            var texture = GetOrCreateRenderTexture(width, height);

            Render(mirrorData, texture, cullingMask);

            material.DisableKeyword(ObjectSpaceKeyword);
            material.SetTexture(LeftEyeTextureId, texture);
            material.SetTexture(RightEyeTextureId, texture);
        }

        RenderTexture GetOrCreateRenderTexture(int width, int height)
        {
            Assert.IsNull(renderTexture);
            if (renderTexture == null) // safety
            {
                renderTexture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.Default,
                    RenderTextureReadWrite.Default, MsaaLevel);
            }

            return renderTexture;
        }

        void Render(in MirrorRenderData mirrorData, RenderTexture targetRenderTexture, LayerMask cullingMask)
        {
            var rect = GetScissorRect(mirrorData.ProjectionMatrix * mirrorData.WorldToCameraMatrix);
            if (rect.width <= 0f || rect.height <= 0f)
            {
                return;
            }
            var worldToCameraMatrix = mirrorData.WorldToCameraMatrix * mirrorData.Reflection;
            preRenderCamera.worldToCameraMatrix = worldToCameraMatrix;
            preRenderCamera.projectionMatrix = GetScissorMatrix(rect) * mirrorData.ProjectionMatrix;
            var localPos = worldToCameraMatrix.MultiplyPoint(mirrorData.MirrorPosition);
            var localNormal = worldToCameraMatrix.MultiplyVector(mirrorData.MirrorNormal);
            var localPlane = new Vector4(localNormal.x, localNormal.y, localNormal.z,
                -Vector3.Dot(localPos, localNormal));
            preRenderCamera.projectionMatrix = preRenderCamera.CalculateObliqueMatrix(localPlane);
            SetRect(rect, material);

            preRenderCamera.cullingMask = cullingMask;

            DoRender(preRenderCamera, targetRenderTexture);
        }

        void RenderObjectSpace(in MirrorRenderData mirrorData, RenderTexture targetRenderTexture, float farClip,
            Matrix4x4[] cullingMatrices, LayerMask cullingMask)
        {
            var worldSpaceCameraPos = mirrorData.WorldSpaceCameraPos;
            var scale = transform.lossyScale;
            preRenderCamera.worldToCameraMatrix =
                Matrix4x4.TRS(worldSpaceCameraPos, transform.rotation, ZFlip(scale)).inverse * mirrorData.Reflection;
            var mirrorLocalCameraPoint = transform.InverseTransformPoint(worldSpaceCameraPos);
            var rect = GetObjectSpaceScissorRect(cullingMatrices);
            if (rect.width <= 0f || rect.height <= 0f)
            {
                return;
            }
            var projectionMatrix = Matrix4x4.Frustum(
                -mirrorLocalCameraPoint.x - 0.5f + rect.xMin,
                -mirrorLocalCameraPoint.x - 0.5f + rect.xMax,
                -mirrorLocalCameraPoint.y - 0.5f + rect.yMin,
                -mirrorLocalCameraPoint.y - 0.5f + rect.yMax,
                -mirrorLocalCameraPoint.z,
                farClip / scale.z);
            preRenderCamera.projectionMatrix = projectionMatrix;
            SetRect(rect, material);

            preRenderCamera.cullingMask = cullingMask;

            DoRender(preRenderCamera, targetRenderTexture);
        }

        void DoRender(Camera camera, RenderTexture targetRenderTexture)
        {
#if UNITY_URP
            if (IsUrp())
            {
                RenderPipeline.SubmitRenderRequest(
                    camera, new UnityEngine.Rendering.Universal.UniversalRenderPipeline.SingleCameraRequest { destination = targetRenderTexture });
            }
            else
#endif
            {
                camera.targetTexture = targetRenderTexture;
                camera.Render();
            }
        }

        Rect GetScissorRect(Matrix4x4 mat)
        {
            var bounds = cachedRenderer.bounds;
            var cen = bounds.center;
            var ext = bounds.extents;
            var extentPoints = new Vector3[8]
            {
                WorldPointToViewport(mat, new Vector3(cen.x - ext.x, cen.y - ext.y, cen.z - ext.z)),
                WorldPointToViewport(mat, new Vector3(cen.x + ext.x, cen.y - ext.y, cen.z - ext.z)),
                WorldPointToViewport(mat, new Vector3(cen.x - ext.x, cen.y - ext.y, cen.z + ext.z)),
                WorldPointToViewport(mat, new Vector3(cen.x + ext.x, cen.y - ext.y, cen.z + ext.z)),
                WorldPointToViewport(mat, new Vector3(cen.x - ext.x, cen.y + ext.y, cen.z - ext.z)),
                WorldPointToViewport(mat, new Vector3(cen.x + ext.x, cen.y + ext.y, cen.z - ext.z)),
                WorldPointToViewport(mat, new Vector3(cen.x - ext.x, cen.y + ext.y, cen.z + ext.z)),
                WorldPointToViewport(mat, new Vector3(cen.x + ext.x, cen.y + ext.y, cen.z + ext.z))
            };

            Vector2 min = extentPoints[0];
            Vector2 max = extentPoints[0];
            foreach (var v in extentPoints)
            {
                if (v.z < 0)
                {
                    return FullViewport;
                }

                min = Vector2.Min(min, v);
                max = Vector2.Max(max, v);
            }

            min = Vector2.Max(min, Vector2.zero);
            max = Vector2.Min(max, Vector2.one);

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        Rect GetObjectSpaceScissorRect(Matrix4x4[] mats)
        {
            var rects = mats.Select(GetObjectSpaceScissorRect).ToArray();
            return Rect.MinMaxRect(
                rects.Select(r => r.xMin).Min(),
                rects.Select(r => r.yMin).Min(),
                rects.Select(r => r.xMax).Max(),
                rects.Select(r => r.yMax).Max());
        }

        Rect GetObjectSpaceScissorRect(Matrix4x4 mat)
        {
            var worldToLocal = transform.worldToLocalMatrix;
            var clippingToObject = worldToLocal * Matrix4x4.Inverse(mat);
            var corners = new Vector2[4]
            {
                new Vector2(-1f, -1f),
                new Vector2(1f, -1f),
                new Vector2(-1f, 1f),
                new Vector2(1f, 1f),
            };

            Vector2 min = Vector2.one;
            Vector2 max = Vector2.zero;
            foreach (var corner in corners)
            {
                var localFrom = clippingToObject.MultiplyPoint(new Vector3(corner.x, corner.y, 0f));
                var localTo = clippingToObject.MultiplyPoint(new Vector3(corner.x, corner.y, 1f));

                var depth = localTo.z - localFrom.z;
                if (depth <= 0f)
                {
                    return FullViewport;
                }

                var intersect = Vector2.LerpUnclamped(localFrom, localTo, -localFrom.z / depth);
                intersect += new Vector2(0.5f, 0.5f);

                min = Vector2.Min(min, intersect);
                max = Vector2.Max(max, intersect);
            }

            min = Vector2.Max(min, Vector2.zero);
            max = Vector2.Min(max, Vector2.one);

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        static Matrix4x4 GetScissorMatrix(Rect rect)
        {
            var m2 = Matrix4x4.TRS(
                new Vector3(1 / rect.width - 1, 1 / rect.height - 1, 0),
                Quaternion.identity,
                new Vector3(1 / rect.width, 1 / rect.height, 1));

            var m3 = Matrix4x4.TRS(
                new Vector3(-rect.x * 2 / rect.width, -rect.y * 2 / rect.height, 0),
                Quaternion.identity,
                Vector3.one);

            return m3 * m2;
        }

        static Vector3 WorldPointToViewport(Matrix4x4 mat, Vector3 point)
        {
            Vector3 result;
            result.x = mat.m00 * point.x + mat.m01 * point.y + mat.m02 * point.z + mat.m03;
            result.y = mat.m10 * point.x + mat.m11 * point.y + mat.m12 * point.z + mat.m13;
            result.z = mat.m20 * point.x + mat.m21 * point.y + mat.m22 * point.z + mat.m23;

            var a = mat.m30 * point.x + mat.m31 * point.y + mat.m32 * point.z + mat.m33;
            a = 1.0f / a;
            result.x *= a;
            result.y *= a;
            result.z = a;

            point = result;
            point.x = point.x * 0.5f + 0.5f;
            point.y = point.y * 0.5f + 0.5f;

            return point;
        }

        static void SetRect(Rect rect, Material material)
        {
            SetInversedST(material, LeftEyeTextureId, rect);
            SetInversedST(material, RightEyeTextureId, rect);
        }

        static void SetInversedST(Material material, int textureId, Rect rect)
        {
            if (!material.HasProperty(textureId))
            {
                return;
            }
            var inversedSize = new Vector2(1f / rect.width, 1f / rect.height);
            material.SetTextureOffset(textureId, -rect.min * inversedSize);
            material.SetTextureScale(textureId, inversedSize);
        }

        void OnRenderObject() => OnEndRendering();
        void OnEndCameraRendering(ScriptableRenderContext _, Camera __) => OnEndRendering();

        void OnEndRendering()
        {
            if (!insideRendering && isRenderPrepared && !preservingFor.HasValue)
            {
                ReleaseTemporalRenderTextures();
                isRenderPrepared = false;
            }
        }

        void OnDestroy()
        {
            ReleaseTemporalRenderTextures();
            if (material != null)
            {
                Destroy(material);
            }
            if (cachedRenderer != null)
            {
                RendererMaterialUtility.ClearMaterials(cachedRenderer);
            }
        }

        void ReleaseTemporalRenderTextures()
        {
            if (preRenderCamera != null)
            {
                preRenderCamera.targetTexture = null;
            }

            if (renderTexture != null)
            {
                RenderTexture.ReleaseTemporary(renderTexture);
                renderTexture = null;
            }
        }

        static int renderTextureWidth;
        static int renderTextureHeight;
        static int renderTextureUpdatedFrame;
        const int KeepSizeFrames = 60;

        static void GetRenderTextureSize(out int width, out int height)
        {
            var currentFrameCount = Time.frameCount;
            if (renderTextureUpdatedFrame > 0 && currentFrameCount < renderTextureUpdatedFrame + KeepSizeFrames)
            {
                width = renderTextureWidth;
                height = renderTextureHeight;
                return;
            }

            var preferredWidth = XRSettings.enabled ? XRSettings.eyeTextureWidth : Screen.width;
            var preferredHeight = XRSettings.enabled ? XRSettings.eyeTextureHeight : Screen.height;
            preferredWidth = preferredWidth == 0 ? Screen.width : preferredWidth;
            preferredHeight = preferredHeight == 0 ? Screen.height : preferredHeight;
            width = Math.Min(preferredWidth, MaxResolution);
            height = Math.Min(preferredHeight, MaxResolution);
            if (width != renderTextureWidth || height != renderTextureHeight)
            {
                renderTextureWidth = width;
                renderTextureHeight = height;
                renderTextureUpdatedFrame = currentFrameCount;
            }
        }

        static Vector4 GetPlane(Vector3 position, Vector3 normal)
        {
            var w = Vector3.Dot(normal, position);
            return new Vector4(normal.x, normal.y, normal.z, -w);
        }

        static Matrix4x4 CalculateReflectionMatrix(Vector4 plane)
        {
            Matrix4x4 reflectionMat;
            reflectionMat.m00 = 1F - 2F * plane[0] * plane[0];
            reflectionMat.m01 = -2F * plane[0] * plane[1];
            reflectionMat.m02 = -2F * plane[0] * plane[2];
            reflectionMat.m03 = -2F * plane[3] * plane[0];

            reflectionMat.m10 = -2F * plane[1] * plane[0];
            reflectionMat.m11 = 1F - 2F * plane[1] * plane[1];
            reflectionMat.m12 = -2F * plane[1] * plane[2];
            reflectionMat.m13 = -2F * plane[3] * plane[1];

            reflectionMat.m20 = -2F * plane[2] * plane[0];
            reflectionMat.m21 = -2F * plane[2] * plane[1];
            reflectionMat.m22 = 1F - 2F * plane[2] * plane[2];
            reflectionMat.m23 = -2F * plane[3] * plane[2];

            reflectionMat.m30 = 0F;
            reflectionMat.m31 = 0F;
            reflectionMat.m32 = 0F;
            reflectionMat.m33 = 1F;
            return reflectionMat;
        }

        static Vector3 ZFlip(Vector3 vec)
        {
            vec.z = -vec.z;
            return vec;
        }

        static readonly Plane[] FrustrumPlanes = new Plane[6];

        bool WillBeRenderedBy(Camera camera)
        {
            if (((1 << gameObject.layer) & camera.cullingMask) == 0)
            {
                return false;
            }
            GeometryUtility.CalculateFrustumPlanes(camera, FrustrumPlanes);
            return GeometryUtility.TestPlanesAABB(FrustrumPlanes, cachedRenderer.bounds);
        }

#if UNITY_EDITOR
        void OnEditorUpdate()
        {
            if (HasMirrorMaterial())
            {
                material.SetTexture(LeftEyeTextureId, null);
                material.SetTexture(RightEyeTextureId, null);
            }
        }
#endif
    }
}
