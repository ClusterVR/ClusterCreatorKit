/* ========= Copyright 2016-2017, HTC Corporation. All rights reserved. =========== */

using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR;

namespace ClusterVR.CreatorKit.World.Implements.Mirror
{
    [DisallowMultipleComponent, RequireComponent(typeof(MeshRenderer))]
    public class Mirror : MonoBehaviour
    {
        enum Eye
        {
            Monaural,
            Left,
            Right
        }

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

        static bool insideRendering;

        Camera preRenderCamera;
        Renderer cachedRenderer;
        Material material;
        RenderTexture leftRenderTexture;
        RenderTexture rightRenderTexture;
        bool isRenderPrepared;

        protected virtual LayerMask CullingMask { get; } = 704478423;

        static readonly LayerMask AvatarOnlyCullingMask = 25232576;

        protected virtual bool UseLightweightStereo =>
#if CLUSTER_QUEST
            true;
#else
            false;
#endif
        const float MonauralAcceptableDistanceRateToConvergence = 0.1f;

        void Start()
        {
            preRenderCamera = new GameObject("PreRenderCamera").AddComponent<Camera>();
            preRenderCamera.transform.SetParent(transform);
            preRenderCamera.enabled = false;

            cachedRenderer = GetComponent<Renderer>();
            material = cachedRenderer.material;

            if (material.shader.name == ShaderName)
            {
                material.shader = Shader.Find(ShaderName);
            }
        }

        void OnWillRenderObject()
        {
            if (!material.HasProperty(LeftEyeTextureId) && !material.HasProperty(RightEyeTextureId))
            {
                return;
            }

            if (insideRendering || isRenderPrepared)
            {
                return;
            }
            insideRendering = true;

            var transform = this.transform;
            var scale = transform.lossyScale;
            if (Mathf.Approximately(scale.x, 0f) || Mathf.Approximately(scale.y, 0f) ||
                Mathf.Approximately(scale.z, 0f))
            {
                insideRendering = false;
                return;
            }

            var targetCamera = Camera.current;

            preRenderCamera.cullingMask = CullingMask;

            Shader.EnableKeyword(MirrorRenderingKeyword);
            GL.invertCulling = true;

            if (targetCamera.stereoEnabled)
            {
                if (UseLightweightStereo)
                {
                    var monauralAcceptableDistance = targetCamera.stereoConvergence *
                        MonauralAcceptableDistanceRateToConvergence * targetCamera.transform.lossyScale.z;
                    var distance = Vector3.Dot(transform.position - targetCamera.transform.position, transform.forward);
                    if (distance < monauralAcceptableDistance)
                    {
                        PrepareLightWeightStereo(targetCamera);
                    }
                    else
                    {
                        PrepareObjectSpaceMonaural(targetCamera);
                    }
                }
                else
                {
                    PrepareFullStereo(targetCamera);
                }
            }
            else
            {
                PrepareScreenSpaceMonaural(targetCamera);
            }

            isRenderPrepared = true;

            GL.invertCulling = false;
            Shader.DisableKeyword(MirrorRenderingKeyword);

            insideRendering = false;
        }

        void PrepareLightWeightStereo(Camera targetCamera)
        {
            PrepareStereo(targetCamera, AvatarOnlyCullingMask);
        }

        void PrepareFullStereo(Camera targetCamera)
        {
            PrepareStereo(targetCamera, CullingMask);
        }

        void PrepareObjectSpaceMonaural(Camera targetCamera)
        {
            GetRenderTextureSize(out var width, out var height);
            var texture = GetOrCreateRenderTexture(Eye.Monaural, width, height);

            RenderObjectSpace(texture, targetCamera.transform.position, targetCamera.farClipPlane,
                targetCamera.cullingMatrix, Eye.Monaural, CullingMask);

            material.EnableKeyword(ObjectSpaceKeyword);
            material.SetTexture(LeftEyeTextureId, texture);
            material.SetTexture(RightEyeTextureId, texture);
        }

        void PrepareScreenSpaceMonaural(Camera targetCamera)
        {
            GetRenderTextureSize(out var width, out var height);
            var texture = GetOrCreateRenderTexture(Eye.Monaural, width, height);

            Render(texture, targetCamera.worldToCameraMatrix, targetCamera.projectionMatrix, Eye.Monaural, CullingMask);

            material.DisableKeyword(ObjectSpaceKeyword);
            material.SetTexture(LeftEyeTextureId, texture);
            material.SetTexture(RightEyeTextureId, texture);
        }

        void PrepareStereo(Camera targetCamera, LayerMask cullingMask)
        {
            GetRenderTextureSize(out var width, out var height);

            var leftTexture = GetOrCreateRenderTexture(Eye.Left, width, height);
            var rightTexture = GetOrCreateRenderTexture(Eye.Right, width, height);

            RenderForSingleEye(leftTexture, targetCamera, Eye.Left, cullingMask);
            RenderForSingleEye(rightTexture, targetCamera, Eye.Right, cullingMask);

            material.DisableKeyword(ObjectSpaceKeyword);
            material.SetTexture(LeftEyeTextureId, leftTexture);
            material.SetTexture(RightEyeTextureId, rightTexture);
        }

