using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public sealed class CraftItemComponentValidator : IComponentValidator
    {
        static readonly string[] ShaderNameWhiteList =
        {
            "Standard",
            "Unlit/Texture",
            "ClusterVR/InternalSDK/MainScreen",
            "ClusterVR/UnlitNonTiledWithBackgroundColor",
            "ClusterCreatorKit/Mirror"
        };

        static readonly Vector3Int ItemSizeLimit = new Vector3Int(4, 4, 4);

        static readonly Vector3 BoundsCenterLimit = new Vector3(0, 2, 0);
        static readonly Vector3 BoundsSizeLimit = new Vector3(5, 5, 5);
        static readonly int MaxMirrorCount = 1;

        public IEnumerable<ValidationMessage> Validate(GameObject gameObject, bool isBeta)
        {
            var validationMessages = new List<ValidationMessage>();
            validationMessages.AddRange(ComponentValidator.ValidateItem(gameObject, ItemSizeLimit, false, true));
            validationMessages.AddRange(ComponentValidator.ValidateMovableItem(gameObject, isBeta));
            validationMessages.AddRange(ComponentValidator.ValidateScriptableItem(gameObject));
            validationMessages.AddRange(ComponentValidator.ValidateRenderers(gameObject));

            var requireComponentValidator = new RequireComponentValidator();
            foreach (var component in gameObject.GetComponentsInChildren<Component>(true))
            {
                var isRoot = component.gameObject == gameObject;
                validationMessages.AddRange(ComponentValidator.ValidateComponent(component, isRoot));
                requireComponentValidator.Validate(component);
            }
            validationMessages.AddRange(requireComponentValidator.GetMessage());

            validationMessages.AddRange(ComponentValidator.ValidateBounds(gameObject, BoundsCenterLimit, BoundsSizeLimit));
            validationMessages.AddRange(ComponentValidator.ValidateShader(gameObject, ShaderNameWhiteList, true));
            validationMessages.AddRange(ComponentValidator.ValidateItemAudioSetList(gameObject));
            validationMessages.AddRange(ComponentValidator.ValidateMirror(gameObject, MaxMirrorCount));
            validationMessages.AddRange(ComponentValidator.ValidateCollider(gameObject));

            return validationMessages;
        }
    }
}
