using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Preview.PlayerController;
using UnityEditor;
using UnityEngine;

#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine.Rendering.PostProcessing;
#endif

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public sealed class PlayerPresenter
    {
        const string PreviewOnlyPrefabPath =
            "Packages/mu.cluster.cluster-creator-kit/Editor/Preview/Prefabs/PreviewOnly.prefab";

        readonly IPlayerController playerController;
        readonly SpawnPointManager spawnPointManager;

        Vector3? recordedPosition;
        Quaternion? recordedRotation;

        bool isInPersonalCamera;

        public Transform PlayerTransform { get; }
        public Quaternion RootRotation => playerController.RootRotation;
        public Transform CameraTransform { get; }
        public IMoveInputController MoveInputController { get; }

        public PermissionType PermissionType { get; private set; }

        public PlayerPresenter(PermissionType permissionType,
            SpawnPointManager spawnPointManager)
        {
            PermissionType = permissionType;
            this.spawnPointManager = spawnPointManager;

            var previewOnlyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PreviewOnlyPrefabPath);
            var previewOnly = PrefabUtility.InstantiatePrefab(previewOnlyPrefab) as GameObject;
            playerController = previewOnly.GetComponentInChildren<IPlayerController>();
            PlayerTransform = playerController.PlayerTransform;
            CameraTransform = playerController.CameraTransform;
            MoveInputController = previewOnly.GetComponentInChildren<IMoveInputController>();

#if UNITY_POST_PROCESSING_STACK_V2
            var postProcessLayer = CameraTransform.gameObject.GetComponent<PostProcessLayer>() ??
                CameraTransform.gameObject.AddComponent<PostProcessLayer>();

            postProcessLayer.volumeTrigger = CameraTransform;
            postProcessLayer.volumeLayer = 1 << LayerName.PostProcessing;
#endif
            Respawn();
        }

        public void Respawn()
        {
            var spawnPoint = spawnPointManager.GetRespawnPoint(PermissionType);
            WarpTo(spawnPoint.Position);
            RotateTo(Quaternion.Euler(0f, spawnPoint.YRotation, 0f));
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
                    PlayerTransform.gameObject.layer = LayerName.Performer;
                    break;
                case PermissionType.Audience:
                    PlayerTransform.gameObject.layer = LayerName.Audience;
                    break;
            }
        }

        public void WarpTo(Vector3 position)
        {
            playerController.WarpTo(position);
        }

        public void RotateTo(Quaternion rotation)
        {
            var yawOnlyRotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);
            playerController.SetRotationKeepingHeadPitch(yawOnlyRotation);
            playerController.ResetCameraRotation(rotation);
        }

        public void SetMoveSpeedRate(float moveSpeedRate)
        {
            playerController.SetMoveSpeedRate(moveSpeedRate);
        }

        public void SetJumpSpeedRate(float jumpSpeedRate)
        {
            playerController.SetJumpSpeedRate(jumpSpeedRate);
        }

        public void SetRidingItem(IRidableItem ridingItem)
        {
            playerController.SetRidingItem(ridingItem);
        }
    }

    public enum PermissionType
    {
        Performer,
        Audience
    }
}
