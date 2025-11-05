using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(AudioConfigurationSet), useForChildren: true)]
    public class AudioConfigurationSetPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var idProperty = property.FindPropertyRelative("id");
            container.Add(new PropertyField(idProperty));

            var audioSourceProperty = property.FindPropertyRelative("audioSource");
            var audioSourceIsNotPrefabHelpBox = new IMGUIContainer(() =>
                EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_recommend_prefab_for_property, audioSourceProperty.displayName),
                    MessageType.Warning));

            void SwitchDisplayHelp(Object obj)
            {
                var isSceneObjectAssigned = obj != null && ((AudioSource) obj).gameObject.scene.name != null;
                audioSourceIsNotPrefabHelpBox.SetVisibility(isSceneObjectAssigned);
            }

            container.Add(audioSourceIsNotPrefabHelpBox);

            var audioSourceField = new PropertyField(audioSourceProperty);
            container.Add(audioSourceField);
            audioSourceField.RegisterValueChangeCallback(e => SwitchDisplayHelp(e.changedProperty.objectReferenceValue));
            return container;
        }
    }
}
