using System.Collections.Generic;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Item;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    public static class ItemTemplateIdAssigner
    {
        public static void Execute()
        {
            var templateIdItems = new Dictionary<ItemTemplateId, IItem>();

            ItemTemplateId GetOrCreateTemplateId(IItem item, IEnumerable<CreateItemGimmick> createItemGimmicks)
            {
                ItemTemplateId templateId;
                foreach (var createItemGimmick in createItemGimmicks)
                {
                    templateId = createItemGimmick.ItemTemplateId;
                    if (templateId.Value == 0 || templateIdItems.ContainsKey(templateId)) continue;
                    templateIdItems.Add(templateId, item);
                    return templateId;
                }

                do templateId = ItemTemplateId.Create();
                while (templateId.Value == 0 || templateIdItems.ContainsKey(templateId));
                templateIdItems.Add(templateId, item);
                return templateId;
            }

            var scene = SceneManager.GetActiveScene();
            var createItemGimmickGroup = ItemTemplateGatherer.GatherCreateItemGimmicksForItemTemplates(scene);

            foreach (var gimmicks in createItemGimmickGroup)
            {
                var item = gimmicks.Key;
                var templateId = GetOrCreateTemplateId(item, gimmicks);
                foreach (var gimmick in gimmicks)
                {
                    if (gimmick.ItemTemplateId.Equals(templateId)) continue;
                    gimmick.ItemTemplateId = templateId;
                    if (!Application.isPlaying) EditorUtility.SetDirty(gimmick);
                }
            }

            if (!Application.isPlaying) EditorSceneManager.MarkSceneDirty(scene);
        }
    }
}
