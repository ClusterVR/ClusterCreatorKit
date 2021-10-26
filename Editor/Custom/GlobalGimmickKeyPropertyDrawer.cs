using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Validator;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(GlobalGimmickKey), true)]
    public sealed class GlobalGimmickKeyPropertyDrawer : PropertyDrawer
    {
        static readonly List<GimmickTarget> LocalizableSelectables = new List<GimmickTarget>
        {
            GimmickTarget.Global, GimmickTarget.Item, GimmickTarget.Player
        };

        static readonly List<GimmickTarget> ConsistentlySyncSelectables =
            new List<GimmickTarget> { GimmickTarget.Global, GimmickTarget.Item };

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var keyProperty = property.FindPropertyRelative("key");
            var targetProperty = keyProperty.FindPropertyRelative("target");

            var itemContainer = new PropertyField(property.FindPropertyRelative("item"));

            var component = (Component) property.serializedObject.targetObject;
            var scene = component.gameObject.scene;
            var isInScene = scene.name != null && !EditorSceneManager.IsPreviewScene(scene);
            var messageType = isInScene ? MessageType.Error : MessageType.Warning;
            var localPlayerHelpBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox(LocalPlayerGimmickValidation.ErrorMessage, messageType));

            void SwitchDisplayItem(GimmickTarget target)
            {
                itemContainer.SetVisibility(target == GimmickTarget.Item);
                localPlayerHelpBox.SetVisibility(!LocalPlayerGimmickValidation.IsValid(target, component));
            }

            SwitchDisplayItem((GimmickTarget) targetProperty.enumValueIndex);

            var targetField = EnumField.Create(targetProperty.displayName, targetProperty,
                LocalPlayerGimmickValidation.IsLocalizable(component)
                    ? LocalizableSelectables
                    : ConsistentlySyncSelectables, (GimmickTarget) targetProperty.enumValueIndex, FormatTarget,
                SwitchDisplayItem);

            var keyField = new PropertyField(keyProperty.FindPropertyRelative("key"));

            container.Add(localPlayerHelpBox);
            container.Add(targetField);
            container.Add(keyField);
            container.Add(itemContainer);

            return container;
        }

        public static string FormatTarget(GimmickTarget target)
        {
            switch (target)
            {
                case GimmickTarget.Player:
                    return "LocalPlayer";
                default:
                    return target.ToString();
            }
        }
    }
}
