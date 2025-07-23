using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Translation;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public static class AttachTargetListValidator
    {
        public static IEnumerable<ValidationMessage> Validate(IAttachTargetList attachTargetList)
        {
            var messages = new List<ValidationMessage>();
            var emptyIdReported = false;
            var ids = new HashSet<string>();
            var collidedIds = new HashSet<string>();
            var emptyNodeReportedIds = new HashSet<string>();
            foreach (var attachTarget in attachTargetList.AttachTargets)
            {
                var id = attachTarget.Id;

                if (string.IsNullOrEmpty(id))
                {
                    if (!emptyIdReported)
                    {
                        messages.Add(new ValidationMessage(TranslationTable.cck_attachtargetlist_empty_id, ValidationMessage.MessageType.Error));
                        emptyIdReported = true;
                    }
                    continue;
                }

                if (ids.Add(id))
                {
                    if (!Constants.Component.ValidIdCharactersRegex.IsMatch(id))
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_attachtargetlist_invalid_characters, id), ValidationMessage.MessageType.Error));
                    }

                    if (id.Length > Constants.Component.MaxIdLength)
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_attachtargetlist_id_length, id, Constants.Component.MaxIdLength), ValidationMessage.MessageType.Error));
                    }
                }
                else
                {
                    collidedIds.Add(id);
                }

                var node = attachTarget.Node;
                if (node == null)
                {
                    if (emptyNodeReportedIds.Add(id))
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_attachtargetlist_node_not_found, id), ValidationMessage.MessageType.Error));
                    }
                }
            }

            if (collidedIds.Count > 0)
            {
                messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_attachtargetlist_duplicate_id, string.Join(", ", collidedIds)), ValidationMessage.MessageType.Error));
            }

            return messages;
        }
    }
}
