using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Common;
using ClusterVR.CreatorKit.Editor.Preview.World;
using ClusterVR.CreatorKit.Item;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Preview.Item
{
    public sealed class TimeProviderAssigner
    {
        readonly ITimeProvider timeProvider;

        public TimeProviderAssigner(ItemCreator itemCreator, ITimeProvider timeProvider, SubSceneManager subSceneManager, IEnumerable<ITimeProviderRequester> timeProviderRequesters)
        {
            this.timeProvider = timeProvider;
            itemCreator.OnCreate += OnCreateItem;
            subSceneManager.OnSubSceneActiveChanged += ev => OnSubSceneActiveChanged(ev.sceneName, ev.isActive);

            foreach (var timeProviderRequester in timeProviderRequesters)
            {
                timeProviderRequester.SetTimeProvider(timeProvider);
            }
        }

        void OnSubSceneActiveChanged(string sceneName, bool isActive)
        {
            if (isActive)
            {
                var sceneRootObjects = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
                foreach (var timeProviderRequester in sceneRootObjects.SelectMany(g => g.GetComponentsInChildren<ITimeProviderRequester>(true)))
                {
                    timeProviderRequester.SetTimeProvider(timeProvider);
                }
            }
        }

        void OnCreateItem(IItem item)
        {
            foreach (var timeProviderRequester in item.gameObject.GetComponentsInChildren<ITimeProviderRequester>(true))
            {
                timeProviderRequester.SetTimeProvider(timeProvider);
            }
        }
    }
}
