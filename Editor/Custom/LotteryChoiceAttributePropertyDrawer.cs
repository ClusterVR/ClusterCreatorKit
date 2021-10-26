using ClusterVR.CreatorKit.Operation.Implements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(LotteryChoiceAttribute), true)]
    public sealed class LotteryChoiceAttributePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement
            {
                style =
                {
                    borderLeftWidth = new StyleFloat(2),
                    borderLeftColor = new StyleColor(Color.gray),
                    marginTop = new StyleLength(5),
                    marginBottom = new StyleLength(5),
                    marginLeft = new StyleLength(2),
                    paddingLeft = new StyleLength(2)
                }
            };

            var weightProperty = property.FindPropertyRelative("weight");
            var weightField = new PropertyField(weightProperty);
            container.Add(weightField);

            var triggersProperty = property.FindPropertyRelative("triggers");
            var triggersField = ReorderableArrayField.CreateReorderableArrayField(triggersProperty);
            container.Add(triggersField);

            return container;
        }
    }
}
