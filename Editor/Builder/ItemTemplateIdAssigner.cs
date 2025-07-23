using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Builder
{
    public static class ItemTemplateIdAssigner
    {
        public static void Execute()
        {
            var scene = SceneManager.GetActiveScene();
            var itemTemplateContainers = ItemTemplateGatherer.GatherItemTemplateContainers(scene).ToArray();

            var itemTemplates = new Dictionary<IItem, ItemTemplateId>();
            var itemTemplateIds = new HashSet<ItemTemplateId>();
            foreach (var container in itemTemplateContainers)
            {
                foreach (var template in container.ItemTemplates())
                {
                    if (itemTemplates.TryGetValue(template.Item, out var id) && id.IsValid())
                    {
                        continue;
                    }

                    if (template.Id.IsValid() && itemTemplateIds.Add(template.Id))
                    {
                        itemTemplates[template.Item] = template.Id;
                    }
                    else
                    {
                        itemTemplates[template.Item] = default;
                    }
                }
            }

            foreach (var item in itemTemplates.Where(x => !x.Value.IsValid()).ToArray())
            {
                ItemTemplateId templateId;
                do
                {
                    templateId = ItemTemplateId.Create();
                } while (!templateId.IsValid() || !itemTemplateIds.Add(templateId));

                itemTemplates[item.Key] = templateId;
            }

            foreach (var container in itemTemplateContainers)
            {
                var objectChanged = false;

                foreach (var template in container.ItemTemplates())
                {
                    var id = itemTemplates[template.Item];
                    if (!template.Id.Equals(id))
                    {
                        container.SetItemTemplateId(template.Item, id);
                        objectChanged = true;
                    }
                }

                if (objectChanged && !Application.isPlaying)
                {
                    container.MarkObjectDirty();
                }
            }

            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(scene);
            }
        }
    }
}
