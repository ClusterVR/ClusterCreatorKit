using System.Collections.Generic;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class PlayerLocalObjectReferenceListValidator
    {
        public static IEnumerable<string> Validate(IPlayerLocalObjectReferenceList referenceList)
        {
            var messages = new List<string>();

            foreach (var entry in referenceList.PlayerLocalObjectReferences)
            {
                CheckId(messages, entry);
                CheckEntry(messages, entry);
            }

            return messages;
        }

        static void CheckId(List<string> messages, IPlayerLocalObjectReferenceListEntry entry)
        {
            var id = entry.Id;
            if (string.IsNullOrEmpty(id))
            {
                messages.Add(TranslationUtility.GetMessage(
                    TranslationTable.cck_id_empty,
                    nameof(PlayerLocalObjectReferenceList)
                    ));
                return;
            }

            if (!Constants.Component.ValidIdCharactersRegex.IsMatch(id))
            {
                messages.Add(IdValidateErrorMessages.RegexErrorMessage(nameof(PlayerLocalObjectReferenceList), id));
            }

            if (id.Length > Constants.Component.MaxIdLength)
            {
                messages.Add(IdValidateErrorMessages.LengthErrorMessage(nameof(PlayerLocalObjectReferenceList), id));
            }
        }

        static void CheckEntry(List<string> messages, IPlayerLocalObjectReferenceListEntry entry)
        {
            if (entry.GameObject == null)
            {
                messages.Add(TranslationUtility.GetMessage(
                    TranslationTable.cck_player_local_obj_ref_list_error_obj_empty,
                    entry.Id
                    ));
                return;
            }

            if (EditorUtility.IsPersistent(entry.GameObject))
            {
                messages.Add(TranslationUtility.GetMessage(
                    TranslationTable.cck_player_local_obj_ref_list_error_obj_prefab,
                    entry.Id
                ));
            }

            if (entry.GameObject.GetComponentInParent<IPlayerLocalUI>(true) == null)
            {
                messages.Add(TranslationUtility.GetMessage(
                    TranslationTable.cck_player_local_obj_ref_list_error_invalid_hierarchy,
                    entry.Id
                ));
            }
        }
    }
}
