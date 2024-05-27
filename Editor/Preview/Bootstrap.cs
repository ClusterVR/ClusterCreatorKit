using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Common;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.Preview.Common;
using ClusterVR.CreatorKit.Editor.Preview.EditorSettings;
using ClusterVR.CreatorKit.Editor.Preview.EditorUI;
using ClusterVR.CreatorKit.Editor.Preview.Gimmick;
using ClusterVR.CreatorKit.Editor.Preview.Item;
using ClusterVR.CreatorKit.Editor.Preview.Operation;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using ClusterVR.CreatorKit.Editor.Preview.Trigger;
using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Operation;
using ClusterVR.CreatorKit.Preview.Common;
using ClusterVR.CreatorKit.Preview.PlayerController;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.DespawnHeights;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace ClusterVR.CreatorKit.Editor.Preview
{
    [InitializeOnLoad]
    public static class Bootstrap
    {
        public static ItemCreator ItemCreator { get; private set; }
        public static ItemDestroyer ItemDestroyer { get; private set; }
        public static PlayerPresenter PlayerPresenter { get; private set; }
        public static SpawnPointManager SpawnPointManager { get; private set; }
        public static MainScreenPresenter MainScreenPresenter { get; private set; }
        public static CommentScreenPresenter CommentScreenPresenter { get; private set; }

        public static RoomStateRepository RoomStateRepository { get; private set; }
        public static GimmickManager GimmickManager { get; private set; }
        public static SignalGenerator SignalGenerator { get; private set; }
        public static PersistedRoomStateManager PersistedRoomStateManager { get; private set; }
        public static bool IsInPlayMode { get; private set; }
        public static event OnInitializeEventHandler OnInitializedEvent;

        public delegate void OnInitializeEventHandler();

        static Bootstrap()
        {
#if !CLUSTER_CREATOR_KIT_DISABLE_PREVIEW
            CancellationTokenSource playModeStateChangedCancellationTokenSource = null;
            EditorApplication.playModeStateChanged += async playMode =>
            {
                playModeStateChangedCancellationTokenSource?.Cancel();
                playModeStateChangedCancellationTokenSource?.Dispose();
                playModeStateChangedCancellationTokenSource = new();
                await OnChangePlayModeAsync(playMode, playModeStateChangedCancellationTokenSource.Token);
            };
            SetupProject();
#endif
        }

        static void SetupProject()
        {
            ProjectSettingsConfigurer.Setup();
        }

        static async Task OnChangePlayModeAsync(PlayModeStateChange playMode, CancellationToken cancellationToken)
        {
            switch (playMode)
            {
                case PlayModeStateChange.ExitingPlayMode:
                    SetIsInGameMode(false);
                    SaveRoomState();
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    await PackageListRepository.UpdatePackageList(cancellationToken);
                    XRSettings.enabled = SwitchUseVR.EnabledVR();
                    var coroutine = CoroutineGenerator.StartStaticCoroutine(InitializeAsync());
                    cancellationToken.Register(() => CoroutineGenerator.StopStaticCoroutine(coroutine));
                    break;
            }
        }

        static IEnumerator InitializeAsync()
        {
            var activeScene = SceneManager.GetActiveScene();
            var rootGameObjects = activeScene.GetRootGameObjects();

            if (!IsPreviewable(rootGameObjects, out var message))
            {
                Debug.LogError(message);
                yield break;
            }

            var otherScenes = Enumerable.Range(0, SceneManager.sceneCount)
                .Select(SceneManager.GetSceneAt)
                .Where(scene => scene != activeScene)
                .ToArray();

            foreach (var scene in otherScenes)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }

            ItemIdAssigner.AssignItemId();
            ItemTemplateIdAssigner.Execute();
            LayerCollisionConfigurer.SetupLayerCollision();
            SubSceneNameAssigner.Execute();

            var spawnPoints = GetComponentsInGameObjectsChildren<ISpawnPoint>(rootGameObjects);
            SpawnPointManager = new SpawnPointManager(spawnPoints);

            var urlTextures = GetComponentsInGameObjectsChildren<IUrlTexture>(rootGameObjects);
            UrlTextureDownloader.UrlTextureDownload(urlTextures);

            var localizedAssets = GetComponentsInGameObjectsChildren<ILocalizedAsset>(rootGameObjects);
            ServerLangCodeManager.InitializeLocalizedAssets(localizedAssets);

            var enterDeviceType = EnterDeviceType.Desktop;
            if (XRSettings.enabled)
            {
                enterDeviceType = EnterDeviceType.VR;
            }

            var despawnHeight = GetComponentInGameObjectsChildren<IDespawnHeight>(rootGameObjects).Height;
            PlayerPresenter = new PlayerPresenter(PermissionType.Audience, enterDeviceType, SpawnPointManager);
            new AvatarRespawner(despawnHeight, PlayerPresenter);

            var warpPortals = GetComponentsInGameObjectsChildren<IWarpPortal>(rootGameObjects);
            new WarpPortalExecutor(PlayerPresenter, warpPortals);

            ItemCreator = new ItemCreator(GetComponentsInGameObjectsChildren<ICreateItemGimmick>(rootGameObjects));
            ItemDestroyer = new ItemDestroyer(PlayerPresenter.PlayerTransform.GetComponent<IItemController>());
            new ItemRespawner(despawnHeight, ItemCreator, ItemDestroyer,
                GetComponentsInGameObjectsChildren<IMovableItem>(rootGameObjects));

            var mainScreenViews = GetComponentsInGameObjectsChildren<IMainScreenView>(rootGameObjects);
            MainScreenPresenter = new MainScreenPresenter(mainScreenViews);

            var rankingScreenViews = GetComponentsInGameObjectsChildren<IRankingScreenView>(rootGameObjects);
            var rankingScreenPresenter = new RankingScreenPresenter(rankingScreenViews);
            rankingScreenPresenter.SetRanking(11);

            var commentScreenViews = GetComponentsInGameObjectsChildren<ICommentScreenView>(rootGameObjects);
            CommentScreenPresenter = new CommentScreenPresenter(commentScreenViews);

            var worldGates = GetComponentsInGameObjectsChildren<IWorldGate>(rootGameObjects);
            new WorldGateExecutor(worldGates);

            var subScenes = GetComponentsInGameObjectsChildren<ISubScene>(rootGameObjects);
            var subSceneSubstitutes = GetComponentsInGameObjectsChildren<ISubSceneSubstitutes>(rootGameObjects);
            var subSceneManager = new SubSceneManager(subScenes, subSceneSubstitutes);

            var timeProvider = new TimeProvider();
            var timeProviderRequesters = GetComponentsInGameObjectsChildren<ITimeProviderRequester>(rootGameObjects);
            new TimeProviderAssigner(ItemCreator, timeProvider, subSceneManager, timeProviderRequesters);

            SetupTriggerGimmicks(rootGameObjects, ItemCreator, ItemDestroyer, timeProvider, subSceneManager);

            WorldRuntimeSettingValidator.ShowWarningIfPreviewUnsupportedSettingDetected(activeScene);

            SetIsInGameMode(true);
            OnInitializedEvent?.Invoke();
        }

        static bool IsPreviewable(GameObject[] rootGameObjects, out string message)
        {
            var hasSpawnPoint = GetComponentsInGameObjectsChildren<ISpawnPoint>(rootGameObjects).Any(x => x.SpawnType == SpawnType.Entrance);
            var hasDespawnHeight = GetComponentsInGameObjectsChildren<IDespawnHeight>(rootGameObjects).Any();

            if (hasSpawnPoint && hasDespawnHeight)
            {
                message = default;
                return true;
            }

            var requiredComponentMessages = new List<string>();
            if (!hasSpawnPoint)
            {
                requiredComponentMessages.Add(TranslationUtility.GetMessage(TranslationTable.cck_spawnpoint_entrance_type, nameof(ISpawnPoint.SpawnType), SpawnType.Entrance, nameof(SpawnPoint)));
            }

            if (!hasDespawnHeight)
            {
                requiredComponentMessages.Add($"{nameof(DespawnHeight)}");
            }

            message = TranslationUtility.GetMessage(TranslationTable.cck_required_components_for_preview, string.Join(", ", requiredComponentMessages));
            return false;
        }

        static void SetIsInGameMode(bool value)
        {
            IsInPlayMode = value;
        }

        static T GetComponentInGameObjectsChildren<T>(IEnumerable<GameObject> rootGameObjects)
        {
            return rootGameObjects.Select(x =>
                    x.GetComponentInChildren<T>(true))
                .First(x => x != null);
        }

        static IEnumerable<T> GetComponentsInGameObjectsChildren<T>(IEnumerable<GameObject> rootGameObjects)
        {
            return rootGameObjects.SelectMany(x =>
                x.GetComponentsInChildren<T>(true));
        }

        static void SetupTriggerGimmicks(IEnumerable<GameObject> rootGameObjects, ItemCreator itemCreator,
            ItemDestroyer itemDestroyer, ITimeProvider timeProvider, SubSceneManager subSceneManager)
        {
            RoomStateRepository = new RoomStateRepository();
            GimmickManager = new GimmickManager(RoomStateRepository, itemCreator, itemDestroyer, subSceneManager);
            SignalGenerator = new SignalGenerator(timeProvider);
            var triggerManager = new TriggerManager(RoomStateRepository, itemCreator, GimmickManager, SignalGenerator);
            var items = GetComponentsInGameObjectsChildren<IItem>(rootGameObjects).ToArray();
            triggerManager.Add(items.SelectMany(x => x.gameObject.GetComponents<IItemTrigger>()));
            triggerManager.Add(GetComponentsInGameObjectsChildren<IPlayerTrigger>(rootGameObjects));
            triggerManager.Add(GetComponentsInGameObjectsChildren<IGlobalTrigger>(rootGameObjects));
            GimmickManager.AddGimmicksInScene(GetComponentsInGameObjectsChildren<IGimmick>(rootGameObjects));
            foreach (var item in items)
            {
                GimmickManager.AddGimmicksInItem(item.gameObject.GetComponentsInChildren<IGimmick>(true),
                    item.Id.Value);
            }

            new LogicManager(itemCreator, RoomStateRepository, GimmickManager,
                GetComponentsInGameObjectsChildren<IItemLogic>(rootGameObjects),
                GetComponentsInGameObjectsChildren<IPlayerLogic>(rootGameObjects),
                GetComponentsInGameObjectsChildren<IGlobalLogic>(rootGameObjects),
                SignalGenerator);
            new PlayerEffectManager(PlayerPresenter, itemCreator,
                GetComponentsInGameObjectsChildren<IPlayerEffectGimmick>(rootGameObjects));
            new CreateItemGimmickManager(itemCreator,
                GetComponentsInGameObjectsChildren<ICreateItemGimmick>(rootGameObjects));
            new DestroyItemGimmickManager(itemCreator, itemDestroyer,
                GetComponentsInGameObjectsChildren<IDestroyItemGimmick>(rootGameObjects));

            var ridableItemManager = new RidableItemManager(itemCreator, itemDestroyer, PlayerPresenter,
                GetComponentsInGameObjectsChildren<IRidableItem>(rootGameObjects));
            new SteerItemTriggerEmitter(ridableItemManager, PlayerPresenter, PlayerPresenter.MoveInputController);
            new ProductDisplayItemManager(itemCreator,
                GetComponentsInGameObjectsChildren<IProductDisplayItem>(rootGameObjects));

            var onReceiveOwnershipItemTriggerManager = new OnReceiveOwnershipItemTriggerManager(itemCreator);
            var onCreateItemTriggerManager = new OnCreateItemTriggerManager(itemCreator);
            var initialPlayerTriggerManager = new InitialPlayerTriggerManager();

            PersistedRoomStateManager = PersistedRoomStateManager.CreateFromActiveScene();

            LoadPersistedRoomState();
            onCreateItemTriggerManager.Invoke(items.SelectMany(x =>
                x.gameObject.GetComponents<IOnCreateItemTrigger>()));
            initialPlayerTriggerManager.Invoke(
                GetComponentsInGameObjectsChildren<IInitializePlayerTrigger>(rootGameObjects),
                GetComponentsInGameObjectsChildren<IOnJoinPlayerTrigger>(rootGameObjects));
            onReceiveOwnershipItemTriggerManager.InvokeOnStart(items.SelectMany(x =>
                x.gameObject.GetComponents<IOnReceiveOwnershipItemTrigger>()));
        }

        static void LoadPersistedRoomState()
        {
            if (PersistedRoomStateManager == null)
            {
                return;
            }
            var updatedKeys = PersistedRoomStateManager.Load(RoomStateRepository);
            GimmickManager.OnStateUpdated(updatedKeys);
        }

        static void SaveRoomState()
        {
            PersistedRoomStateManager?.Save(RoomStateRepository);
        }
    }
}
