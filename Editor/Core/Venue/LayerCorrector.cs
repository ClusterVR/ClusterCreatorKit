using System.Linq;
using ClusterVR.CreatorKit.Constants;
using ClusterVR.CreatorKit.Item;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    [InitializeOnLoad]
    public static class LayerCorrector
    {
        static LayerCorrector()
        {
            EditorApplication.hierarchyChanged += CorrectLayer;
        }

        public static void CorrectLayer()
        {
            if (Application.isPlaying) return;
            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();
            var interactableItems = rootObjects.SelectMany(o => o.GetComponentsInChildren<IInteractableItem>(true));
            foreach (var interactableItem in interactableItems)
            {
                interactableItem.Item.gameObject.SetLayerRecursively(LayerName.InteractableItem);
            }
        }

        static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            if (gameObject.layer != layer)
            {
                gameObject.layer = layer;
                EditorUtility.SetDirty(gameObject);
                EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
            foreach (Transform t in gameObject.transform)
            {
                SetLayerRecursively(t.gameObject, layer);
            }
        }
    }
}
