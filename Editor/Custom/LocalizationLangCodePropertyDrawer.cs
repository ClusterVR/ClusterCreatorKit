using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.Localization;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomPropertyDrawer(typeof(LangCodeAttribute))]
    public sealed class LocalizationLangCodePropertyDrawer : PropertyDrawer
    {
        static readonly List<string> options = ServerLang.LangCodes.ToList();
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var currentIndex = options.FindIndex(option => option == property.stringValue);
            currentIndex = Math.Max(currentIndex, 0);
            
            var targetField = new PopupField<string>(options, options[currentIndex]);
            targetField.RegisterValueChangedCallback(e =>
            {
                property.stringValue = e.newValue;
                property.serializedObject.ApplyModifiedProperties();
            });
            
            return targetField;
        }
    }
}
