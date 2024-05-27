using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Preview.Common;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public sealed class SubSceneManager
    {
        readonly Dictionary<ISubScene, SubSceneLoadState> subSceneStatus = new();
        readonly Dictionary<ISubScene, ISubSceneSubstitutes[]> subSceneSubstitutes = new();

        public event Action<(string sceneName, bool isActive)> OnSubSceneActiveChanged;

        public SubSceneManager(IEnumerable<ISubScene> subScenes, IEnumerable<ISubSceneSubstitutes> subSceneSubstitutes)
        {
            foreach (var subScene in subScenes)
            {
                this.subSceneSubstitutes[subScene] = subSceneSubstitutes.Where(s => s.SubScene == subScene).ToArray();
                UpdateSubSceneLoadState(subScene, SubSceneLoadState.NotLoaded);

                subScene.OnStayAffectableAreaEvent += OnStayAffectableArea;
                subScene.OnLeaveAffectableAreaEvent += OnLeaveAffectableArea;
            }
        }

        void OnStayAffectableArea(OnStayAffectableAreaEventArgs e)
        {
            if (!e.StayObject.CompareTag("Player"))
            {
                return;
            }

            if (e.SubScene.UnityScene == null)
            {
                Debug.LogWarning(TranslationTable.cck_subscene_no_scene_set);
                return;
            }

            switch (subSceneStatus[e.SubScene])
            {
                case SubSceneLoadState.Loading:
                case SubSceneLoadState.Loaded:
                case SubSceneLoadState.Unloading:
                case SubSceneLoadState.Error:
                    break;
                case SubSceneLoadState.NotLoaded:
                    CoroutineGenerator.StartStaticCoroutine(LoadScene(e.SubScene));
                    break;
                case SubSceneLoadState.WaitingToUnload:
                    UpdateSubSceneLoadState(e.SubScene, SubSceneLoadState.Loading);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        IEnumerator LoadScene(ISubScene subScene)
        {
            var subSceneAssetPath = AssetDatabase.GetAssetPath(subScene.UnityScene);
            if (string.IsNullOrEmpty(subSceneAssetPath))
            {
                Debug.LogWarning(TranslationUtility.GetMessage(TranslationTable.cck_subscene_not_found_by_name, subScene.UnityScene.name));
                UpdateSubSceneLoadState(subScene, SubSceneLoadState.Error);
                yield break;
            }

            UpdateSubSceneLoadState(subScene, SubSceneLoadState.Loading);

            yield return EditorSceneManager.LoadSceneAsyncInPlayMode(
                subSceneAssetPath,
                new LoadSceneParameters(LoadSceneMode.Additive)
            );

            Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_load_scene, subScene.UnityScene.name));

            if (subSceneStatus[subScene] == SubSceneLoadState.WaitingToUnload)
            {
                yield return UnloadScene(subScene);
                yield break;
            }

            UpdateSubSceneLoadState(subScene, SubSceneLoadState.Loaded);
        }

        void OnLeaveAffectableArea(OnLeaveAffectableAreaEventArgs e)
        {
            if (!e.LeaveObject.CompareTag("Player"))
            {
                return;
            }

            if (e.SubScene.UnityScene == null)
            {
                Debug.LogWarning(TranslationTable.cck_subscene_no_scene_set);
                return;
            }

            switch (subSceneStatus[e.SubScene])
            {
                case SubSceneLoadState.NotLoaded:
                case SubSceneLoadState.Unloading:
                case SubSceneLoadState.WaitingToUnload:
                case SubSceneLoadState.Error:
                    break;
                case SubSceneLoadState.Loading:
                    UpdateSubSceneLoadState(e.SubScene, SubSceneLoadState.WaitingToUnload);
                    break;
                case SubSceneLoadState.Loaded:
                    CoroutineGenerator.StartStaticCoroutine(UnloadScene(e.SubScene));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        IEnumerator UnloadScene(ISubScene subScene)
        {
            UpdateSubSceneLoadState(subScene, SubSceneLoadState.Unloading);

            yield return SceneManager.UnloadSceneAsync(subScene.UnityScene.name);
            UpdateSubSceneLoadState(subScene, SubSceneLoadState.NotLoaded);
            Resources.UnloadUnusedAssets();
            Debug.Log(TranslationUtility.GetMessage(TranslationTable.cck_unload_scene, subScene.UnityScene.name));
        }

        void UpdateSubSceneLoadState(ISubScene subScene, SubSceneLoadState state)
        {
            var prevActive = subSceneStatus.TryGetValue(subScene, out var prevStatus) && IsSubSceneActive(prevStatus);
            subSceneStatus[subScene] = state;
            var isActive = IsSubSceneActive(state);
            SetSubSceneSubstitutesActive(subScene, !isActive);
            if (prevActive != isActive)
            {
                OnSubSceneActiveChanged?.Invoke((subScene.UnityScene.name, isActive));
            }
        }

        bool IsSubSceneActive(SubSceneLoadState state)
        {
            return state switch
            {
                SubSceneLoadState.Unloading or SubSceneLoadState.Loaded => true,
                _ => false,
            };
        }

        void SetSubSceneSubstitutesActive(ISubScene subScene, bool isActive)
        {
            foreach (var substitutes in subSceneSubstitutes[subScene])
            {
                if (substitutes != null)
                {
                    substitutes.SetActive(isActive);
                }
            }
        }

        enum SubSceneLoadState
        {
            NotLoaded,
            Loading,
            Loaded,
            Unloading,
            WaitingToUnload,
            Error,
        }
    }
}
