using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(WorldItemReferenceList), isFallback = true), CanEditMultipleObjects]
    public class WorldItemReferenceListEditor : VisualElementEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = base.CreateInspectorGUI();

            var showWarning = ShowWarning(out var message);
            var helpBox = new HelpBox
            {
                text = message,
                messageType = HelpBoxMessageType.Warning
            };
            helpBox.SetVisibility(showWarning);
            helpBox.TrackSerializedObjectValue(serializedObject, _ =>
            {
                helpBox.SetVisibility(ShowWarning(out var message));
                helpBox.text = message;
            });
            container.Add(helpBox);
            return container;
        }

        IEnumerable<IWorldItemReferenceListEntry> InvalidWorldItemReferences(WorldItemReferenceList worldItemReferenceList)
        {
            string selfPrefabPath;

            if (EditorUtility.IsPersistent(worldItemReferenceList))
            {
                selfPrefabPath = AssetDatabase.GetAssetPath(worldItemReferenceList);
            }
            else if (PrefabStageUtility.GetCurrentPrefabStage() is { } prefabStage)
            {
                selfPrefabPath = prefabStage.assetPath;
            }
            else
            {
                selfPrefabPath = null;
            }

            return worldItemReferenceList.WorldItemReferences.Where(entry =>
            {
                if (entry.Item == null)
                {
                    return false;
                }
                var itemAssetPath = AssetDatabase.GetAssetPath(entry.Item.gameObject);
                return !string.IsNullOrEmpty(itemAssetPath) && itemAssetPath != selfPrefabPath;
            });
        }

        bool ShowWarning(out string message)
        {
            var invalidWorldItemReferences = serializedObject.targetObjects
                .OfType<WorldItemReferenceList>()
                .SelectMany(InvalidWorldItemReferences);

            if (!invalidWorldItemReferences.Any())
            {
                message = null;
                return false;
            }

            var invalidIds = string.Join(", ", invalidWorldItemReferences.Select(entry => entry.Id));
            message = TranslationUtility.GetMessage(TranslationTable.cck_worlditemreferencelist_scene_items_only, invalidIds);
            return true;
        }
    }
}
