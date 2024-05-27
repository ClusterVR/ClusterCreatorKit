using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public static class GameObjectValidator
    {
        public static IEnumerable<ValidationMessage> Validate(GameObject rootGameObject)
        {
            if (!rootGameObject.activeSelf)
            {
                return new[]
                {
                    new ValidationMessage(
                        TranslationUtility.GetMessage(TranslationTable.cck_rootgameobject_active_required, rootGameObject.name),
                        ValidationMessage.MessageType.Error)
                };
            }

            return Enumerable.Empty<ValidationMessage>();
        }
    }
}
