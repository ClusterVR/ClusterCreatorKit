using ClusterVR.CreatorKit.Extensions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(WorldItemTemplateOnlyAttribute))]
    public class WorldItemTemplateOnlyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var self = GetRootGameObject(property);
            var objectField = new ObjectField()
            {
                label = property.displayName,
                objectType = typeof(Item.Implements.Item),
                allowSceneObjects = false
            };
            objectField.BindProperty(property);

            objectField.RegisterValueChangedCallback(e =>
            {
                var target = e.newValue as Item.Implements.Item;
                if (target != null && self == target.gameObject)
                {
                    objectField.value = e.previousValue;
                }
            });

            return objectField;
        }

        static GameObject GetRootGameObject(SerializedProperty property)
        {
            var component = (Component)property.serializedObject.targetObject;
            return component.gameObject;
        }
    }
}
