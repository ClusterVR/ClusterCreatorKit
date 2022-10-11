using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Builder;
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
using ClusterVR.CreatorKit.Preview.PlayerController;
using ClusterVR.CreatorKit.Trigger;
using ClusterVR.CreatorKit.World;
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
            EditorApplication.playModeStateChanged += async playMode =>
            {
                await OnChangePlayModeAsync(playMode);
                OnChangePlayMode(playMode);
            };
            SetupProject();
#endif
        }

        static void SetupProject()
        {
            ProjectSettingsConfigurer.Setup();
        }

        static void OnChangePlayMode(PlayModeStateChange playMode)
        {
            switch (playMode)
            {
                case PlayModeStateChange.ExitingPlayMode:
                    SetIsInGameMode(false);
                    SaveRoomState();
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    SetIsInGameMode(true);

                    ItemIdAssigner.AssignItemId();
                    ItemTemplateIdAssigner.Execute();
                    LayerCollisionConfigurer.SetupLayerCollision();

                    var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

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

                    ItemCreator =
                        new ItemCreator(GetComponentsInGameObjectsChildren<ICreateItemGimmick>(rootGameObjects));
                    ItemDestroyer =
                        new ItemDestroyer(PlayerPresenter.PlayerTransform.GetComponent<IItemController>());
                    new ItemRespawner(despawnHeight, ItemCreator, ItemDestroyer,
                        GetComponentsInGameObjectsChildren<IMovableItem>(rootGameObjects));

                    var mainScreenViews = GetComponentsInGameObjectsChildren<IMainScreenView>(rootGameObjects);
                    MainScreenPresenter = new MainScreenPresenter(mainScreenViews);

                    var rankingScreenViews = GetComponentsInGameObjectsChildren<IRankingScreenView>(rootGameObjects);
                    var rankingScreenPresenter = new RankingScreenPresenter(rankingScreenViews);
                    rankingScreenPresenter.SetRanking(11);

                    var commentScreenViews =
                        GetComponentsInGameObjectsChildren<ICommentScreenView>(rootGameObjects);
                    CommentScreenPresenter = new CommentScreenPresenter(commentScreenViews);

                    var worldGates = GetComponentsInGameObjectsChildren<IWorldGate>(rootGameObjects);
                    new WorldGateExecutor(worldGates);

                    SetupTriggerGimmicks(rootGameObjects, ItemCreator, ItemDestroyer);

                    OnInitializedEvent?.Invoke();
                    break;
            }
        }

        static async Task OnChangePlayModeAsync(PlayModeStateChange playMode)
        {
            if (playMode != PlayModeStateChange.EnteredPlayMode)
            {
                return;
            }

            await PackageListRepository.UpdatePackageList();

            XRSettings.enabled = SwitchUseVR.EnabledVR();
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
            ItemDestroyer itemDestroyer)
        {
            RoomStateRepository = new RoomStateRepository();
            GimmickManager = new GimmickManager(RoomStateRepository, itemCreator, itemDestroyer);
            SignalGenerator = new SignalGenerator();
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