        void RenderForSingleEye(RenderTexture texture, Camera targetCamera, Eye eye, LayerMask cullingMask)
        {
            var stereoscopicEye = eye == Eye.Left ? Camera.StereoscopicEye.Left : Camera.StereoscopicEye.Right;
            Render(texture,
                targetCamera.GetStereoViewMatrix(stereoscopicEye),
                targetCamera.GetStereoProjectionMatrix(stereoscopicEye),
                eye, cullingMask);
        }

        RenderTexture GetOrCreateRenderTexture(Eye eye, int width, int height)
        {
            if (eye == Eye.Left)
            {
                Assert.IsNull(leftRenderTexture);
                if (leftRenderTexture == null) // safety
                {
                    leftRenderTexture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.Default,
                        RenderTextureReadWrite.Default, MsaaLevel);
                }

                return leftRenderTexture;
            }
            else
            {
                Assert.IsNull(rightRenderTexture);
                if (rightRenderTexture == null) // safety
                {
                    rightRenderTexture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.Default,
                        RenderTextureReadWrite.Default, MsaaLevel);
                }

                return rightRenderTexture;
            }
        }

        void Render(RenderTexture targetRenderTexture, Matrix4x4 sourceWorldToCameraMatrix,
            Matrix4x4 sourceProjectionMatrix, Eye eye, LayerMask cullingMask)
        {
            var mirrorPosition = transform.position;
            var mirrorNormal = -transform.forward;
            var rect = GetScissorRect(sourceProjectionMatrix * sourceWorldToCameraMatrix);
            if (rect.width <= 0f || rect.height <= 0f)
            {
                return;
            }
            var reflection = CalculateReflectionMatrix(GetPlane(mirrorPosition, mirrorNormal));
            var worldToCameraMatrix = sourceWorldToCameraMatrix * reflection;
            preRenderCamera.worldToCameraMatrix = worldToCameraMatrix;
            preRenderCamera.projectionMatrix = GetScissorMatrix(rect) * sourceProjectionMatrix;
            var localPos = worldToCameraMatrix.MultiplyPoint(mirrorPosition);
            var localNormal = worldToCameraMatrix.MultiplyVector(mirrorNormal);
            var localPlane = new Vector4(localNormal.x, localNormal.y, localNormal.z,
                -Vector3.Dot(localPos, localNormal));
            preRenderCamera.projectionMatrix = preRenderCamera.CalculateObliqueMatrix(localPlane);
            SetRect(rect, preRenderCamera, material, eye);

            preRenderCamera.cullingMask = cullingMask;
            preRenderCamera.targetTexture = targetRenderTexture;
            preRenderCamera.Render();
        }

        void RenderObjectSpace(RenderTexture targetRenderTexture, Vector3 position, float farClip,
            Matrix4x4 cullingMatrix, Eye eye, LayerMask cullingMask)
        {
            var mirrorPosition = transform.position;
            var mirrorNormal = -transform.forward;
            var scale = transform.lossyScale;
            var reflection = CalculateReflectionMatrix(GetPlane(mirrorPosition, mirrorNormal));
            preRenderCamera.worldToCameraMatrix =
                Matrix4x4.TRS(position, transform.rotation, ZFlip(scale)).inverse * reflection;
            var mirrorLocalCameraPoint = transform.InverseTransformPoint(position);
            var rect = GetObjectSpaceScissorRect(cullingMatrix);
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
            SetRect(rect, preRenderCamera, material, eye);

            preRenderCamera.cullingMask = cullingMask;
            preRenderCamera.targetTexture = targetRenderTexture;
            preRenderCamera.Render();
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

        static void SetRect(Rect rect, Camera camera, Material material, Eye eye)
        {
            switch (eye)
            {
                case Eye.Monaural:
                    SetInversedST(material, LeftEyeTextureId, rect);
                    SetInversedST(material, RightEyeTextureId, rect);
                    break;
                case Eye.Left:
                    SetInversedST(material, LeftEyeTextureId, rect);
                    break;
                case Eye.Right:
                    SetInversedST(material, RightEyeTextureId, rect);
                    break;
            }
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

        void OnRenderObject()
        {
            if (!insideRendering)
            {
                ReleaseTemporalRenderTextures();
            }
        }

        void OnDestroy()
        {
            ReleaseTemporalRenderTextures();
        }

        void ReleaseTemporalRenderTextures()
        {
            if (preRenderCamera != null)
            {
                preRenderCamera.targetTexture = null;
            }

            if (leftRenderTexture != null)
            {
                RenderTexture.ReleaseTemporary(leftRenderTexture);
                leftRenderTexture = null;
            }

            if (rightRenderTexture != null)
            {
                RenderTexture.ReleaseTemporary(rightRenderTexture);
                rightRenderTexture = null;
            }

            isRenderPrepared = false;
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
    }
}
