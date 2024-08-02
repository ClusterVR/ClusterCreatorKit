using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class ItemTemplateGatherer
    {
        public static IEnumerable<IItem> GatherItemTemplates(Scene scene)
        {
            return GatherItemTemplateContainers(scene)
                .SelectMany(x => x.ItemTemplates())
                .Select(x => x.Item)
                .Distinct();
        }

        public static IEnumerable<IItemTemplateContainer> GatherItemTemplateContainers(Scene scene)
        {
            var itemTemplateContainers = new HashSet<IItemTemplateContainer>();

            foreach (var itemTemplateContainer in scene.GetRootGameObjects()
                         .SelectMany(o => o.GetComponentsInChildren<IItemTemplateContainer>(true)))
            {
                AddItemTemplateContainer(itemTemplateContainer);
            }

            return itemTemplateContainers;

            void AddItemTemplateContainer(IItemTemplateContainer itemTemplateContainer)
            {
                if (!itemTemplateContainers.Add(itemTemplateContainer))
                {
                    return;
                }
                foreach (var innerItemTemplateContainer in itemTemplateContainer.ItemTemplates()
                             .SelectMany(i => i.Item.gameObject.GetComponents<IItemTemplateContainer>()))
                {
                    AddItemTemplateContainer(innerItemTemplateContainer);
                }
            }
        }
    }
}
