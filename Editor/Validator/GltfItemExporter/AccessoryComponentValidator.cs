using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public sealed class AccessoryComponentValidator : IComponentValidator
    {
        static readonly string[] ShaderNameWhiteList =
        {
            "VRM/MToon"
        };

        static readonly Vector3Int ItemSizeLimit = new Vector3Int(2, 2, 2);

        static readonly Vector3 BoundsCenterLimit = new Vector3(0, 0, 0);
        static readonly Vector3 BoundsSizeLimit = ItemSizeLimit;

        static readonly Vector3 OffsetPositionLimit = new Vector3(2, 2, 2);

        public IEnumerable<ValidationMessage> Validate(GameObject gameObject)
        {
            var validationMessages = new List<ValidationMessage>();

            validationMessages.AddRange(ComponentValidator.ValidateTransform(gameObject));
            validationMessages.AddRange(ComponentValidator.ValidateItem(gameObject, ItemSizeLimit, true, false));
            validationMessages.AddRange(ComponentValidator.ValidateAttachableItem(gameObject, OffsetPositionLimit));
            validationMessages.AddRange(ComponentValidator.ValidateRenderers(gameObject));

            var requireComponentValidator = new RequireComponentValidator();
            foreach (var component in gameObject.GetComponentsInChildren<Component>(true))
            {
                var isRoot = component.gameObject == gameObject;
                validationMessages.AddRange(ComponentValidator.ValidateAccessoryComponent(component, isRoot));
                requireComponentValidator.Validate(component);
            }
            validationMessages.AddRange(requireComponentValidator.GetMessage());

            validationMessages.AddRange(ComponentValidator.ValidateBounds(gameObject, BoundsCenterLimit, BoundsSizeLimit)); // boundは2m以内
            validationMessages.AddRange(ComponentValidator.ValidateShader(gameObject, ShaderNameWhiteList, false)); // mtoonシェーダのみを許可

            return validationMessages;
        }
    }
}
