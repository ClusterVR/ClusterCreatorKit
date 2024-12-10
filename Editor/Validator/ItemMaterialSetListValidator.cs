using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class ItemMaterialSetListValidator
    {
        public static IEnumerable<string> Validate(GameObject itemRoot, IItemMaterialSetList itemMaterialSetList)
        {
            var messages = new List<string>();

            var renderers = itemRoot.GetComponentsInChildren<Renderer>(true);
            var rendererMaterials = new HashSet<Material>(renderers.SelectMany(r => r.sharedMaterials));
            foreach (var set in itemMaterialSetList.ItemMaterialSets)
            {
                CheckId(messages, set);
                CheckMaterial(messages, set, rendererMaterials);
            }

            CheckDuplicated(messages, itemMaterialSetList);

            return messages;
        }

        static void CheckDuplicated(List<string> messages, IItemMaterialSetList itemMaterialSetList)
        {
            var collidedIds = GetDuplicatedIds(itemMaterialSetList);
            if (collidedIds.Count > 0)
            {
                messages.Add(TranslationUtility.GetMessage(TranslationTable.cck_item_material_set_list_duplicate_ids, string.Join(", ", collidedIds)));
            }

            var collidedMaterials = GetDuplicatedMaterials(itemMaterialSetList);
            if (collidedMaterials.Count > 0)
            {
                messages.Add(TranslationUtility.GetMessage(TranslationTable.cck_item_material_set_list_duplicate_materials, string.Join(", ", collidedMaterials.Select(m => m.name))));
            }
        }

        static HashSet<string> GetDuplicatedIds(IItemMaterialSetList itemMaterialSetList)
        {
            var checkedIds = new HashSet<string>();
            var collidedIds = new HashSet<string>();

            foreach (var set in itemMaterialSetList.ItemMaterialSets)
            {
                var id = set.Id;
                if (checkedIds.Contains(id))
                {
                    collidedIds.Add(id);
                }
                else
                {
                    checkedIds.Add(id);
                }
            }

            return collidedIds;
        }

        static HashSet<Material> GetDuplicatedMaterials(IItemMaterialSetList itemMaterialSetList)
        {
            var checkedMaterials = new HashSet<Material>();
            var collidedMaterials = new HashSet<Material>();

            foreach (var set in itemMaterialSetList.ItemMaterialSets)
            {
                var material = set.Material;
                if (checkedMaterials.Contains(material))
                {
                    collidedMaterials.Add(material);
                }
                else
                {
                    checkedMaterials.Add(material);
                }
            }

            return collidedMaterials;
        }

        static void CheckId(List<string> messages, ItemMaterialSet set)
        {
            var id = set.Id;
            if (string.IsNullOrEmpty(id))
            {
                messages.Add(TranslationTable.cck_item_material_set_list_empty_id);
                return;
            }

            if (!Constants.Component.ValidIdCharactersRegex.IsMatch(id))
            {
                messages.Add(IdValidateErrorMessages.RegexErrorMessage("ItemMaterialSetList", id));
            }

            if (id.Length > Constants.Component.MaxIdLength)
            {
                messages.Add(IdValidateErrorMessages.LengthErrorMessage("ItemMaterialSetList", id));
            }
        }

        static void CheckMaterial(List<string> messages, ItemMaterialSet set, HashSet<Material> rendererMaterials)
        {
            var id = set.Id;
            var material = set.Material;

            if (material == null)
            {
                messages.Add(TranslationTable.cck_item_material_set_material_not_found);
                return;
            }

            if (!rendererMaterials.Contains(material))
            {
                messages.Add(TranslationUtility.GetMessage(TranslationTable.cck_material_not_assigned_to_renderer, id));
            }
        }
    }
}
