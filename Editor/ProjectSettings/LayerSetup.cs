using ClusterVR.CreatorKit.Constants;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [InitializeOnLoad]
    public static class LayerSetup
    {
        static LayerSetup()
        {
            AddLayer(nameof(LayerName.CameraOnly), LayerName.CameraOnly);
            AddLayer(nameof(LayerName.VenueLayer0), LayerName.VenueLayer0);
            AddLayer(nameof(LayerName.VenueLayer1), LayerName.VenueLayer1);
            AddLayer(nameof(LayerName.PostProcessing), LayerName.PostProcessing);
            AddLayer(nameof(LayerName.PerformerOnly), LayerName.PerformerOnly);
            AddLayer(nameof(LayerName.VenueLayer2), LayerName.VenueLayer2);
        }

        static void AddLayer(string layerName, int layerIndex)
        {
            if (layerIndex is < 0 or > 31)
            {
                Debug.LogError("Layer index must be between 0 and 31. Provided index: " + layerIndex);
                return;
            }

            var tagManager =
                new SerializedObject(AssetDatabase.LoadMainAssetAtPath("ProjectSettings/TagManager.asset"));
            var layersProp = tagManager.FindProperty("layers");

            if (layersProp == null || !layersProp.isArray)
            {
                Debug.LogError("Unable to find 'layers' property in TagManager.asset.");
                return;
            }

            if (layersProp.GetArrayElementAtIndex(layerIndex).stringValue == layerName)
            {
                return;
            }

            for (var i = 0; i < layersProp.arraySize; i++)
            {
                var existingLayer = layersProp.GetArrayElementAtIndex(i);
                if (existingLayer.stringValue == layerName)
                {
                    existingLayer.stringValue = "";
                }
            }

            layersProp.GetArrayElementAtIndex(layerIndex).stringValue = layerName;

            tagManager.ApplyModifiedProperties();
        }
    }
}
