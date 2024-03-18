using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public static class ItemMaterialSetListValidator
    {
        public static IEnumerable<ValidationMessage> Validate(bool isBeta, GameObject itemRoot, IItemMaterialSetList itemMaterialSetList)
        {
            var messages = Validator.ItemMaterialSetListValidator.Validate(isBeta, itemRoot, itemMaterialSetList);
            return messages.Select(message => new ValidationMessage(message, ValidationMessage.MessageType.Error));
        }
    }
}
