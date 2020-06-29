using System;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.SpawnPoints;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SpawnPoint)), CanEditMultipleObjects]
    public class SpawnPointEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var spawnTypeProperty = serializedObject.FindProperty("spawnType");
            var spawnTypeField = new PropertyField(spawnTypeProperty);
            var worldGateKeyField = new PropertyField(serializedObject.FindProperty("worldGateKey"));

            void SwitchDisplayKeyField()
            {
                worldGateKeyField.SetVisibility((SpawnType) spawnTypeProperty.enumValueIndex == SpawnType.WorldGateDestination);
            }

            SwitchDisplayKeyField();
            spawnTypeField.RegisterCallback((ChangeEvent<string> ev) => SwitchDisplayKeyField());

            container.Add(spawnTypeField);
            container.Add(worldGateKeyField);
            container.Bind(serializedObject);

            return container;
        }
    }
}