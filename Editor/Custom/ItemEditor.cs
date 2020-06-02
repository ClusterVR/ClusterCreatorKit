using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(CreatorKit.Item.Implements.Item)), CanEditMultipleObjects]
    public class ItemEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var item = target as CreatorKit.Item.Implements.Item;
            container.Add(new TextField("Id") { value = item.Id.Value.ToString(), isReadOnly = true });
            var itemNameField = new PropertyField(serializedObject.FindProperty("itemName"));
            container.Add(itemNameField);
            container.Bind(serializedObject);
            return container;
        }
    }
}