using System.Linq;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(ItemGroupMember), isFallback = true), CanEditMultipleObjects]
    public class ItemGroupMemberEditor : VisualElementEditor
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

        static bool Validate(ItemGroupMember itemGroupMember)
        {
            string selfPrefabPath;

            if (EditorUtility.IsPersistent(itemGroupMember))
            {
                selfPrefabPath = null;
            }
            else if (PrefabStageUtility.GetCurrentPrefabStage() is { } prefabStage)
            {
                selfPrefabPath = prefabStage.assetPath;
            }
            else
            {
                selfPrefabPath = null;
            }

            var hostItem = itemGroupMember.Host;
            if (hostItem == null || hostItem.Item == null)
            {
                return false;
            }

            var itemAssetPath = AssetDatabase.GetAssetPath(hostItem.Item.gameObject);
            return string.IsNullOrEmpty(itemAssetPath) || itemAssetPath == selfPrefabPath;
        }

        bool ShowWarning(out string message)
        {
            var valid = serializedObject.targetObjects
                .OfType<ItemGroupMember>()
                .All(Validate);

            if (valid)
            {
                message = null;
                return false;
            }

            message = TranslationTable.cck_itemgroupmember_scene_items_only;
            return true;
        }
    }
}
