using System.Collections.Generic;
using System.Linq;
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
                        $"{rootGameObject.name}はActiveである必要があります",
                        ValidationMessage.MessageType.Error)
                };
            }

            return Enumerable.Empty<ValidationMessage>();
        }
    }
}
