using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public class VisualElementEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();

            var iterator = serializedObject.GetIterator();
            if (!iterator.NextVisible(true)) return container;

            while (iterator.NextVisible(false))
            {
                var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };

                if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                    propertyField.SetEnabled(false);

                container.Add(propertyField);
            }

            container.Bind(serializedObject);

            return container;
        }
    }
}