﻿using System;
using ClusterVR.CreatorKit.Preview.PlayerController;
using UnityEditor;
using UnityEngine;

#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine.Rendering.PostProcessing;
#endif

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public class PlayerPresenter
    {
        const string NonVRPrefabPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Preview/Prefabs/PreviewOnly.prefab";
        const string VRPrefabPath = "Packages/mu.cluster.cluster-creator-kit/Editor/Preview/Prefabs/VRPlayerController.prefab";

        readonly IPlayerController playerController;
        readonly EnterDeviceType enterDeviceType;

        Vector3? recordedPosition;
        Quaternion? recordedRotation;

        bool isInPersonalCamera;

        public Transform PlayerTransform { get; }
        public Transform CameraTransform { get; }

        public PermissionType PermissionType { get; private set; }

        public PlayerPresenter(PermissionType permissionType, EnterDeviceType enterDeviceType)
        {
            PermissionType = permissionType;
            this.enterDeviceType = enterDeviceType;

            var previewOnlyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PreviewOnlyPrefabPath());
            var previewOnly = PrefabUtility.InstantiatePrefab(previewOnlyPrefab) as GameObject;
            playerController = previewOnly.GetComponentInChildren<IPlayerController>();
            PlayerTransform = playerController.PlayerTransform;
            CameraTransform = playerController.CameraTransform;

#if UNITY_POST_PROCESSING_STACK_V2
            var postProcessLayer = CameraTransform.gameObject.GetComponent<PostProcessLayer>() ?? CameraTransform.gameObject.AddComponent<PostProcessLayer>();

            postProcessLayer.volumeTrigger = CameraTransform;
            postProcessLayer.volumeLayer = 1 << LayerMask.NameToLayer("PostProcessing");
#endif

            // Permissionに応じた初期位置にスポーンする
            Bootstrap.SpawnPointManager.Respawn(permissionType, PlayerTransform, CameraTransform);
        }

        string PreviewOnlyPrefabPath()
        {
            switch (enterDeviceType)
            {
                case EnterDeviceType.Desktop:
                    return NonVRPrefabPath;
                case EnterDeviceType.VR:
                    return VRPrefabPath;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enterDeviceType), enterDeviceType, null);
            }
        }

        public void ChangePermissionType(PermissionType permissionType)
        {
            PermissionType = permissionType;
            ChangeLayer(permissionType);
        }

        void ChangeLayer(PermissionType permissionType)
        {
            switch (permissionType)
            {
                case PermissionType.Performer:
                    PlayerTransform.gameObject.layer = LayerMask.NameToLayer("Performer");
                    break;
                case PermissionType.Audience:
                    PlayerTransform.gameObject.layer = LayerMask.NameToLayer("Audience");
                    break;
            }
        }

        public void SetPointOfView(Transform targetPoint)
        {
            isInPersonalCamera = true;
            // 位置を記録した後PersonalCameraの位置に移動、CharacterControllerを無効化する
            if (enterDeviceType == EnterDeviceType.VR)
            {
                recordedPosition = PlayerTransform.position;
                recordedRotation = PlayerTransform.rotation;
                // OpenVRのカメラの高さがそのまま視点の高さになるので、 見せたい視点の高さ(y) - OpenVRによるカメラの高さ(local y)をしてやることで、視点の視線の高さにする
                var targetPosition = new Vector3(targetPoint.position.x, targetPoint.position.y - CameraTransform.localPosition.y, targetPoint.position.z);
                PlayerTransform.SetPositionAndRotation(targetPosition, targetPoint.rotation);
            }
            else
            {
                recordedPosition = CameraTransform.position;
                recordedRotation = CameraTransform.rotation;
                CameraTransform.SetPositionAndRotation(targetPoint.position, targetPoint.rotation);
            }

            playerController.ActivateCharacterController(false);
        }

        public void ResetPointOfView()
        {
            if (!isInPersonalCamera)
            {
                return;
            }

            // SetPointOfViewの時に記録した位置に戻す、CharacterControllerを再度有効化
            if (!recordedPosition.HasValue || !recordedRotation.HasValue)
            {
                return;
            }

            if (enterDeviceType == EnterDeviceType.VR)
            {
                PlayerTransform.SetPositionAndRotation(recordedPosition.Value, recordedRotation.Value);
            }
            else
            {
                CameraTransform.SetPositionAndRotation(recordedPosition.Value, recordedRotation.Value);
            }

            playerController.ActivateCharacterController(true);
            isInPersonalCamera = false;
        }
    }

    public enum PermissionType
    {
        Performer,
        Audience
    }

    public enum EnterDeviceType
    {
        Desktop,
        VR
    }
}
