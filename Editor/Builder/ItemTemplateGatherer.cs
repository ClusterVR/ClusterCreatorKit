using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class ItemTemplateGatherer
    {
        public static IEnumerable<IGrouping<IItem, CreateItemGimmick>> GatherCreateItemGimmicksForItemTemplates(
            Scene scene)
        {
            return GatherValidCreateItemGimmicks(scene)
                .GroupBy(x => x.ItemTemplate);
        }

        public static IEnumerable<IItem> GatherItemTemplates(Scene scene)
        {
            return GatherValidCreateItemGimmicks(scene)
                .Select(x => x.ItemTemplate)
                .Distinct();
        }

        static IEnumerable<CreateItemGimmick> GatherValidCreateItemGimmicks(Scene scene)
        {
            var createItemGimmicks = new HashSet<CreateItemGimmick>();

            void AddCreateItemGimmick(CreateItemGimmick createItemGimmick)
            {
                if (!createItemGimmick.IsValid())
                {
                    return;
                }
                if (!createItemGimmicks.Add(createItemGimmick))
                {
                    return;
                }
                foreach (var innerCreateItemGimmick in createItemGimmick.ItemTemplate.gameObject
                    .GetComponents<CreateItemGimmick>())
                {
                    AddCreateItemGimmick(innerCreateItemGimmick);
                }
            }

            foreach (var createItemGimmickItem in scene.GetRootGameObjects()
                .SelectMany(o => o.GetComponentsInChildren<CreateItemGimmick>(true)))
            {
                AddCreateItemGimmick(createItemGimmickItem);
            }

            return createItemGimmicks;
        }
    }
}
