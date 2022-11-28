using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    sealed class RequireComponentValidator
    {
        readonly Dictionary<GameObject, HashSet<Type>> missingComponentList = new();

        internal IEnumerable<ValidationMessage> GetMessage()
        {
            var validationMessages = new List<ValidationMessage>();

            foreach (var missingInfo in missingComponentList)
            {
                validationMessages.Add(new ValidationMessage(
                    $"{missingInfo.Key.name}に必要なコンポーネント{string.Join(", ", missingInfo.Value)}が設定されていません。",
                    ValidationMessage.MessageType.Error));
            }

            return validationMessages;
        }

        internal void Validate(Component component)
        {
            var requireComponentAttributes = Attribute.GetCustomAttributes(component.GetType(), typeof(RequireComponent))
                .Cast<RequireComponent>();
            foreach (var requireComponent in requireComponentAttributes)
            {
               ValidateRequireComponent(component, requireComponent.m_Type0);
               ValidateRequireComponent(component, requireComponent.m_Type1);
               ValidateRequireComponent(component, requireComponent.m_Type2);
            }
        }

        void ValidateRequireComponent(Component component, Type requireType)
        {
            if (requireType == null)
            {
                return;
            }

            var gameObject = component.gameObject;
            if (gameObject.TryGetComponent(requireType, out _))
            {
                return;
            }

            if (missingComponentList.TryGetValue(gameObject, out var componentList))
            {
                componentList.Add(requireType);
            }
            else
            {
                missingComponentList.Add(gameObject, new HashSet<Type>{requireType});
            }
        }
    }
}
