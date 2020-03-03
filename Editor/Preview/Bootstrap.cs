using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Preview.EditorUI;
using ClusterVR.CreatorKit.Editor.Preview.World;
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
        public static PlayerPresenter PlayerPresenter { get; private set; }
        public static SpawnPointManager SpawnPointManager { get; private set; }
        public static MainScreenPresenter MainScreenPresenter { get; private set; }
        public static CommentScreenPresenter CommentScreenPresenter { get; private set; }
        public static bool IsInPlayMode { get; private set; }

        static Bootstrap()
        {
            EditorApplication.playModeStateChanged += async playMode =>
            {
                await OnChangePlayModeAsync(playMode);
                OnChangePlayMode(playMode);
            };
        }

        static void OnChangePlayMode(PlayModeStateChange playMode)
        {
            switch (playMode)
            {
                case PlayModeStateChange.ExitingPlayMode:
                    SetIsInGameMode(false);
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    SetIsInGameMode(true);

                    var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

                    var spawnPoints = GetComponentsInGameObjectsChildren<ISpawnPoint>(rootGameObjects);
                    SpawnPointManager = new SpawnPointManager(spawnPoints);

                    // 疑似Playerの設定
                    var enterDeviceType = EnterDeviceType.Desktop;
                    if (XRSettings.enabled)
                    {
                        enterDeviceType = EnterDeviceType.VR;
                    }

                    var despawnHeight = GetComponentInGameObjectsChildren<IDespawnHeight>(rootGameObjects).Height;
                    PlayerPresenter = new PlayerPresenter(PermissionType.Audience, enterDeviceType);
                    new AvatarRespawner(despawnHeight, PlayerPresenter);

                    var mainScreenViews = GetComponentsInGameObjectsChildren<IMainScreenView>(rootGameObjects);
                    MainScreenPresenter = new MainScreenPresenter(mainScreenViews);

                    var rankingScreenViews = GetComponentsInGameObjectsChildren<IRankingScreenView>(rootGameObjects);
                    var rankingScreenPresenter = new RankingScreenPresenter(rankingScreenViews);
                    rankingScreenPresenter.SetRanking(11);

                    var commentScreenViews =
                        GetComponentsInGameObjectsChildren<ICommentScreenView>(rootGameObjects);
                    CommentScreenPresenter = new CommentScreenPresenter(commentScreenViews);
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

    }
}
