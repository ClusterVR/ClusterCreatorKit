using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class ItemMaterialSetListValidator
    {
        public static IEnumerable<string> Validate(bool isBeta, GameObject itemRoot, IItemMaterialSetList itemMaterialSetList)
        {
            var messages = new List<string>();

            if (!isBeta)
            {
                return new[] { "ItemMaterialSetListはベータ機能です" };
            }

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
                messages.Add($"ItemMaterialSetListのIdが重複しています。 Id: {string.Join(", ", collidedIds)}");
            }

            var collidedMaterials = GetDuplicatedMaterials(itemMaterialSetList);
            if (collidedMaterials.Count > 0)
            {
                messages.Add($"ItemMaterialSetListのMaterialが重複しています。 Material: {string.Join(", ", collidedMaterials.Select(m => m.name))}");
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
                messages.Add($"ItemMaterialSetListのIdが空です。有効なIdを設定してください。");
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
                messages.Add($"Materialがみつかりませんでした。ItemMaterialSetにMaterialを設定してください。");
                return;
            }

            if (!rendererMaterials.Contains(material))
            {
                messages.Add($"MaterialはこのItem及び子孫要素のRendererに割り当てられている必要があります。 (Id: {id})");
            }
        }
    }
}
