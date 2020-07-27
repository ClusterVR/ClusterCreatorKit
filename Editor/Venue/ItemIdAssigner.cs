using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    public static class ItemIdAssigner
    {
        public static void AssignItemId()
        {
            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();
            var hashSet = new HashSet<ItemId>();
            foreach (var item in rootObjects.SelectMany(o => o.GetComponentsInChildren<Item.Implements.Item>(true)))
            {
                while (item.Id.IsReserved() || hashSet.Contains(item.Id))
                {
                    var id = ItemId.Create();
                    item.Id = id;
                    if (!Application.isPlaying)
                    {
                        EditorUtility.SetDirty(item);
                        EditorSceneManager.MarkSceneDirty(scene);
                    }
                }
                hashSet.Add(item.Id);
            }
        }
    }
}